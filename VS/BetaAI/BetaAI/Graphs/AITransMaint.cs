using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace BetaAI
{
  public class AITransMaint : PXGraph<AITransMaint>
  {
        #region DataViews
        public SelectFrom<AITrans>.View Transactions;
        #endregion

        #region Event Handlers
        #endregion
    }
}