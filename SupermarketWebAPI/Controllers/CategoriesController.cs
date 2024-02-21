using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupermarketWebAPI.Domain.Models;
using SupermarketWebAPI.Domain.Services;
using SupermarketWebAPI.Resources;

namespace SupermarketWebAPI.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

      
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryResource>), 200)]
        public async Task<IEnumerable<CategoryResource>> ListAsync()
        {
            var categories = await _categoryService.ListAsync();
            return _mapper.Map<IEnumerable<CategoryResource>>(categories);
        }

       
        [HttpPost]
        [ProducesResponseType(typeof(CategoryResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
        {
            var category = _mapper.Map<Category>(resource);
            var result = await _categoryService.SaveAsync(category);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message!));
            }

            var categoryResource = _mapper.Map<CategoryResource>(result.Resource!);
            return Ok(categoryResource);
        }

  
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategoryResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
        {
            var category = _mapper.Map<Category>(resource);
            var result = await _categoryService.UpdateAsync(id, category);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message!));
            }

            var categoryResource = _mapper.Map<CategoryResource>(result.Resource!);
            return Ok(categoryResource);
        }

    
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CategoryResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message!));
            }

            var categoryResource = _mapper.Map<CategoryResource>(result.Resource!);
            return Ok(categoryResource);
        }
    }

}
