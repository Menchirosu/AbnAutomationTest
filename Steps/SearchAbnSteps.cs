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

        [BeforeTestRun]
        public static async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true,
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

        [When(@"I click the random search result")]
        public async Task WhenIClickTheRandomSearchResult()
        {
            var firstAbnLink = _page.Locator("//div[@id='content-matching']//a").First;
            await firstAbnLink.WaitForAsync(new() { Timeout = 10000 });
            var abn = await firstAbnLink.InnerTextAsync();
            Console.WriteLine($"Clicking on ABN: {abn.Trim()}");
            await firstAbnLink.ClickAsync();
        }

        [Then(@"I should see a non-empty entity name")]
        public async Task ThenIShouldSeeANonEmptyEntityName()
        {
            var entityNameLocator = _page.Locator("span[itemprop='legalName']");
            await entityNameLocator.WaitForAsync();
            var actualEntityName = await entityNameLocator.InnerTextAsync();
            Console.WriteLine($"Entity Name: {actualEntityName}");

            Assert.That(string.IsNullOrWhiteSpace(actualEntityName), Is.False, "Entity name should not be empty or whitespace.");
        }
        
        // [AfterScenario]
        // public async Task PauseAfterScenario()
        // {
        //     await Task.Delay(2000);
        // }

        [AfterTestRun]
        public static async Task TearDown()
        {
            await _browser.CloseAsync();
        }
    }
}
