using Microsoft.AspNetCore.Mvc;
namespace restaurantAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly ICategoryService _categoryService;

    public CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [HttpGet(Name = "Catgories")]
    public ApiResponse GetAll([FromQuery] CategoryInputFilter filter)
    {
        try
        {
            _logger.LogInformation("Getting All Categories");
            return new ApiResponse(statusCode: StatusCodes.Status200OK, data: _categoryService.GetAll(filter));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ApiResponse(StatusCodes.Status500InternalServerError, null, "Ha ocurrido un error. Intentalo más tarde", false);
        }
    }

    [HttpPost(Name = "Catgories")]
    public ApiResponse Create(CreateCategoryDto model)
    {
        try
        {
            _logger.LogInformation("Creating a category");
            return new ApiResponse(statusCode: StatusCodes.Status200OK, data: _categoryService.Create(model));
        }
        catch (Exception ex) when (ex is ValidationException)
        {
            _logger.LogError(ex.Message);
            return new ApiResponse(StatusCodes.Status400BadRequest, null, ex.Message, false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ApiResponse(StatusCodes.Status500InternalServerError, null, "Ha ocurrido un error. Intentalo más tarde", false);
        }
    }

    [HttpPut("{id:int}")]
    public ApiResponse Update(int id, UpdateCategoryDto model)
    {
        try
        {
            _logger.LogInformation("Updating a category");
            return new ApiResponse(statusCode: StatusCodes.Status200OK, data: _categoryService.Update(id, model));
        }
        catch (Exception ex) when (ex is ValidationException || ex is NotFoundException)
        {
            _logger.LogError(ex.Message);
            return new ApiResponse(StatusCodes.Status400BadRequest, null, ex.Message, false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ApiResponse(StatusCodes.Status500InternalServerError, null, "Ha ocurrido un error. Intentalo más tarde", false);
        }
    }


    [HttpDelete("{id:int}")]
    public ApiResponse Delete(int id)
    {
        try
        {
            _logger.LogInformation("Deleting a category");
            _categoryService.Delete(id);
            return new ApiResponse(statusCode: StatusCodes.Status200OK);
        }
        catch (Exception ex) when (ex is ValidationException || ex is NotFoundException)
        {
            _logger.LogError(ex.Message);
            return new ApiResponse(StatusCodes.Status400BadRequest, null, ex.Message, false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ApiResponse(StatusCodes.Status500InternalServerError, null, "Ha ocurrido un error. Intentalo más tarde", false);
        }
    }
}
