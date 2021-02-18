# we use tags for grouping scenarios, we may assume each sceanrio as an independent test case
# feature is the folder containing similar scenarios
# if there is a tag on the feature header, this tag applies all scenarios in the feature
# using tags we can run some specific scenarios locally or on the server by Team City
# background steps run before each scenario
# the codes run in the following order - before feaure - before scenario - background steps - scenario steps - after scenario - after feature 
# Scenario Outline has an examples table, this scenario runs for each line of the table using provided data mentioned in the step "<tableHeader>"
# a step starting with Given means something is completed in the previous steps and we can assert this in the binded code
# a step starting with When requires and action in the binded code
# a step starting with Then is usually the last kine of the scnenario for asserting the expected sitiaution
# a step starting with And depends on the previos step, if previous step starts with "When"  "And" means "When", if previos step starts with "Then" "And" means "Then"
# the phrases in the feature have no binding only for explanation, to have a binding the phrases must start with Given, When, Then, And 

@regression
Feature: Google  Search
	As a user I should be able to search any string on oogle home page

Background: Landing home page

	Given I am at the google home page
	Then I should see the searh bar
		


@tc1251  
Scenario Outline: As a user I should see section headers at the home page
					
	When I search the any "<item>"
	Then I should see a result containing "<item>"
	And another assertion here

	Examples: 
	| item       |
	| cell phone |
	| mouse pad  | 
