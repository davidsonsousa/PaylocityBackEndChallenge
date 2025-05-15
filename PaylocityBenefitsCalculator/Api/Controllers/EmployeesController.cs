namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    IEmployeeService employeeService;

    public EmployeesController(IEmployeeService empService)
    {
        employeeService = empService;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            var employee = employeeService.GetEmployeeById(id);

            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = employee,
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
            return BadRequest(new ApiResponse<GetEmployeeDto>
            {
                Data = null,
                Success = false,
                Error = ex.Message
            });
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach

        try
        {
            var employees = employeeService.GetEmployees();

            var result = new ApiResponse<List<GetEmployeeDto>>
            {
                Data = employees.ToList(),
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
            return BadRequest(new ApiResponse<GetEmployeeDto>
            {
                Data = null,
                Success = false,
                Error = ex.Message
            });
        }
    }
}
