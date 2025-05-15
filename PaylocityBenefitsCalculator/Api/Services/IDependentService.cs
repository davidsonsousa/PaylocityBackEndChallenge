namespace Api.Services;

public interface IDependentService
{
    IEnumerable<GetDependentDto> GetDependents();

    GetDependentDto GetDependentById(int id);
}
