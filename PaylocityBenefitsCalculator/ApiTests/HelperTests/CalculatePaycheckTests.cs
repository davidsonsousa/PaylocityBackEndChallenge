namespace ApiTests.HelperTests;

/// <summary>
/// These tests should cover the paycheck requirements
/// They were broke down in different scenarios to make the calculations easier to visualize
/// </summary>
public class PaycheckCalculatorTests
{
    private const int PaychecksPerYear = 26;

    [Fact]
    public void CalculatePaycheck_EmployeeWithoutDependents_ReturnsCorrectNetSalary()
    {
        var employee = new GetEmployeeDto
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Salary = 56431.99m,
            DateOfBirth = new DateTime(1984, 12, 30)
        };

        var paycheck = CalculatePaycheckHelper.CalculatePaycheck(employee);

        // Expected results
        decimal yearlyBenefitCost = 1000m * 12;
        decimal grossSalary = PrepareGrossSalary(employee.Salary);
        decimal perPaycheckDeduction = PreparePaycheckDeduction(yearlyBenefitCost);
        decimal netSalary = PrepareNetSalary(grossSalary, perPaycheckDeduction);

        // Assertions
        Assert.NotNull(paycheck);
        Assert.Equal(grossSalary, paycheck.GrossSalary, 2);
        Assert.Equal(perPaycheckDeduction, paycheck.BenefitDeductions, 2);
        Assert.Equal(netSalary, paycheck.NetSalary, 2);
    }

    [Fact]
    public void CalculatePaycheck_EmployeeWithDependents_ReturnsCorrectNetSalary()
    {
        var employee = new GetEmployeeDto
        {
            Id = 2,
            FirstName = "John",
            LastName = "Doe",
            Salary = 56431.99m,
            DateOfBirth = new DateTime(1999, 8, 10),
            Dependents = new List<GetDependentDto>
            {
                new GetDependentDto { Id = 1, FirstName = "Spouse", LastName = "Morant", Relationship = Relationship.Spouse, DateOfBirth = new DateTime(1998, 3, 3) },
                new GetDependentDto { Id = 2, FirstName = "Child1", LastName = "Morant", Relationship = Relationship.Child, DateOfBirth = new DateTime(2020, 6, 23) }
            }
        };

        var paycheck = CalculatePaycheckHelper.CalculatePaycheck(employee);

        // Expected results
        decimal yearlyBenefitCost = 1000m * 12 + (600m * 12 * employee.Dependents.Count);
        decimal grossSalary = PrepareGrossSalary(employee.Salary);
        decimal perPaycheckDeduction = PreparePaycheckDeduction(yearlyBenefitCost);
        decimal netSalary = PrepareNetSalary(grossSalary, perPaycheckDeduction);

        // Assertions
        Assert.NotNull(paycheck);
        Assert.Equal(grossSalary, paycheck.GrossSalary, 2);
        Assert.Equal(perPaycheckDeduction, paycheck.BenefitDeductions, 2);
        Assert.Equal(netSalary, paycheck.NetSalary, 2);
    }

    [Fact]
    public void CalculatePaycheck_HighEarner_ReturnsCorrectDeduction()
    {
        var employee = new GetEmployeeDto
        {
            Id = 3,
            FirstName = "John",
            LastName = "Doe",
            Salary = 98715.54m,
            DateOfBirth = new DateTime(1963, 2, 17)
        };

        var paycheck = CalculatePaycheckHelper.CalculatePaycheck(employee);

        // Expected results
        decimal yearlyBenefitCost = 1000m * 12 + (employee.Salary * 0.02m);
        decimal grossSalary = PrepareGrossSalary(employee.Salary);
        decimal perPaycheckDeduction = PreparePaycheckDeduction(yearlyBenefitCost);
        decimal netSalary = PrepareNetSalary(grossSalary, perPaycheckDeduction);

        // Assertions
        Assert.NotNull(paycheck);
        Assert.Equal(grossSalary, paycheck.GrossSalary, 2);
        Assert.Equal(perPaycheckDeduction, paycheck.BenefitDeductions, 2);
        Assert.Equal(netSalary, paycheck.NetSalary, 2);
    }

    [Fact]
    public void CalculatePaycheck_EmployeeWithDependents_HighEarner_ReturnsCorrectPaycheck()
    {
        var employee = new GetEmployeeDto
        {
            Id = 2,
            FirstName = "John",
            LastName = "Doe",
            Salary = 154986.22m,
            DateOfBirth = new DateTime(1999, 8, 10),
            Dependents = new List<GetDependentDto>
            {
                new GetDependentDto { Id = 1, FirstName = "Spouse", LastName = "Morant", Relationship = Relationship.Spouse, DateOfBirth = new DateTime(1998, 3, 3) },
                new GetDependentDto { Id = 2, FirstName = "Child1", LastName = "Morant", Relationship = Relationship.Child, DateOfBirth = new DateTime(2020, 6, 23) }
            }
        };

        var paycheck = CalculatePaycheckHelper.CalculatePaycheck(employee);

        // Expected results
        decimal yearlyBenefitCost = 1000m * 12 + (600m * 12 * employee.Dependents.Count) + (employee.Salary * 0.02m);
        decimal grossSalary = PrepareGrossSalary(employee.Salary);
        decimal perPaycheckDeduction = PreparePaycheckDeduction(yearlyBenefitCost);
        decimal netSalary = PrepareNetSalary(grossSalary, perPaycheckDeduction);

        // Assertions
        Assert.NotNull(paycheck);
        Assert.Equal(grossSalary, paycheck.GrossSalary, 2);
        Assert.Equal(perPaycheckDeduction, paycheck.BenefitDeductions, 2);
        Assert.Equal(netSalary, paycheck.NetSalary, 2);
    }

    private decimal PrepareGrossSalary(decimal employeeSalary)
    {
        return employeeSalary / PaychecksPerYear;
    }

    private decimal PreparePaycheckDeduction(decimal yearlyBenefitCost)
    {
        return yearlyBenefitCost / PaychecksPerYear;
    }

    private decimal PrepareNetSalary(decimal grossSalary, decimal deductions)
    {
        return grossSalary - deductions;
    }
}