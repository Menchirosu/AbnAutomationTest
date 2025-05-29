
# ABN Lookup Automation Test

This project automates the testing of the ABN Lookup website using SpecFlow, NUnit, and Playwright.

## Project Overview

The tests cover searching for a business by name on the ABN Lookup website and verifying that results are displayed correctly.

## Technologies Used

- [.NET 9.0](https://dotnet.microsoft.com/en-us/)
- [SpecFlow 3.9.74](https://specflow.org/)
- [NUnit 4.3.2](https://nunit.org/)
- [Microsoft Playwright 1.52.0](https://playwright.dev/dotnet/)
- NUnit3TestAdapter 5.0.0 (for test discovery and running tests in Visual Studio)

## Prerequisites

- .NET 9.0 SDK installed
- Node.js installed (required by Playwright)
- Visual Studio Code or another IDE

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```
2. Navigate to the project folder:
   ```bash
   cd AbnAutomationTest/AbnAutomationTest
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Install Playwright browsers:
   ```bash
   playwright install
   ```

## Running Tests

To run the tests, use the command:

```bash
dotnet test
```

You can also run tests within Visual Studio Code by using the Test Explorer or the integrated terminal.

## Notes

- Tests are written using SpecFlow with NUnit as the test runner.
- Playwright is used for browser automation.
- You might see warnings about non-nullable fields if you haven’t initialized some private fields in constructors — these can be addressed as needed.

## Contact

For questions or issues, please contact [Your Name or Email].
