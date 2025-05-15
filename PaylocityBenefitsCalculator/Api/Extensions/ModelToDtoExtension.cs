namespace Api.Extensions;

/// <summary>
/// A set of extensions to transform the data models into DTO
/// Employee and dependent extensions can be separated in their own classes but I simplified
/// </summary>
public static class ModelToDtoExtension
{
    public static GetEmployeeDto MapToDto(this Employee? employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        return new GetEmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Salary = employee.Salary,
            DateOfBirth = employee.DateOfBirth,
            Dependents = employee.Dependents.MapToDto()
        };
    }

    public static GetDependentDto MapToDto(this Dependent? dependent)
    {
        if (dependent == null)
        {
            throw new ArgumentNullException(nameof(dependent));
        }

        return new GetDependentDto
        {
            Id = dependent.Id,
            FirstName = dependent.FirstName,
            LastName = dependent.LastName,
            DateOfBirth = dependent.DateOfBirth,
            Relationship = dependent.Relationship
        };
    }

    public static ICollection<GetEmployeeDto> MapToDto(this IEnumerable<Employee> employees)
    {
        return employees.Select(employee => employee.MapToDto()).ToList();
    }

    public static ICollection<GetDependentDto> MapToDto(this IEnumerable<Dependent> dependents)
    {
        return dependents.Select(dependent => dependent.MapToDto()).ToList();
    }
}
