Feature: GetPosts
	Test GET posts operation with Restsharp.net
Background: 
Given I get JWT authentication of User with following details
    | Email             | Password      |
    | admin@wgtcorp.com | Just4Today!    |

Scenario: Verify author of the posts 
	Given I perform GET operation for "App/userData"
	And  Verify statuse code should be OK
	Then I should see the firstName name as Admin





	