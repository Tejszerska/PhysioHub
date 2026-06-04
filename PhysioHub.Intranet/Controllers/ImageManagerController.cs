using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace PhysioHub.Intranet.Controllers
{
    public class ImageManagerController : Controller
    {
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile uploadedFile, string category, int entityId)
        {
            if (uploadedFile == null || uploadedFile.Length == 0)
            {
                ViewBag.Error = "Please select a file to upload.";
                return View();
            }

            if (Path.GetExtension(uploadedFile.FileName).ToLower() != ".jpg")
            {
                ViewBag.Error = "Only .jpg files are allowed.";
                return View();
            }

            string folderName = "";
            string fileName = "";

            if (category == "Therapist")
            {
                folderName = "therapists";
                fileName = $"portrait-{entityId}.jpg";
            }
            else if (category == "Article")
            {
                folderName = "articles";
                fileName = $"article-{entityId}.jpg";
            }
            else
            {
                ViewBag.Error = "Invalid category selected.";
                return View();
            }

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "PhysioHub.Web", "wwwroot", "img", folderName);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string fullPath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(stream);
            }

            ViewBag.Success = $"Image {fileName} uploaded successfully!";
            return View();
        }
    }
}