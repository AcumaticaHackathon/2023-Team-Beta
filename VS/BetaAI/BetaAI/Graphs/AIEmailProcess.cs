using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaAI
{
    public class AIEmailProcess : PXGraph<AIEmailProcess>
    {
        public PXCancel<SMEmail> Cancel;
        public PXProcessing<SMEmail, Where<SMEmailExt.usrEvaluated, IsNull,
            Or<SMEmailExt.usrEvaluated,Equal<False>>>>  EmailsToProcess;

        public AIEmailProcess()
        {

            EmailsToProcess.SetProcessDelegate(delegate (List<SMEmail> list)
            {
                List<SMEmail> newlist = new List<SMEmail>(list.Count);
                foreach (SMEmail doc in list)
                {
                    if (doc.Selected == true)
                    {
                        newlist.Add(doc);
                    }
                }
                Evaluatemail(newlist);
            });
        }

        public static void Evaluatemail(List<SMEmail> emailList)
        {
            foreach (SMEmail mail in emailList)
            {
                //is it a puchase order
                AITransMaint graph = PXGraph.CreateInstance<AITransMaint>();
                graph.Clear();
                AITrans t = new AITrans();

                if (mail.Subject.Contains("Purchase"))
                {
                    PXResult<CRActivity, POOrder> res = (PXResult<CRActivity, POOrder>)SelectFrom<CRActivity>
                        .InnerJoin<POOrder>.On<CRActivity.refNoteID.IsEqual<POOrder.noteID>>
                        .Where<CRActivity.noteID.IsEqual<@P.AsGuid>>.View.Select(graph, mail.RefNoteID);

                    POOrder order = (POOrder)res;

                    //t.TransNbr = "12345";
                    t.RefType = "PO";
                    //t.RefNbr = order.OrderNbr;
                    t.RefNbr = order.OrderNbr;
                    t.SubRefType = "Purchase Order Update";
                    t.AnalysisText = mail.Body;
                    graph.MasterView.Insert(t);

                }
                if (mail.Subject.Contains("Case"))
                {

                    PXResult<CRActivity, CRCase> res = (PXResult<CRActivity, CRCase>)SelectFrom<CRActivity>
                        .InnerJoin<POOrder>.On<CRActivity.refNoteID.IsEqual<CRCase.noteID>>
                        .Where<CRActivity.noteID.IsEqual<@P.AsGuid>>.View.Select(graph, mail.RefNoteID);

                    CRCase crmCase = (CRCase)res;
                    //t.TransNbr = "12346";
                    t.RefType = "CM";
                    t.RefNbr = crmCase.CaseCD;
                    t.SubRefType = "Case Analysis";
                    t.AnalysisText = mail.Body;
                    graph.MasterView.Insert(t);

                }

                graph.Actions.PressSave();
            }
        }

    }
}
