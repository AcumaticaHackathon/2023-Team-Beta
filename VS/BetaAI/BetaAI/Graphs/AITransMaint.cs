using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace BetaAI
{
    public class AITransMaint : PXGraph<AITransMaint, AITrans>
    {


        public SelectFrom<AITrans>.View MasterView;
        public SelectFrom<AITransEntityLink>.View EntityLink;  //TODO:  Need to rename this to be specific type and add all 4
        public SelectFrom<AITranSentiment>.View Sentiment;
        public SelectFrom<AITransEntityRecog>.View EntityRecognition;
        public SelectFrom<AITransKeyPhrase>.View KeyPhrase;


    }
}