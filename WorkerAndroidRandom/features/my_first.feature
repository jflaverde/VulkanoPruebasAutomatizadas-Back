Feature: Add an asset

  Scenario: As a valid user I can add an asset
    Given I start the app the first time
    Then I press "Assets"
    Then I press "Current Assets"
    Then I press create transaction
    Then I enter my account name

