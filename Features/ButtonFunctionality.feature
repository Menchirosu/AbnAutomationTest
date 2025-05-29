Feature: Button functionality

  Scenario: Verify Search and Tools & resources buttons work correctly
    Given I am on the homepage
    Then I should see the following buttons enabled and clickable:
      | ButtonSelector                                         |
      | (//a[normalize-space()='Search'])[1]                   |
      | (//a[contains(text(),'Tools & resources')])[1]         |
      | (//a[normalize-space()='Help'])[1]                     |
      | (//a[normalize-space()='ABN Lookup tools'])[2]         |
      | (//a[normalize-space()='register a business name'])[1] |
      | (//a[normalize-space()='our help section'])[1]         |
      | (//a[normalize-space()='abr.gov.au'])[1]               |
      | (//a[normalize-space()='ABN Lookup web services'])[1]  |
      | (//a[normalize-space()='About us'])[1]                 |
      | (//a[normalize-space()='Contact us'])[1]               |
      | (//a[normalize-space()='Legal notices'])[1]            |
      | (//a[normalize-space()='Disclaimer'])[1]               |
      | (//a[normalize-space()='Accessibility'])[1]            |
      | (//a[normalize-space()='Other languages'])[1]          |
      | (//a[normalize-space()='Site map'])[1]                 |
      | (//a[normalize-space()='Home'])[1]                     |

    When I click the button "(//a[normalize-space()='Search'])[1]"
    Then I should be navigated to the search page
    When I navigate back to the homepage

    When I click the button "(//a[contains(text(),'Tools & resources')])[1]"
    Then I should be navigated to the tools & resources page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='Help'])[1]"
    Then I should be navigated to the help page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='ABN Lookup tools'])[2]"
    Then I should be navigated to the lookup tools page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='register a business name'])[1]"
    Then I should be navigated to the register page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='our help section'])[1]"
    Then I should be navigated to the help section page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='abr.gov.au'])[1]"
    Then I should be navigated to the abn details page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='ABN Lookup web services'])[1]"
    Then I should be navigated to the web services page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='About us'])[1]"
    Then I should be navigated to the about us page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='Contact us'])[1]"
    Then I should be navigated to the contact us page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='Legal notices'])[1]"
    Then I should be navigated to the legal notices page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='Disclaimer'])[1]"
    Then I should be navigated to the disclaimer page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='Accessibility'])[1]"
    Then I should be navigated to the accessibility page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='Other languages'])[1]"
    Then I should be navigated to the other languages page
    When I navigate back to the homepage

    When I click the button "(//a[normalize-space()='Site map'])[1]"
    Then I should be navigated to the site map page

    When I click the button "(//a[normalize-space()='Home'])[1]"
    Then I should be navigated to the home page
