using Data.Contracts;
using Data.Repositories.Models;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WebFramework.Filters;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiResultFilter]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<List<CategoryDto>> Get()
        {
            var category = _categoryRepository.TableNoTracking
               .Select(x => new CategoryDto
               {
                   Name = x.Name,
                   Description = x.Description,
                   Books = x.BookCategories.Select(x => x.Book.Title).ToList()
               }).ToList();
            return category;
        }

        [HttpPost]
        public async Task<ApiResult<Category>> Add(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };
            await _categoryRepository.Add(category, cancellationToken);
            return category;
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResult> Update(int id, Category category, CancellationToken cancellationToken)
        {
            var updateCategory = await _categoryRepository.GetByIdAsync(cancellationToken, id);
            updateCategory.Name = category.Name;
            updateCategory.Description = category.Description;

            await _categoryRepository.UpdateAsync(updateCategory, cancellationToken);
            return Ok();
        }

    }
}
