using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceShared.Service;
using Web.Models.Catalog;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ISharedIdentityService _identityService;
        private readonly ICatalogService _catalogService;

        public CourseController(ISharedIdentityService identityService, ICatalogService catalogService)
        {
            _identityService = identityService;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _catalogService.GetAllCourseByUserIdAsync(_identityService.UserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();

            ViewBag.categoryList = new SelectList(categories,"Id","Name");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            courseCreateInput.UserId = _identityService.UserId;
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
             
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            await _catalogService.CreateCourseAsync(courseCreateInput);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update( string Id)
        {
            var course = await _catalogService.GetByCourseId(Id);
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name",course.Id);
            if (course==null)
            {
                RedirectToAction(nameof(Index));
            }
            
                CourseUpdateInput courseUpdateInput = new CourseUpdateInput()
                {
                    Id = course.Id,
                    Name = course.Name,
                    Price = course.Price,
                    Description = course.Description,
                    Feature = course.Feature,
                    CategorId=course.CategorId,
                    UserId=course.UserId,
                    Picture=course.Picture,

                };
            return View(courseUpdateInput);
            
           

        }
        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _catalogService.UpdateCourseAsync(courseUpdateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string Id)
        {
            await _catalogService.DeleteCourseAsync(Id);
            return RedirectToAction(nameof(Index));
        }



    }
}
