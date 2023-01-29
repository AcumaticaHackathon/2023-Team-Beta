using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace BetaAI
{
    public class AITransMaint : PXGraph<AITransMaint, AITrans>
    {


        public SelectFrom<AITrans>.View MasterView;
        public SelectFrom<AITransEntityLink>
            .Where<AITransEntityLink.transNbr.IsEqual<AITrans.transNbr.FromCurrent>>.View EntityLink;  //TODO:  Need to rename this to be specific type and add all 4
        public SelectFrom<AITranSentiment>
            .Where<AITranSentiment.transNbr.IsEqual<AITrans.transNbr.FromCurrent>>.View Sentiment;
        public SelectFrom<AITransEntityRecog>
            .Where<AITransEntityRecog.transNbr.IsEqual<AITrans.transNbr.FromCurrent>>.View EntityRecog;
            
        public SelectFrom<AITransKeyPhrase>
            .Where<AITransKeyPhrase.transNbr.IsEqual<AITrans.transNbr.FromCurrent>>.View KeyPhrase;
    }
}