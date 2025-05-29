using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace AbnAutomationTest.Steps
{
    [Binding]
    public class ButtonFunctionalitySteps
    {
        private static IPage _page => AbnAutomationTest.SearchAbnSteps.GetPage();

        [Given(@"I am on the homepage")]
        public async Task GivenIAmOnTheHomepage()
        {
            await _page.GotoAsync("https://abr.business.gov.au/");
        }

        [Then(@"I should see the following buttons enabled and clickable:")]
        public async Task ThenIShouldSeeTheFollowingButtonsEnabledAndClickable(Table table)
        {
            foreach (var row in table.Rows)
            {
                var selector = row["ButtonSelector"];
                var button = _page.Locator(selector);

                Assert.That(await button.IsVisibleAsync(), Is.True, $"Button '{selector}' is not visible.");
                Assert.That(await button.IsEnabledAsync(), Is.True, $"Button '{selector}' is not enabled.");
            }
        }

        [When(@"I click the button ""(.*)""")]
        public async Task WhenIClickTheButton(string selector)
        {
            await _page.Locator(selector).ClickAsync();

            // Wait until network is idle, so the page should be fully loaded
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        [Then(@"I should be navigated to the (.*) page")]
        public async Task ThenIShouldBeNavigatedToThePage(string expectedPage)
        {
            // Small delay to ensure navigation is settled
            await Task.Delay(500);

            var url = _page.Url.ToLower();
            Console.WriteLine($"Current URL: {url}");

            var pagePaths = new Dictionary<string, string>
            {
                { "search", "/search" },
                { "tools & resources", "/tools" },
                { "help", "/help" },
                { "lookup tools", "/tools/abnlookup" },
                { "register", "/for-business/registering-a-business-name/" },
                { "help section", "/help" },
                { "abn details", "https://www.abr.gov.au/" },  // partial domain match, no protocol
                { "web services", "/webservices" },
                { "about us", "/about" },
                { "contact us", "/contact" },
                { "legal notices", "/legal" },
                { "disclaimer", "/disclaimer" },
                { "accessibility", "/accessibility" },
                { "other languages", "abr.business.gov.au/Home/OtherLanguages" },
                { "site map", "https://abr.business.gov.au/Home/SiteMap" },
                { "home", "https://abr.business.gov.au/" }
            };

            Assert.That(pagePaths.ContainsKey(expectedPage), Is.True, $"No URL mapping found for: {expectedPage}");

            string expectedSubstring = pagePaths[expectedPage].ToLower();

            Assert.That(url, Does.Contain(expectedSubstring), $"Expected to be on '{expectedPage}' page, but was on: {url}");
        }

        [When(@"I navigate back to the homepage")]
        public async Task WhenINavigateBackToTheHomepage()
        {
            await _page.GoBackAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
    }
}
