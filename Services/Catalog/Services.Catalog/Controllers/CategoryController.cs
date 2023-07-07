using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.Dtos;
using Services.Catalog.Services;
using ServiceShared.ControllerBases;

namespace Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustumBaseController
    {
        private readonly ICatagoryService _catagoryService;

        public CategoryController(ICatagoryService catagoryService)
        {
            _catagoryService = catagoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories=await _catagoryService.GetAllAsync();
            return CreateActionResultIntance(categories);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            var categories = await _catagoryService.GetByIdAsync(Id);
            return CreateActionResultIntance(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto  categoryDto)
        {
            var categories = await _catagoryService.CreateAsync(categoryDto);
            return CreateActionResultIntance(categories);
        }


    }
}
