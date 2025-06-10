using System.Threading.Tasks;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using FluentAssertions;
using NUnit.Framework;
using qa_automation_exercise__mejiabritoabraham.Base.UI;
using qa_automation_exercise__mejiabritoabraham.Utils;

namespace qa_automation_exercise__mejiabritoabraham.Tests.UI.GiftPage_Task1
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Gift Page Tests")]
    public class GiftPageTests : UiTestBase
    {

        [Test, Order(1)]
        public async Task ShouldLoadPageMenu()
        {
            await GiftPage.NavigateToGiftPage();
            var isTopMenuBarVisible = await GiftPage.IsPageTopMenuLoadedCorrectly();
            isTopMenuBarVisible.Should().BeTrue("Because the top menu bar is visible");
        }

        [Test, Order(2)]
        public async Task ShouldLoadPageTitle()
        {
            var isTitleVisible = await GiftPage.IsPageTitleLoadedCorrectly();
            isTitleVisible.Should().BeTrue("Because the title is visible");
        }

        [Test, Order(3)]
        public async Task ShouldDisplayNewsletterSubConfirmationMessage()
        {
            await GiftPage.SubscribeToNewsletterEmail();
            var isSuccessfullySubscribedMessageDisplayed = await GiftPage.IsSuccessfullySubscribedMessageDisplayedCorrectly();
            isSuccessfullySubscribedMessageDisplayed.Should().BeTrue("Because the message is displayed");
        }

        [Test, Order(4)]
        public async Task ShouldContainAmazonLinksOnTagParameter()
        {
            await GiftPage.AssertAllAmazonLinksHaveTag(Constants.TagElementReference);
        }

        [Test, Order(5)]
        public async Task ShouldTriggerGoogleAnalyticsEvents()
        {
            var events = await GiftPage.GetTriggeredTrackingEvents();
            events.Should().Contain(Constants.PageView);
            events.Should().Contain(Constants.ViewPromotion);
            events.Should().Contain(Constants.SelectPromotion);
        }

        [Test, Order(6)]
        public async Task ShouldContainPromotionIdOnTagParameter()
        {
            await GiftPage.NavigateTo(Constants.ApprovedGiftUrlSpringSale20);
            await GiftPage.AssertPromotionTagInUrl(Constants.AmazonTagSpringSale20);
            await GiftPage.AssertAllAmazonLinksHaveTag(Constants.AmazonTagSpringSale20);
        }
    }
}