# Tests
There are three things to consider when writing tests for this project:

1) AAA Scheme
   - All test follow the same scheme of Arrange - Act - Assert to provide a consistent construct
2) Testmethod names start with "Should_"
   - That's to enforce the method name to contain a short description of what should be tested
3) All testclasses should inherit from the TestBase class
   - The TestBase class provides some helpful properties for all test classes