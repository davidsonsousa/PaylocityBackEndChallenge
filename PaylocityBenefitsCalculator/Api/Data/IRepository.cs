namespace Api.Data;

public interface IRepository
{
    IEnumerable<Employee> GetEmployees();

    IEnumerable<Dependent> GetDependents();

    Employee? GetEmployeeById(int id);

    Dependent? GetDependentById(int id);

    ICollection<Dependent> GetDependentsByEmployeeId(int employeeId);
}
