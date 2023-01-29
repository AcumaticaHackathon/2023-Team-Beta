using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.BQL;

namespace BetaAI
{
    public class AITransMaint : PXGraph<AITransMaint, AITrans>
    {


        public PXSetup<AINumberingSequence> AISetup;

        public SelectFrom<AITrans>.View MasterView;
        public SelectFrom<AITransEntityLink>
            .Where<AITransEntityLink.transNbr.IsEqual<AITrans.transNbr.FromCurrent>>.View EntityLink;
        public SelectFrom<AITranSentiment>
            .Where<AITranSentiment.transNbr.IsEqual<AITrans.transNbr.FromCurrent>>.View Sentiment;
        public SelectFrom<AITransEntityRecog>
            .Where<AITransEntityRecog.transNbr.IsEqual<AITrans.transNbr.FromCurrent>>.View EntityRecog;
        public SelectFrom<AITransKeyPhrase>
            .Where<AITransKeyPhrase.transNbr.IsEqual<AITrans.transNbr.FromCurrent>>.View KeyPhrase;


        #region clear
        public PXAction<AITrans> clearanalysis;
        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Clear", MapEnableRights = PXCacheRights.Insert,
            MapViewRights = PXCacheRights.Insert)]
        public virtual System.Collections.IEnumerable ClearAnalysis(PXAdapter adapter)
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

            this.Persist();


            return adapter.Get();
        }
        #endregion

        #region sentiment
        public PXAction<AITrans> analyzeSentimentAction;
        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Sentiment", MapEnableRights = PXCacheRights.Insert,
            MapViewRights = PXCacheRights.Insert)]
        public virtual System.Collections.IEnumerable AnalyzeSentimentAction(PXAdapter adapter)
        {
            var rec = this.MasterView.Current;
            if (rec == null) return null;

            AnalyzeSentiment(rec);

            return adapter.Get();
        }

        public void AnalyzeSentiment(AITrans rec) 
        {

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
            this.Persist();


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
            sb.AppendFormat("        \"text\": {0}", Newtonsoft.Json.JsonConvert.ToString(rec.AnalysisText));
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

            this.Persist();

        }
        #endregion




        #region entitylink
        public PXAction<AITrans> analyzeEntityLinkAction;
        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "EntityLink", MapEnableRights = PXCacheRights.Insert,
            MapViewRights = PXCacheRights.Insert)]
        public virtual System.Collections.IEnumerable AnalyzeEntityLinkAction(PXAdapter adapter)
        {
            var rec = this.MasterView.Current;
            if (rec == null) return null;

            AnalyzeEntityLink(rec);

            return adapter.Get();

        }

        public void AnalyzeEntityLink(AITrans rec)
        { 
            // CLear Old Results from Child
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
            this.Persist();



            var sb = new System.Text.StringBuilder();
            sb.Length = 0;

            sb.AppendLine("{ ");
            sb.AppendLine("  \"kind\": \"EntityLinking\", ");
            sb.AppendLine("  \"parameters\": { ");
            sb.AppendLine("    \"modelVersion\": \"latest\" ");
            sb.AppendLine("  }, ");
            sb.AppendLine("  \"analysisInput\": { ");
            sb.AppendLine("    \"documents\": [ ");
            sb.AppendLine("      { ");
            sb.AppendLine("        \"id\": \"1\", ");
            sb.AppendLine("        \"language\": \"en\", ");
            sb.AppendFormat("        \"text\": {0}", Newtonsoft.Json.JsonConvert.ToString(rec.AnalysisText));
            sb.AppendLine("      } ");
            sb.AppendLine("    ] ");
            sb.AppendLine("  } ");
            sb.AppendLine("} ");


            var uri = new Uri(String.Concat(Constants.CSURLBase, Constants.CSURLAnalyzeText));
            bool errflag = false;
            string errmsg = "";
            string resp = "";
            System.Net.WebHeaderCollection oRespHeaders;

            resp = AISendRequest.SendRequest(uri, sb.ToString(), Constants.ContentTypeJson, Constants.SendMethodPost, out errflag, out errmsg, out oRespHeaders);

            EntityLinkResponse oResponse;
            oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<EntityLinkResponse>(resp);


            foreach (var doc in oResponse.results.documents)
            {
                rec.EntityLinkProcDate = DateTime.Now;

                foreach (var entity in doc.entities)
                {
                    foreach (var match in entity.matches)
                    {
                        var newrec = new AITransEntityLink();
                        newrec.TransNbr = rec.TransNbr;
                        newrec.EntityName = entity.name;
                        newrec.EntityText = match.text;
                        newrec.Confidence = new System.Decimal(match.confidenceScore);

                        EntityLink.Insert(newrec);
                    }
                }

            }

            MasterView.Cache.Update(rec);
            MasterView.Update(rec);

            this.Persist();

        }
        #endregion


        #region entityrecogAction
        public PXAction<AITrans> analyzeEntityRecogAction;
        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "EntityRecog", MapEnableRights = PXCacheRights.Insert,
            MapViewRights = PXCacheRights.Insert)]
        public virtual System.Collections.IEnumerable AnalyzeEntityRecogAction(PXAdapter adapter)
        {
            var rec = this.MasterView.Current;
            if (rec == null) return null;

            AnalyzeEntityRecog(rec);

            return adapter.Get();

        }

        public void AnalyzeEntityRecog(AITrans rec)
        { 
            // CLear Old Results from Child
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
            this.Persist();

            // This one works different.  You have to call a "Job" and then later on call to get the results

            var sb = new System.Text.StringBuilder();
            sb.Length = 0;

            sb.AppendLine("{ ");
            sb.AppendLine("    \"tasks\": [ ");
            sb.AppendLine("        { ");
            sb.AppendLine("            \"kind\": \"CustomEntityRecognition\", ");
            sb.AppendLine("            \"parameters\": { ");
            sb.AppendLine("                \"projectName\": \"Hackathon2023\", ");
            sb.AppendLine("                \"deploymentName\": \"Hackathon2023\", ");
            sb.AppendLine("                \"stringIndexType\": \"TextElement_v8\" ");
            sb.AppendLine("            } ");
            sb.AppendLine("        } ");
            sb.AppendLine("    ], ");
            sb.AppendLine("    \"displayName\": \"CustomTextPortal_CustomEntityRecognition\", ");
            sb.AppendLine("    \"analysisInput\": { ");
            sb.AppendLine("        \"documents\": [ ");
            sb.AppendLine("            { ");
            sb.AppendLine("                \"id\": \"document_CustomEntityRecognition\", ");
            sb.AppendFormat("                \"text\": {0}", Newtonsoft.Json.JsonConvert.ToString(rec.AnalysisText));
            sb.AppendLine();
            sb.AppendLine("                \"language\": \"EN\" ");
            sb.AppendLine("            } ");
            sb.AppendLine("        ] ");
            sb.AppendLine("    } ");
            sb.AppendLine("} ");



            var uri = new Uri(String.Concat(Constants.CSURLBase, Constants.CSURLAnalyzeTextJob));
            bool errflag = false;
            string errmsg = "";
            string resp = "";
            System.Net.WebHeaderCollection oRespHeaders;

            resp = AISendRequest.SendRequest(uri, sb.ToString(), Constants.ContentTypeJson, Constants.SendMethodPost, out errflag, out errmsg, out oRespHeaders);

            // This request returns the result to call the endpoint to get it in the operation-location header
            string operlocation = oRespHeaders["operation-location"];

            // Sleep
            // For a real/live implementation, this shoud be put into a loop that if not done yet, keep querying
            System.Threading.Thread.Sleep(2000);

            // Get Response
            uri = new Uri(operlocation);
            errflag = false;
            errmsg = "";
            resp = "";
            oRespHeaders = null;

            resp = AISendRequest.SendRequest(uri, "", Constants.ContentTypeJson, Constants.SendMethodGet, out errflag, out errmsg, out oRespHeaders);

            CustomEntityResponse oResponse;
            oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomEntityResponse>(resp);

            rec.EntityLinkProcDate = DateTime.Now;
            foreach (var item in oResponse.tasks.items)
            {

                foreach (var doc in item.results.documents)
                {
                    foreach (var entity in doc.entities)
                    {
                        var newrec = new AITransEntityRecog();
                        newrec.TransNbr = rec.TransNbr;
                        newrec.EntityText = entity.text;
                        newrec.Category = entity.category;
                        newrec.Confidence = new System.Decimal(entity.confidenceScore);

                        EntityRecog.Insert(newrec);
                    }
                }
            }

            MasterView.Cache.Update(rec);
            MasterView.Update(rec);

            this.Persist();

        }
        #endregion

        #region keyphraseaction
        public PXAction<AITrans> analyzeKeyPhraseAction;
        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "KeyPhrase", MapEnableRights = PXCacheRights.Insert,
            MapViewRights = PXCacheRights.Insert)]
        public virtual System.Collections.IEnumerable AnalyzeKeyPhraseAction(PXAdapter adapter)
        {
            var rec = this.MasterView.Current;
            if (rec == null) return null;

            AnalyzeKeyPhrase(rec);

            return adapter.Get();

        }

        public void AnalyzeKeyPhrase(AITrans rec)
        {
            // CLear Old Results from Child
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
            this.Persist();



            var sb = new System.Text.StringBuilder();
            sb.Length = 0;

            sb.AppendLine("{ ");
            sb.AppendLine("  \"kind\": \"KeyPhraseExtraction\", ");
            sb.AppendLine("  \"parameters\": { ");
            sb.AppendLine("    \"modelVersion\": \"latest\" ");
            sb.AppendLine("  }, ");
            sb.AppendLine("  \"analysisInput\": { ");
            sb.AppendLine("    \"documents\": [ ");
            sb.AppendLine("      { ");
            sb.AppendLine("        \"id\": \"1\", ");
            sb.AppendLine("        \"language\": \"en\", ");
            sb.AppendFormat("        \"text\": {0}", Newtonsoft.Json.JsonConvert.ToString(rec.AnalysisText));
            sb.AppendLine();

            sb.AppendLine("      } ");
            sb.AppendLine("    ] ");
            sb.AppendLine("  } ");
            sb.AppendLine("} ");




            var uri = new Uri(String.Concat(Constants.CSURLBase, Constants.CSURLAnalyzeText));
            bool errflag = false;
            string errmsg = "";
            string resp = "";
            System.Net.WebHeaderCollection oRespHeaders;

            resp = AISendRequest.SendRequest(uri, sb.ToString(), Constants.ContentTypeJson, Constants.SendMethodPost, out errflag, out errmsg, out oRespHeaders);

            KeyPhraseResponse oResponse;
            oResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<KeyPhraseResponse>(resp);


            foreach (var doc in oResponse.results.documents)
            {
                rec.KeyPhraseProcDate = DateTime.Now;

                foreach (var phrase in doc.keyPhrases)
                {
                    var newrec = new AITransKeyPhrase();
                    newrec.TransNbr = rec.TransNbr;
                    newrec.KeyPhrase = phrase;

                    KeyPhrase.Insert(newrec);
                }

            }

            MasterView.Cache.Update(rec);
            MasterView.Update(rec);

            this.Persist();

        }
        #endregion


    }


}
