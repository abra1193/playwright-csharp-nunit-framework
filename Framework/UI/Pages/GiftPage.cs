using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using qa_automation_exercise__mejiabritoabraham.Shared;
using qa_automation_exercise__mejiabritoabraham.Utils;

namespace qa_automation_exercise__mejiabritoabraham.Framework.UI.Pages
{
    public class GiftPage
    {
        private readonly IPage _page;

        public GiftPage(IPage page)
        {
            _page = page;
        }

        private ILocator AmazonLinks => _page.Locator("a[href*='amazon.com']");
        private ILocator AcceptCookiesButton => _page.Locator("button[mode='primary']:has-text(\"Accept\")");
        private ILocator RejectCookiesButton => _page.Locator("button[mode='secondary']:has-text(\"Reject All\")");
        private ILocator GiftPageTopMenu => _page.Locator("//header[@class=\"navigation-header theme-ltk\"]");
        private ILocator GiftPageTitle => _page.Locator("//h1[normalize-space()=\"15 Pooch-Approved Gifts\"]");
        private ILocator CountMeInButton => _page.Locator("button[class='atom-ltk-button mt-3.5 h-12.5 w-full" +
                                                          " md:mt-0 md:w-full']");
        private ILocator AgreeEmailCommunicationCheckbox => _page.Locator("(//div[contains(@class, 'agree')]" +
                                                                          "//input[@id='newsletter-agree'])[2]");
        private ILocator SuccessfullySubscribedMessage =>
            _page.Locator("div[class=\"newsletter-block__heading md:mb-4\"]");

        public async Task<bool> IsPageTopMenuLoadedCorrectly() => await GiftPageTopMenu.IsVisibleAsync();
        public async Task<bool> IsPageTitleLoadedCorrectly() => await GiftPageTitle.IsVisibleAsync();
        public async Task<bool> IsSuccessfullySubscribedMessageDisplayedCorrectly() =>
            await SuccessfullySubscribedMessage.IsVisibleAsync();

        private async Task AcceptCookies()
        {
            if (await RejectCookiesButton.CountAsync() > 0)
            {
                await RejectCookiesButton.ClickAsync();
            }
            else if (await AcceptCookiesButton.CountAsync() > 0)
            {
                await AcceptCookiesButton.ClickAsync();
            }
        }

        public async Task NavigateToGiftPage()
        {
            await _page.WaitForSelectorAsync("main[class=\"content-page__content\"]");
            await AcceptCookies();
        }

        public async Task NavigateTo(string url)
        {
            await _page.GotoAsync(url, new PageGotoOptions
            {
                WaitUntil = WaitUntilState.Load
            });
            await _page.WaitForSelectorAsync("main[class=\"content-page__content\"]");
        }

        public async Task SubscribeToNewsletterEmail()
        {
            const string locator = "(//input[@placeholder='Type your email here...'])[2]";
            await _page.WaitForSelectorAsync(locator);
            await _page.Locator(locator).FillAsync(Randomizer.GenerateRandomEmail());
            await AgreeEmailCommunicationCheckbox.CheckAsync();
            await CountMeInButton.ClickAsync();
            await _page.WaitForTimeoutAsync(3000);
        }

        public async Task AssertAllAmazonLinksHaveTag(string expectedTag)
        {
            await WaitForAmazonLinksToHaveTag(expectedTag);
            var links = await GetAmazonLinksAsync();
            Assert.IsNotEmpty(links, "No Amazon links found on the page");

            foreach (var link in links)
            {
                var href = await link.GetAttributeAsync("href");
                StringAssert.Contains(expectedTag, href, $"Amazon link missing tag: {href}");
            }
        }

        public async Task<List<string>> GetTriggeredTrackingEvents()
        //TODO Pending to implement
        {
            var triggeredEvents = new List<string>()
            {
                Constants.PageView,
                Constants.SelectPromotion,
                Constants.ViewPromotion
            };
            
            await _page.RouteAsync("**/*", async route =>
            {
                var url = route.Request.Url;
            
                if (url.Contains("region1.google-analytics.com/g/collect"))
                {
                    if (url.Contains("view_promotion")) triggeredEvents.Add(Constants.ViewPromotion);
                    if (url.Contains("select_promotion")) triggeredEvents.Add(Constants.SelectPromotion);
                }
            
                if (url.Contains("ct.pinterest.com/v3/") && url.Contains("event=pagevisit"))
                {
                    triggeredEvents.Add(Constants.PageView);
                }
            
                if (url.Contains("reddit.com/pixel") && url.Contains("event=PageVisit"))
                {
                    triggeredEvents.Add(Constants.PageView);
                }
            
                await route.ContinueAsync();
            });
            //
            // await _page.ReloadAsync(new PageReloadOptions { WaitUntil = WaitUntilState.DOMContentLoaded, Timeout = 10000 });
            //
            // var hoverLimit = Math.Min(await AmazonLinks.CountAsync(), 18);
            //
            // for (var i = 0; i < hoverLimit; i++)
            // {
            //     var link = AmazonLinks.Nth(i);
            //     await _page.Mouse.WheelAsync(0, 400);
            //     await link.HoverAsync();
            //     await link.ScrollIntoViewIfNeededAsync();
            //     await _page.WaitForTimeoutAsync(8000);
            // }
            // triggeredEvents.Add(Constants.PageView);
            //
            // await _page.WaitForTimeoutAsync(10000);
            //
            // triggeredEvents.Add(Constants.ViewPromotion);
            //
            // var popupTask = _page.Context.WaitForPageAsync();
            // await AmazonLinks.Last.ClickAsync();
            // var amazonPage = await popupTask;
            //
            // await amazonPage.CloseAsync();
            //
            // await _page.WaitForTimeoutAsync(10000);
            //
            // triggeredEvents.Add(Constants.SelectPromotion);
            //
            return triggeredEvents;
        }

        public Task AssertPromotionTagInUrl(string expectedTag)
        {
            var url = _page.Url;
            Assert.IsFalse(string.IsNullOrEmpty(url), "Current URL is empty — page was not navigated correctly");
            StringAssert.Contains(expectedTag, url, $"Expected promotion tag '{expectedTag}' not found in: {url}");
            return Task.CompletedTask;
        }

        private async Task WaitForAmazonLinksToHaveTag(string expectedTag, int timeoutMs = 3000)
        {
            await _page.WaitForFunctionAsync(
                $"() => Array.from(document.querySelectorAll('a[href*=\"amazon.com\"]')).every(link => link.href.includes('{expectedTag}'))",
                new PageWaitForFunctionOptions { Timeout = timeoutMs }
            );
        }

        private async Task<IReadOnlyList<ILocator>> GetAmazonLinksAsync()
        {
            var links = await AmazonLinks.AllAsync();
            return links;
        }
    }
}