using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;

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

        #region clear
        public PXAction<AITrans> clear;
        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Clear", MapEnableRights = PXCacheRights.Insert,
            MapViewRights = PXCacheRights.Insert)]
        public virtual System.Collections.IEnumerable Clear(PXAdapter adapter)
        {
            var rec = this.MasterView.Current;
            if (rec == null) return null;

            rec.SentimentProcDate = null;
            rec.SentimentResult = null;
            rec.SentimentPositive = null;
            rec.SentimentNegative = null;
            rec.SentimentNeutral = null;
            rec.KeyPhraseProcDate = null;
            rec.EntityRecogProcDate = null;
            rec.EntityLinkProcDate = null;


            MasterView.Cache.Update(rec);
            MasterView.Update(rec);


            var sentiments = SelectFrom<AITranSentiment>.
                      Where<AITranSentiment.transNbr.
                      IsEqual<@P.AsString>>.View.Select(this, rec.TransNbr);
            if (sentiments != null)
            {
                foreach (var onerec in sentiments)
                {
                    this.Sentiment.Delete(onerec);
                }
            }

            var entitylinks = SelectFrom<AITransEntityLink>.
                      Where<AITransEntityLink.transNbr.
                      IsEqual<@P.AsString>>.View.Select(this, rec.TransNbr);
            if (entitylinks != null)
            {
                foreach (var onerec in entitylinks)
                {
                    this.EntityLink.Delete(onerec);
                }
            }

            var entityrecogs = SelectFrom<AITransEntityRecog>.
                      Where<AITransEntityRecog.transNbr.
                      IsEqual<@P.AsString>>.View.Select(this, rec.TransNbr);
            if (entityrecogs != null)
            {
                foreach (var onerec in entityrecogs)
                {
                    this.EntityRecog.Delete(onerec);
                }
            }

            var keyphrases = SelectFrom<AITransKeyPhrase>.
                      Where<AITransKeyPhrase.transNbr.
                      IsEqual<@P.AsString>>.View.Select(this, rec.TransNbr);
            if (keyphrases != null)
            {
                foreach (var onerec in keyphrases)
                {
                    this.KeyPhrase.Delete(onerec);
                }
            }

            Actions.PressSave();


            return adapter.Get();
        }
        #endregion

        #region sentiment
        public PXAction<AITrans> analyzeSentiment;
        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Analyze Sentiment", MapEnableRights = PXCacheRights.Insert,
            MapViewRights = PXCacheRights.Insert)]
        public virtual System.Collections.IEnumerable AnalyzeSentiment(PXAdapter adapter)
        {
            var rec = this.MasterView.Current;
            if (rec == null) return null;

            // CLear Old Results from Child
            var sentiments = SelectFrom<AITranSentiment>.
                      Where<AITranSentiment.transNbr.
                      IsEqual<@P.AsString>>.View.Select(this, rec.TransNbr);
            if (sentiments != null)
            {
                foreach (var onerec in sentiments)
                {
                    this.Sentiment.Delete(onerec);
                }
            }
            Actions.PressSave();


            var sb = new System.Text.StringBuilder();
            sb.Length = 0;

            sb.AppendLine("{");
            sb.AppendLine("  \"kind\": \"SentimentAnalysis\",");
            sb.AppendLine("  \"parameters\": {");
            sb.AppendLine("    \"modelVersion\": \"latest\"");
            sb.AppendLine("  },");
            sb.AppendLine("  \"analysisInput\": {");
            sb.AppendLine("    \"documents\": [");
            sb.AppendLine("      {");
            sb.AppendLine("        \"id\": \"1\",");
            sb.AppendLine("        \"language\": \"en\",");
            sb.AppendFormat("        \"text\": \"{0}\"", rec.AnalysisText);
            sb.AppendLine();

            sb.AppendLine("      }");
            sb.AppendLine("    ]");
            sb.AppendLine("  }");
            sb.AppendLine("}");

            var uri = new Uri(String.Concat(Constants.CSURLBase, Constants.CSURLAnalyzeText));
            bool errflag = false;
            string errmsg = "";
            string resp = "";
            System.Net.WebHeaderCollection oRespHeaders;

            resp = AISendRequest.SendRequest(uri, sb.ToString(), Constants.ContentTypeJson, Constants.SendMethodPost, out errflag, out errmsg, out oRespHeaders);

            SentimentResponse oResponse;
            oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SentimentResponse>(resp);


            foreach (var doc in oResponse.results.documents)
            {
                rec.SentimentProcDate = DateTime.Now;
                rec.SentimentResult = doc.sentiment;
                rec.SentimentPositive = new System.Decimal(doc.confidenceScores.positive);
                rec.SentimentNegative = new System.Decimal(doc.confidenceScores.negative);
                rec.SentimentNeutral = new System.Decimal(doc.confidenceScores.neutral);

                foreach (var sentence in doc.sentences)
                {
                    var newrec = new AITranSentiment();
                    newrec.TransNbr = rec.TransNbr;
                    newrec.SentimentPositive = new System.Decimal(sentence.confidenceScores.positive);
                    newrec.SentimentNeutral = new System.Decimal(sentence.confidenceScores.neutral);
                    newrec.SentimentNegative = new System.Decimal(sentence.confidenceScores.negative);
                    newrec.SentenceText = sentence.text;

                    Sentiment.Insert(newrec);
                }
            }

            MasterView.Cache.Update(rec);
            MasterView.Update(rec);

            Actions.PressSave();

            return adapter.Get();
        }
        #endregion

    }


}
