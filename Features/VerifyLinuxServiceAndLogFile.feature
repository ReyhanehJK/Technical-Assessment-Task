Feature: Verify Linux Service and Log File

Scenario: Verify my-service and log file existence
    Given I connect to the Linux VM using SSH
    When I check if "my-service" is running
    And I verify the existence of log file "myservice.log"
    Then the service should be running and the log file should exist
