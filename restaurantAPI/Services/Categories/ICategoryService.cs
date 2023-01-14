using RestaurantAPI.Entites;

public interface ICategoryService
{
    IEnumerable<CategoryDto> GetAll(CategoryInputFilter filter);
    CategoryDto GetById(int id);
    CategoryDto Create(CreateCategoryDto model);
    CategoryDto Update(int id, UpdateCategoryDto model);
    void Delete(int id);
}