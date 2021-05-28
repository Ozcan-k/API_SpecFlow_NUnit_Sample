Feature: FileUpload
	
	Background: 
Given I get JWT authentication of User with following details
    | Email             | Password      |
    | Test@gmail.com | Just4Today!    |


Scenario: Test file upload functionality
	Given I perform POST operation for "/uploads" 
	Then I see the file is being uploaded with response as OK
