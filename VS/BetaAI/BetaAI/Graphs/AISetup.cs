using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaAI
{
    public class AISetupMaint : PXGraph<AISetupMaint>
    {
        public PXSelect<AINumberingSequence> AISetupRecord;
        public PXSave<AINumberingSequence> Save;
        public PXCancel<AINumberingSequence> Cancel;
    }
}
