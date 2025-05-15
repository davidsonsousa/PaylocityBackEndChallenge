namespace Api.Helper;

/// <summary>
/// I decided to have a helper to do all the calculation with hardcoded values for simplicity
/// The ideal scenario is to have the values being easily configured somewhere (DB, settings file) and being used here
/// We could also have a "paycheck calculator" interface with different implementations, according to different calculation scenarios
/// </summary>
public static class CalculatePaycheckHelper
{
    private const decimal BenefitBaseCost = 1000m;
    private const decimal BenefitDependentCost = 600m;
    private const decimal HighEarnerThreshold = 80000m;
    private const decimal HighEarnerAdditionalRate = 0.02m;
    private const decimal AdditionalDependentCostPerMonth = 200m;
    private const int PaychecksPerYear = 26;

    public static PaycheckDto CalculatePaycheck(GetEmployeeDto employee)
    {
        // Calculate yearly benefit costs
        decimal yearlyBenefitCost = BenefitBaseCost * 12;

        yearlyBenefitCost = ProcessDependentBenefits(employee, yearlyBenefitCost);
        yearlyBenefitCost = ApplyAdditionalCostAboveThreshold(employee, yearlyBenefitCost);
        var distributedBenefits = DistributeBenefitsAcrossPaychecks(employee, yearlyBenefitCost);

        return new PaycheckDto
        {
            GrossSalary = Math.Round(distributedBenefits.PerPaycheckSalary, 2),
            BenefitDeductions = Math.Round(distributedBenefits.PerPaycheckDeduction, 2),
            NetSalary = Math.Round(distributedBenefits.NetSalary, 2)
        };
    }

    private static decimal ProcessDependentBenefits(GetEmployeeDto employee, decimal yearlyBenefitCost)
    {
        if (employee.Dependents != null)
        {
            foreach (var dependent in employee.Dependents)
            {
                yearlyBenefitCost += BenefitDependentCost * 12;

                // Apply additional cost for dependents over 50 years old
                if (DateTime.Now.Year - dependent.DateOfBirth.Year > 50)
                {
                    yearlyBenefitCost += AdditionalDependentCostPerMonth * 12;
                }
            }
        }

        return yearlyBenefitCost;
    }

    private static decimal ApplyAdditionalCostAboveThreshold(GetEmployeeDto employee, decimal yearlyBenefitCost)
    {
        if (employee.Salary > HighEarnerThreshold)
        {
            yearlyBenefitCost += employee.Salary * HighEarnerAdditionalRate;
        }

        return yearlyBenefitCost;
    }

    private static (decimal PerPaycheckDeduction, decimal PerPaycheckSalary, decimal NetSalary)
        DistributeBenefitsAcrossPaychecks(GetEmployeeDto employee, decimal yearlyBenefitCost)
    {
        var perPaycheckDeduction = yearlyBenefitCost / PaychecksPerYear;
        var perPaycheckSalary = employee.Salary / PaychecksPerYear;
        var netSalary = perPaycheckSalary - perPaycheckDeduction;

        return (perPaycheckDeduction, perPaycheckSalary, netSalary);
    }
}
