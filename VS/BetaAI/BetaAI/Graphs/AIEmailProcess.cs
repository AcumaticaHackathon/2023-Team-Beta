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
            Or<SMEmailExt.usrEvaluated,Equal<False>>>,
            OrderBy<Desc<SMEmail.createdDateTime>>>  EmailsToProcess;

        public PXSelect<SMEmail> EmailsToUpdate;

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

                    t.RefType = "PO";
                    if (order != null)
                    {
                        t.RefNbr = order.OrderNbr;
                        t.RefNoteId = order.NoteID;
                    }
                    else
                    {
                        t.RefNbr = "Not Mapped";
                    }
                    t.SubRefType = "Purchase Order Update";
                    t.AnalysisText = mail.Body;
                    graph.MasterView.Insert(t);

                    graph.Persist();
                    graph.AnalyzeEntityRecog(t);

                }
                if (mail.Subject.Contains("Case"))
                {

                    PXResult<CRActivity, CRCase> res = (PXResult<CRActivity, CRCase>)SelectFrom<CRActivity>
                        .InnerJoin<CRCase>.On<CRActivity.refNoteID.IsEqual<CRCase.noteID>>
                        .Where<CRActivity.noteID.IsEqual<@P.AsGuid>>.View.Select(graph, mail.RefNoteID);

                    CRCase crmCase = (CRCase)res;
                    t.RefType = "CRCase";
                    if (crmCase != null)
                    {
                        t.RefNbr = crmCase.CaseCD;
                        t.RefNoteId = crmCase.NoteID;
                    }
                    else
                    {
                        t.RefNbr = "Not Mapped";
                    }

                    t.SubRefType = "Case Analysis";
                    t.AnalysisText = mail.Body;
                    graph.MasterView.Insert(t);


                    graph.Persist();
                    graph.AnalyzeSentiment(t);

                }

                SMEmail updateEmail = mail;
                SMEmailExt ext = PXCache<SMEmail>.GetExtension<SMEmailExt>(updateEmail);
                ext.UsrEvaluated = true;

                //SetEmailFlat(mail);

                graph.Actions.PressSave();
                
            }
        }

        static void SetEmailFlat(SMEmail m)
        {
            AIEmailProcess g = PXGraph.CreateInstance<AIEmailProcess>();
            g.EmailsToProcess.Insert(m);
            g.Persist(typeof(SMEmail), PXDBOperation.Update);
        }

    }
}
