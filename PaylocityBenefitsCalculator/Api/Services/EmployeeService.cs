namespace Api.Services;

public class EmployeeService : IEmployeeService
{
    IRepository repository;

    public EmployeeService(IRepository repo)
    {
        repository = repo;
    }

    public IEnumerable<GetEmployeeDto> GetEmployees()
    {
        var employeeDtoCollection = repository.GetEmployees().MapToDto();
        foreach (var employeeDto in employeeDtoCollection)
        {
            employeeDto.Paycheck = CalculatePaycheckHelper.CalculatePaycheck(employeeDto);
        }

        return employeeDtoCollection;
    }

    public GetEmployeeDto GetEmployeeById(int id)
    {
        var employeeDto = repository.GetEmployeeById(id).MapToDto();
        employeeDto.Paycheck = CalculatePaycheckHelper.CalculatePaycheck(employeeDto);

        return employeeDto;
    }
}
