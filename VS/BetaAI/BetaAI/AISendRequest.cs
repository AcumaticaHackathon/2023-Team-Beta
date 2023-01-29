using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaAI
{
    public static class AISendRequest
    {

        public static string SendRequest(
            Uri uri,
            string dataToSend,
            string contentType,
            string method,
            out bool ErrFlag,
            out string ErrMsg,
            out System.Net.WebHeaderCollection oRespHeaders)
        {
            string res = "";
            byte[] jsonDataBytes = null;
            int contentLength = 0;
            ErrFlag = false;
            ErrMsg = "";
            System.Net.HttpStatusCode ResponseCode = 0;
            oRespHeaders = null;

            if (dataToSend != "")
            {
                jsonDataBytes = System.Text.Encoding.UTF8.GetBytes(dataToSend);
                contentLength = jsonDataBytes.Length;
            }


            System.Net.WebRequest req = System.Net.WebRequest.Create(uri);
            req.Timeout = 120000; // Timeout in Milliseconds - 120 seconds (2 minutes)
            req.ContentType = contentType;
            req.Method = method;
            req.ContentLength = contentLength;
            req.Headers.Add("Ocp-Apim-Subscription-Key", Constants.APIKey);

            if (contentLength > 0)
            {
                var stream = req.GetRequestStream();
                stream.Write(jsonDataBytes, 0, jsonDataBytes.Length);
                stream.Close();
            }

            try
            {
                // Reset All Fields in case we are retrying from error
                ErrFlag = false;
                ErrMsg = "";
                res = "";
                ResponseCode = 0;
                oRespHeaders = null;

                var response = req.GetResponse();
                var responseStream = response.GetResponseStream();
                oRespHeaders = response.Headers;

                var oResp = (System.Net.HttpWebResponse)response;
                ResponseCode = oResp.StatusCode;

                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream);
                res = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
            }
            catch (System.Net.WebException ex)
            {
                ErrFlag = true;
                var oResp = (System.Net.HttpWebResponse)ex.Response;
                ResponseCode = oResp.StatusCode;

                var responseStream = ex.Response.GetResponseStream();
                if (responseStream.CanRead)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(responseStream);
                    res = reader.ReadToEnd();
                    reader.Close();
                }
                responseStream.Close();

                if (res.Trim().Length == 0)
                    ErrMsg = ResponseCode + " - " + ex.Message;
                else
                    ErrMsg = ResponseCode + " - " + res + " - " + ex.Message;
            }

            catch (Exception ex)
            {
                ErrFlag = true;
                ErrMsg = ex.Message;
            }

            return res;
        }
    }
}
