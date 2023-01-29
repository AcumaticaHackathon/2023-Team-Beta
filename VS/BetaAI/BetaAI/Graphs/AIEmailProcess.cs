using PX.Data;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaAI
{
    public class AIEmailProcess : PXGraph<AIEmailProcess>
    {
        public PXCancel<SMEmail> Cancel;
        public PXProcessing<SMEmail, Where<SMEmailExt.usrEvaluated, Equal<False>>> EmailsToProcess;

        public AIEmailProcess()
        {

            EmailsToProcess.SetProcessDelegate(delegate (List<SMEmail> list)
            {
                List<SMEmail> newlist = new List<SMEmail>(list.Count);
                foreach (SMEmail doc in list)
                {
                    if (doc.Selected == true)
                    {
                        newlist.Add(doc);
                    }
                }
                Evaluatemail(newlist);
            });
        }

        public static void Evaluatemail(List<SMEmail> emailList)
        {

        }

    }
}
