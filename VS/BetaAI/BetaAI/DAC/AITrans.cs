using System;
using PX.Data;

namespace BetaAI
{
  [Serializable]
  [PXCacheName("AITrans")]
  public class AITrans : IBqlTable
  {
    #region TransNbr
    [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Trans Nbr")]
    public virtual string TransNbr { get; set; }
    public abstract class transNbr : PX.Data.BQL.BqlString.Field<transNbr> { }
    #endregion

    #region RefType
    [PXDBString(60, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Ref Type")]
    public virtual string RefType { get; set; }
    public abstract class refType : PX.Data.BQL.BqlString.Field<refType> { }
    #endregion

    #region RefNbr
    [PXDBString(60, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Ref Nbr")]
    public virtual string RefNbr { get; set; }
    public abstract class refNbr : PX.Data.BQL.BqlString.Field<refNbr> { }
    #endregion

    #region RefNoteId
    [PXDBGuid()]
    [PXUIField(DisplayName = "Ref Note Id")]
    public virtual Guid? RefNoteId { get; set; }
    public abstract class refNoteId : PX.Data.BQL.BqlGuid.Field<refNoteId> { }
    #endregion

    #region SubRefType
    [PXDBString(60, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Sub Ref Type")]
    public virtual string SubRefType { get; set; }
    public abstract class subRefType : PX.Data.BQL.BqlString.Field<subRefType> { }
    #endregion

    #region SubRefNbr
    [PXDBString(60, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Sub Ref Nbr")]
    public virtual string SubRefNbr { get; set; }
    public abstract class subRefNbr : PX.Data.BQL.BqlString.Field<subRefNbr> { }
    #endregion

    #region SubNoteId
    [PXDBGuid()]
    [PXUIField(DisplayName = "Sub Note Id")]
    public virtual Guid? SubNoteId { get; set; }
    public abstract class subNoteId : PX.Data.BQL.BqlGuid.Field<subNoteId> { }
    #endregion

    #region AnalysisText
    [PXDBString(IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Analysis Text")]
    public virtual string AnalysisText { get; set; }
    public abstract class analysisText : PX.Data.BQL.BqlString.Field<analysisText> { }
    #endregion

    #region SentimentProcDate
    [PXDBDate()]
    [PXUIField(DisplayName = "Sentiment Proc Date")]
    public virtual DateTime? SentimentProcDate { get; set; }
    public abstract class sentimentProcDate : PX.Data.BQL.BqlDateTime.Field<sentimentProcDate> { }
    #endregion

    #region SentimentResult
    [PXDBString(40, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Sentiment Result")]
    public virtual string SentimentResult { get; set; }
    public abstract class sentimentResult : PX.Data.BQL.BqlString.Field<sentimentResult> { }
    #endregion

    #region SentimentPositive
    [PXDBDecimal()]
    [PXUIField(DisplayName = "Sentiment Positive")]
    public virtual Decimal? SentimentPositive { get; set; }
    public abstract class sentimentPositive : PX.Data.BQL.BqlDecimal.Field<sentimentPositive> { }
    #endregion

    #region SentimentNeutral
    [PXDBDecimal()]
    [PXUIField(DisplayName = "Sentiment Neutral")]
    public virtual Decimal? SentimentNeutral { get; set; }
    public abstract class sentimentNeutral : PX.Data.BQL.BqlDecimal.Field<sentimentNeutral> { }
    #endregion

    #region SentimentNegative
    [PXDBDecimal()]
    [PXUIField(DisplayName = "Sentiment Negative")]
    public virtual Decimal? SentimentNegative { get; set; }
    public abstract class sentimentNegative : PX.Data.BQL.BqlDecimal.Field<sentimentNegative> { }
    #endregion

    #region KeyPhraseProcDate
    [PXDBDate()]
    [PXUIField(DisplayName = "Key Phrase Proc Date")]
    public virtual DateTime? KeyPhraseProcDate { get; set; }
    public abstract class keyPhraseProcDate : PX.Data.BQL.BqlDateTime.Field<keyPhraseProcDate> { }
    #endregion

    #region EntityRecogProcDate
    [PXDBDate()]
    [PXUIField(DisplayName = "Entity Recog Proc Date")]
    public virtual DateTime? EntityRecogProcDate { get; set; }
    public abstract class entityRecogProcDate : PX.Data.BQL.BqlDateTime.Field<entityRecogProcDate> { }
    #endregion

    #region EntityLinkProcDate
    [PXDBDate()]
    [PXUIField(DisplayName = "Entity Link Proc Date")]
    public virtual DateTime? EntityLinkProcDate { get; set; }
    public abstract class entityLinkProcDate : PX.Data.BQL.BqlDateTime.Field<entityLinkProcDate> { }
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