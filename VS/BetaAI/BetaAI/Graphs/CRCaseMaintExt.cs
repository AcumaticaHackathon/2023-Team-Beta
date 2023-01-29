using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;
using System.Web;
using System.Net;
using System.Collections;
using System.Linq;
using PX.Common;
using System.Runtime.CompilerServices;
using BetaAI;

namespace PX.Objects.CR
{
    public class CRCaseMaint_Extension : PXGraphExtension<CRCaseMaint>
    {
        public PXSelect<CRCase> Que_Cases;

        public SelectFrom<AITrans>.
        Where<AITrans.refNoteId.IsEqual<CRCase.noteID.FromCurrent>>.
        OrderBy<AITrans.createdDateTime.Desc>.View AITrans;

        public PXAction<CRCase> analyzesentiment;

        [PXUIField(DisplayName = "Sentiment", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton(CommitChanges = true)]
        public virtual IEnumerable AnalyzeSentiment(PXAdapter adapter)
        {
            if (Que_Cases.Current != null)
            {
                var reccase = Que_Cases.Current;
                var graph = PXGraph.CreateInstance<BetaAI.AITransMaint>();

                var rec = new AITrans();
                rec.RefType = "CRCase";
                rec.RefNbr = reccase.CaseCD;
                rec.RefNoteId = reccase.NoteID;
                rec.SubRefType = "DetailsSentiment";
                rec.AnalysisText = reccase.Description;
                graph.MasterView.Insert(rec);
                graph.Persist();
                
                graph.AnalyzeSentiment(rec);

            }
            return adapter.Get();
        }

    }

}