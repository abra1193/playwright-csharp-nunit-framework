using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Allure.Net.Commons;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using qa_automation_exercise__mejiabritoabraham.Framework.UI.Pages;
using qa_automation_exercise__mejiabritoabraham.Utils;

namespace qa_automation_exercise__mejiabritoabraham.Base.UI
{
    public abstract class UiTestBase
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;
        protected GiftPage GiftPage = null!;

        [OneTimeSetUp]
        protected async Task GlobalSetup()
        {
            _playwright = await Playwright.CreateAsync();

            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 50,
            });

            _context = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                            "AppleWebKit/537.36 (KHTML, like Gecko) " +
                            "Chrome/114.0.0.0 Safari/537.36",
                JavaScriptEnabled = true
            });

            _page = await _context.NewPageAsync();

            // Log all outgoing requests for debugging GA event calls
            _page.RequestFinished += (sender, e) =>
            {
                if (e.Url.Contains("google-analytics.com/g/collect"))
                {
                    Console.WriteLine($"GA Event Request: {e.Method} {e.Url}");
                }
            };

            await _page.GotoAsync(Constants.ApprovedGiftUrl, new PageGotoOptions
            {
                WaitUntil = WaitUntilState.Load,
                Timeout = 10000
            });

            await _page.EvaluateAsync(@"
               window._trackedEvents = [];
               window.gtag = function() {
               window._trackedEvents.push(arguments);
                console.log('gtag event:', arguments);
               };
              ");

            await _page.WaitForTimeoutAsync(6000);

            var factory = new PageFactory(_page);
        }

        [TearDown]
        public async Task TearDownAsync()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var fileName = TestContext.CurrentContext.Test.Name + ".png";
                var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var filePath = Path.Combine(baseDir!, fileName);

                await _page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = filePath
                });
                AllureApi.AddAttachment("Failure Screenshot", "image/png", filePath);
            }
        }

        [OneTimeTearDown]
        protected async Task GlobalTeardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
