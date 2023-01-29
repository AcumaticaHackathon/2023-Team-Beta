using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects;
using PX.SM;
using PX.TM;
using PX.Web.UI;
using System.Collections.Generic;
using System;

namespace PX.Objects.CR
{
  public class SMEmailExt : PXCacheExtension<PX.Objects.CR.SMEmail>
  {
    #region UsrEvaluated
    [PXDBBool]
[PXUIField(DisplayName="Evaluated")]

    public virtual bool? UsrEvaluated { get; set; }
    public abstract class usrEvaluated : PX.Data.BQL.BqlBool.Field<usrEvaluated> { }
    #endregion
  }
}