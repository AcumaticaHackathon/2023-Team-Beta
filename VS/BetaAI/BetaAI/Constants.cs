using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaAI
{
    public class Constants
    {
        public const string APIKey = "a8b600bdf9a943e9a0ef99ddc6f83398";
        public const string CSURLBase = "https://hack2023beta.cognitiveservices.azure.com/language/";
        public const string CSURLAnalyzeText = ":analyze-text?api-version=2022-05-01&showStats=true";
        public const string CSURLAnalyzeTextJob = "analyze-text/jobs?api-version=2022-10-01-preview";
        public const string ContentTypeJson = "application/json";
        public const string SendMethodPost = "POST";
        public const string SendMethodGet = "GET";
    }
}
