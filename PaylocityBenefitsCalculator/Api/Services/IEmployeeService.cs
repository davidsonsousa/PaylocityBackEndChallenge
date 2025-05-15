namespace Api.Services;

public interface IEmployeeService
{
    IEnumerable<GetEmployeeDto> GetEmployees();

    GetEmployeeDto GetEmployeeById(int id);
}
