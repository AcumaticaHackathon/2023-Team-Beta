using System;
using PX.Data;

namespace BetaAI
{
            [Serializable]
            [PXCacheName("AITranSentiment")]
            public class AITranSentiment : IBqlTable
            {
                        #region TransNbr
                        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
                        [PXUIField(DisplayName = "Trans Nbr")]
                        public virtual string TransNbr { get; set; }
                        public abstract class transNbr : PX.Data.BQL.BqlString.Field<transNbr> { }
                        #endregion

                        #region LineNbr
                        [PXDBIdentity(IsKey = true)]
                        public virtual int? LineNbr { get; set; }
                        public abstract class lineNbr : PX.Data.BQL.BqlInt.Field<lineNbr> { }
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

                        #region SentenceText
                        [PXDBString(IsUnicode = true, InputMask = "")]
                        [PXUIField(DisplayName = "Sentence Text")]
                        public virtual string SentenceText { get; set; }
                        public abstract class sentenceText : PX.Data.BQL.BqlString.Field<sentenceText> { }
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