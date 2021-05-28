Feature: PostProfile
	Test POST operation using REST libarray

Background: 
Given I get JWT authentication of User with following details
    | Email             | Password      |
    | admin@wgtcorp.com | Just4Today!    |


Scenario: Verify Post operation for Profile
	Given I perform operation for "/post/{profileNo}/profile" with body
	| name | profile |
	| Sams | 2       |	
	Then I should see the "name" name as "Sams"

