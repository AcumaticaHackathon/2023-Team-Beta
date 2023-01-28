using System;
using PX.Data;

namespace BetaAI
{
  [Serializable]
  [PXCacheName("AITransEntityRecog")]
  public class AITransEntityRecog : IBqlTable
  {
    #region TransNbr
    [PXDBString(15, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Trans Nbr")]
    public virtual string TransNbr { get; set; }
    public abstract class transNbr : PX.Data.BQL.BqlString.Field<transNbr> { }
    #endregion

    #region LIneNbr
    [PXDBIdentity(IsKey = true)]
    public virtual int? LIneNbr { get; set; }
    public abstract class lIneNbr : PX.Data.BQL.BqlInt.Field<lIneNbr> { }
    #endregion

    #region EntityText
    [PXDBString(IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Entity Text")]
    public virtual string EntityText { get; set; }
    public abstract class entityText : PX.Data.BQL.BqlString.Field<entityText> { }
    #endregion

    #region Category
    [PXDBString(200, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Category")]
    public virtual string Category { get; set; }
    public abstract class category : PX.Data.BQL.BqlString.Field<category> { }
    #endregion

    #region Confidence
    [PXDBDecimal()]
    [PXUIField(DisplayName = "Confidence")]
    public virtual Decimal? Confidence { get; set; }
    public abstract class confidence : PX.Data.BQL.BqlDecimal.Field<confidence> { }
    #endregion

    #region Tstamp
    [PXDBTimestamp()]
    [PXUIField(DisplayName = "Tstamp")]
    public virtual byte[] Tstamp { get; set; }
    public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
    #endregion

    #region CreatedByID
    [PXDBCreatedByID()]
    public virtual Guid? CreatedByID { get; set; }
    public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
    #endregion

    #region CreatedByScreenID
    [PXDBCreatedByScreenID()]
    public virtual string CreatedByScreenID { get; set; }
    public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
    #endregion

    #region CreatedDateTime
    [PXDBCreatedDateTime()]
    public virtual DateTime? CreatedDateTime { get; set; }
    public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
    #endregion

    #region LastModifiedByID
    [PXDBLastModifiedByID()]
    public virtual Guid? LastModifiedByID { get; set; }
    public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
    #endregion

    #region LastModifiedByScreenID
    [PXDBLastModifiedByScreenID()]
    public virtual string LastModifiedByScreenID { get; set; }
    public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
    #endregion

    #region LastModifiedDateTime
    [PXDBLastModifiedDateTime()]
    public virtual DateTime? LastModifiedDateTime { get; set; }
    public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
    #endregion

    #region Noteid
    [PXNote()]
    public virtual Guid? Noteid { get; set; }
    public abstract class noteid : PX.Data.BQL.BqlGuid.Field<noteid> { }
    #endregion
  }
}