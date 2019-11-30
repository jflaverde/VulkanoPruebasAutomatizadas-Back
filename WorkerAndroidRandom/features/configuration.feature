Feature: Create and configure an account

  Scenario: Create an account and enable the crash report sending
    Given I start the app and press next
    And I choose a random currency
    And I press "Next"
    And I press "Next"
    And I wait 1 second    
    And I enable the crash report sending
    And I press "Next"
    And I press "Done"
    And I wait 1 second
    And I close the new in this version pop up
    Then I see "Accounts"
  
  @reinstall
  Scenario: Create an account and disable the crash report sending
    Given I start the app and press next
    And I choose a random currency
    And I press "Next"
    And I press "Next"
    And I wait 1 second    
    And I disable the crash report sending
    And I press "Next"
    And I press "Done"
    And I wait 1 second
    And I close the new in this version pop up
    Then I see "Accounts"
