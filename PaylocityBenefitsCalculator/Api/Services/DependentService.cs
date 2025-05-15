namespace Api.Services;

public class DependentService : IDependentService
{
    IRepository repository;

    public DependentService(IRepository repo)
    {
        repository = repo;
    }

    public IEnumerable<GetDependentDto> GetDependents()
    {
        return repository.GetDependents().MapToDto();
    }

    public GetDependentDto GetDependentById(int id)
    {
        return repository.GetDependentById(id).MapToDto();
    }
}
