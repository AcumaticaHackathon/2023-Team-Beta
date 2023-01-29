using System.Collections.Generic;


public class EntityLinkResponse
{
    public string kind { get; set; }
    public Results results { get; set; }

    public class Document
    {
        public string id { get; set; }
        public Statistics statistics { get; set; }
        public List<Entity> entities { get; set; }
        public List<object> warnings { get; set; }
    }

    public class Entity
    {
        public string bingId { get; set; }
        public string name { get; set; }
        public List<Match> matches { get; set; }
        public string language { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public string dataSource { get; set; }
    }

    public class Match
    {
        public string text { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
        public double confidenceScore { get; set; }
    }

    public class Results
    {
        public Statistics statistics { get; set; }
        public List<Document> documents { get; set; }
        public List<object> errors { get; set; }
        public string modelVersion { get; set; }
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


