namespace qa_automation_exercise__mejiabritoabraham.Utils
{
    internal static class Constants
    {
        #region Shared

        #region URLs

        #region API URLs
        internal const string ApiServerUrl = "http://localhost:3000";
        #endregion

        #region UI URLs
        internal const string ApprovedGiftUrl = "";
        internal const string ApprovedGiftUrlSpringSale20 = "";
        #endregion

        #endregion

        #endregion

        #region UI

        #region Tags
        public const string TagElementReference = "tag=";
        public const string AmazonTagSpringSale20 = "tag=ltk-spring-sale-20";
        #endregion

        #region AnalyticsEvents
        public const string PageView = "page_view";
        public const string ViewPromotion = "view_promotion";
        public const string SelectPromotion = "select_promotion";
        #endregion

        #endregion

        #region API

        #region Products
        internal const string ValidProductId = "ABC1234567";
        internal const string ValidProductId2 = "XYZ1234567";
        #endregion Products

        #region Partners
        internal const string PartnerId = "partner-001";
        internal const string OtherPartnerId = "partner-999";
        #endregion

        #endregion
    }
}