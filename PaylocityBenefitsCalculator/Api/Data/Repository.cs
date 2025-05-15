namespace Api.Data;

/// <summary>
/// This class simulates a repository in a data access layer
/// Ideally I would separate employee and dependent in their own repo classes but here I decided to simplify
/// </summary>
public class Repository : IRepository
{
    public IEnumerable<Employee> GetEmployees()
    {
        return new List<Employee>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = GetDependentsByEmployeeId(2)
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = GetDependentsByEmployeeId(3)
            }
        };
    }

    public IEnumerable<Dependent> GetDependents()
    {
        return new List<Dependent>()
        {
            new()
            {
                Id = 1,
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3),
                EmployeeId = 2
            },
            new()
            {
                Id = 2,
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23),
                EmployeeId = 2
            },
            new()
            {
                Id = 3,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18),
                EmployeeId = 2
            },
            new()
            {
                Id = 4,
                FirstName = "DP",
                LastName = "Jordan",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2),
                EmployeeId = 3
            }
        };
    }

    public Employee? GetEmployeeById(int id)
    {
        return GetEmployees().Where(x => x.Id == id).SingleOrDefault();
    }

    public Dependent? GetDependentById(int id)
    {
        return GetDependents().Where(x => x.Id == id).SingleOrDefault();
    }

    public ICollection<Dependent> GetDependentsByEmployeeId(int employeeId)
    {
        var dependents = GetDependents().Where(x => x.EmployeeId == employeeId).ToList();

        // Check for multiple partners
        // This implementation will break the whole thing if there's "data inconsistence"
        // Ideally we would check for multiple partners or spouses in the data entry
        var multiplePartnersOrSpouses = dependents.Count(d => d.Relationship == Relationship.DomesticPartner
                                                              || d.Relationship == Relationship.Spouse);
        if (multiplePartnersOrSpouses > 1)
        {
            throw new Exception($"Employee ID '{employeeId}' has multiple partners");
        }

        return GetDependents().Where(x => x.EmployeeId == employeeId).ToList();
    }
}
