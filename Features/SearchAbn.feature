Feature: ABN Lookup Search 

  As a user of the ABN Lookup website 
  I want to search for a business by name 
  So that I can find its ABN 
 
  Scenario: Search for Telstra 
    Given I open the ABN Lookup website 
    When I enter "Telstra" in the search field 
    And I click the search button 
    Then I should see results containing "Telstra" 

  Scenario: Search for A Company
    Given I open the ABN Lookup website
    When I enter "A Company" in the search field
    And I click the search button
    Then I should see results containing "A Company"

  Scenario: Search for a non-existent business name
    Given I open the ABN Lookup website
    When I enter "AcompanyTesltra" in the search field
    And I click the search button
    Then I should see a no results message

  Scenario: Search without entering text
    Given I open the ABN Lookup website
    When I click the search button
    Then I should see a validation message for empty search

  Scenario: Search for a business by pressing enter
    Given I open the ABN Lookup website
    When I enter "Telstra" in the search field
    And I press enter
    Then I should see results containing "Telstra"

  Scenario: Search for Telstra and validate business details
    Given I open the ABN Lookup website
    When I enter "Telstra" in the search field
    And I press enter
    Then I should see results containing "Telstra"
    When I click a random search result
    Then I should see the correct ABN details displayed




    
