namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    IDependentService dependentService;

    public DependentsController(IDependentService depService)
    {
        dependentService = depService;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        try
        {
            var dependent = dependentService.GetDependentById(id);

            var result = new ApiResponse<GetDependentDto>
            {
                Data = dependent,
                Success = true
            };

            return result;
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<GetDependentDto>
            {
                Data = null,
                Success = false,
                Error = ex.Message
            });
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        try
        {
            var dependents = dependentService.GetDependents();

            var result = new ApiResponse<List<GetDependentDto>>
            {
                Data = dependents.ToList(),
                Success = true
            };

            return result;
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<GetDependentDto>
            {
                Data = null,
                Success = false,
                Error = ex.Message
            });
        }
    }
}
