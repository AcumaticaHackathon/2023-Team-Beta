using System.Collections.Generic;
public class CustomEntityResponse
{
    public string jobId { get; set; }
    public string lastUpdatedDateTime { get; set; }
    public string createdDateTime { get; set; }
    public string expirationDateTime { get; set; }
    public string status { get; set; }
    public List<object> errors { get; set; }
    public string displayName { get; set; }
    public Tasks tasks { get; set; }


    public class Document
    {
        public string id { get; set; }
        public List<Entity> entities { get; set; }
        public List<object> warnings { get; set; }
    }

    public class Entity
    {
        public string text { get; set; }
        public string category { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
        public double confidenceScore { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string lastUpdateDateTime { get; set; }
        public string status { get; set; }
        public Results results { get; set; }
    }

    public class Results
    {
        public List<Document> documents { get; set; }
        public List<object> errors { get; set; }
        public string projectName { get; set; }
        public string deploymentName { get; set; }
    }


    public class Tasks
    {
        public int completed { get; set; }
        public int failed { get; set; }
        public int inProgress { get; set; }
        public int total { get; set; }
        public List<Item> items { get; set; }
    }


}


