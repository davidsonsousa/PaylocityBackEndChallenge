# Notes and explanations
- Upgraded project to .NET 8 without major changes (if any)
  - Reason: .NET 6 support ended last year and I wanted to use the latest LTS ([more info](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core#lifecycle)).
- The Controllers are using try/catch for exception handling
  - Reason: Keeping things simple. In the real world I would approach differently by creating an _ExceptionMiddleware_ class to handle all errors in the API and return the appropriated response. I'd also create some custom exception classes such as _EmployeeNotFoundException_.
- Multiple partners and spouses should be validated during data entry (verifying if the employee already has a partiner during registration) instead of when loading the data.
- The paycheck data is returned with the employee DTO
  - Reason: I wanted to return the paycheck data with its respective employee and also save some time instead of creating a new controller to return it individually.
- For further explanations please refer to the code. I tried to have as much summary comments as possible to explain what is happening and some of my reasoning.