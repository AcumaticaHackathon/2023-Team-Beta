using System.Collections.Generic;

public class KeyPhraseResponse
{
    public string kind { get; set; }
    public Results results { get; set; }


    public class Document
    {
        public string id { get; set; }
        public List<string> keyPhrases { get; set; }
        public Statistics statistics { get; set; }
        public List<object> warnings { get; set; }
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


