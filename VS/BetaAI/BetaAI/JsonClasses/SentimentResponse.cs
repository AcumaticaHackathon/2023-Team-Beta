using System.Collections.Generic;

public class SentimentResponse
{
    public string kind { get; set; }
    public Results results { get; set; }



    public class ConfidenceScores
    {
        public double positive { get; set; }
        public double neutral { get; set; }
        public double negative { get; set; }
    }

    public class Document
    {
        public string id { get; set; }
        public string sentiment { get; set; }
        public Statistics statistics { get; set; }
        public ConfidenceScores confidenceScores { get; set; }
        public List<Sentence> sentences { get; set; }
        public List<object> warnings { get; set; }
    }

    public class Results
    {
        public Statistics statistics { get; set; }
        public List<Document> documents { get; set; }
        public List<object> errors { get; set; }
        public string modelVersion { get; set; }
    }



    public class Sentence
    {
        public string sentiment { get; set; }
        public ConfidenceScores confidenceScores { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
        public string text { get; set; }
    }

    public class Statistics
    {
        public int documentsCount { get; set; }
        public int validDocumentsCount { get; set; }
        public int erroneousDocumentsCount { get; set; }
        public int transactionsCount { get; set; }
        public int charactersCount { get; set; }
    }

}
