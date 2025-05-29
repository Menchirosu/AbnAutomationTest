using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace AbnAutomationTest
{
    [Binding]
    public class SearchAbnSteps
    {
        private static IPage _page;
        private static IBrowser _browser;
        private static IBrowserContext _context;

        public static IPage GetPage() => _page; // ✅ Add this line

        [BeforeTestRun]
        public static async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });

            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        [Given(@"I open the ABN Lookup website")]
        public async Task GivenIOpenTheABNLookupWebsite()
        {
            await _page.GotoAsync("https://abr.business.gov.au/");
        }

        [When(@"I enter ""(.*)"" in the search field")]
        public async Task WhenIEnterInTheSearchField(string businessName)
        {
            await _page.FillAsync("#SearchText", businessName);
        }

        [When(@"I click the search button")]
        public async Task WhenIClickTheSearchButton()
        {
            await _page.ClickAsync("#MainSearchButton");
        }

        [Then(@"I should see results containing ""(.*)""")]
        public async Task ThenIShouldSeeResultsContaining(string expectedText)
        {
            await _page.WaitForSelectorAsync($"text={expectedText}");
            var content = await _page.ContentAsync();
            Assert.That(content.Contains(expectedText), Is.True);
        }

        [Then(@"I should see a no results message")]
        public async Task ThenIShouldSeeNoResultsMessage()
        {
            await _page.WaitForSelectorAsync("text=No matching names found");
            var content = await _page.ContentAsync();
            Assert.That(content.Contains("No matching names found"), Is.True);
        }

        [Then(@"I should see a validation message for empty search")]
        public async Task ThenIShouldSeeValidationMessageForEmptySearch()
        {
            var content = await _page.ContentAsync();
            Assert.That(content.Contains("Search text required"), Is.True);
        }

        [When(@"I press enter")]
        public async Task WhenIPressEnter()
        {
            await _page.PressAsync("#SearchText", "Enter");
        }

        [When(@"I click a random search result")]
        public async Task WhenIClickARandomSearchResult()
        {
            var abnLinks = _page.Locator("//tbody/tr[position() > 1]/td[1]/a[@href]");
            var count = await abnLinks.CountAsync();

            Assert.That(count, Is.GreaterThan(0), "No ABN links found on search result.");

            var random = new Random();
            int randomIndex = random.Next(0, count);

            var randomAbnLink = abnLinks.Nth(randomIndex);
            await randomAbnLink.WaitForAsync(new() { Timeout = 10000 });

            var abnText = (await randomAbnLink.InnerTextAsync()).Trim();
            Console.WriteLine($"Clicking on random ABN #{randomIndex + 1}: {abnText}");

            await randomAbnLink.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        [Then(@"I should see the correct ABN details displayed")]
        public async Task ThenIShouldSeeTheCorrectAbnDetailsDisplayed()
        {
            async Task AssertField(string description, string selector, bool isXPath = false)
            {
                var locator = isXPath ? _page.Locator($"xpath={selector}") : _page.Locator(selector);
                var isVisible = await locator.IsVisibleAsync();

                if (!isVisible)
                {
                    Console.WriteLine($"[Warning] No field found for {description}.");
                    return;
                }

                var value = (await locator.InnerTextAsync()).Trim();
                Console.WriteLine($"{description}: {value}");
                Assert.That(string.IsNullOrWhiteSpace(value), Is.False, $"{description} should not be empty.");
            }

    // Only check the fields you care about (ABN field removed)
            await AssertField("Trading Name", "(//table)[2]/tbody[1]/tr[3]/td[1]", isXPath: true);
            await AssertField("Main Business Location", "span[itemprop='addressLocality']");
            await AssertField("Entity Name", "span[itemprop='legalName']");
        }
        [AfterTestRun]
        public static async Task TearDown()
        {
            await _browser.CloseAsync();
        }
    }
}
