using GenerateQR.Helpers;
using Microsoft.AspNetCore.Mvc;
using static GenerateQR.Helpers.HomeHelper;

namespace GenerateQR.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeHelper _homeHelper;
        public HomeController(HomeHelper homeHelper)
        {
            _homeHelper = homeHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SubmitUploadAttachFile(FileModel formData)
        {
            try
            {
                string cat = "new";//new or old
                var response = await _homeHelper.SubmitUploadAttachFile(formData, cat);
                return Ok(response);
            }
            catch (ExcelException ex)
            {
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

    }
}