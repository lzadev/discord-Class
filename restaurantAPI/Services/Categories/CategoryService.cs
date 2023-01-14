using System.Linq.Expressions;
using AutoMapper;
using RestaurantAPI.Entites;
using RestaurantAPI.Repositories.CategoryRepo;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public CategoryDto Create(CreateCategoryDto model)
    {
        if (model.Name is null) throw new ValidationException("The name of the category must be provide");

        var newCategory = _mapper.Map<Category>(model);
        newCategory.IsActive = true;
        newCategory.CreationTime = DateTimeOffset.UtcNow;

        _categoryRepository.Create(newCategory);
        _categoryRepository.SaveChages();

        return _mapper.Map<CategoryDto>(newCategory);
    }

    public void Delete(int id)
    {
        var result = _categoryRepository.GetById(id);

        if (result is null) throw new NotFoundException($"Category with id {id} was not found ");

        result.IsDelete = true;
        result.DeletionTime = DateTimeOffset.UtcNow;

        _categoryRepository.Delete(result);
        _categoryRepository.SaveChages();
    }

    public IEnumerable<CategoryDto> GetAll(CategoryInputFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.Name))
        {
            Expression<Func<Category, bool>> expression = x => x.Name.ToLower().Contains(filter.Name.ToLower());
            var result = _categoryRepository.GetWhere(expression);
            return _mapper.Map<IEnumerable<CategoryDto>>(result);
        }

        return _mapper.Map<IEnumerable<CategoryDto>>(_categoryRepository.GetAll());
    }

    public CategoryDto GetById(int id)
    {
        var category = _categoryRepository.GetById(id);

        if (category is null) throw new NotFoundException($"Category with id {id} was not found ");

        return _mapper.Map<CategoryDto>(category);
    }

    public CategoryDto Update(int id, UpdateCategoryDto model)
    {
        if (model.Id <= 0) throw new ValidationException($"Category with id {model.Id} was not found ");
        if (model.Id != id) throw new ValidationException($"You must specify the same id in the url and in the body");
        if (string.IsNullOrEmpty(model.Name)) throw new ValidationException($"You must specify a name");

        var category = _categoryRepository.GetById(model.Id);

        if (category is null) throw new ValidationException($"Category with id {model.Id} was not found ");

        var categoryUpdated = _mapper.Map(model, category);
        categoryUpdated.IsModified = true;
        categoryUpdated.ModificationTime = DateTimeOffset.UtcNow;

        _categoryRepository.Update(categoryUpdated);
        _categoryRepository.SaveChages();

        return _mapper.Map<CategoryDto>(categoryUpdated);
    }
}