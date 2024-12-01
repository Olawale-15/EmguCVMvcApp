using Emgu.CV;
using Microsoft.AspNetCore.Mvc;

namespace EmguCVMvcApp.Controllers
{
    public class ImageProcessingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessImage(IFormFile uploadImage)
        {
            if (uploadImage != null)
            {
                // Define the upload folder path
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload");

                // Ensure the directory exists
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Define the file path
                string filePath = Path.Combine(uploadFolder, uploadImage.FileName);

                // Save the uploaded file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    uploadImage.CopyTo(stream);
                }

                // Load the image using EmguCV and process it
                Mat image = CvInvoke.Imread(filePath);
                CvInvoke.CvtColor(image, image, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                // Define the processed image path
                string processedImagePath = Path.Combine(uploadFolder, "processed_" + uploadImage.FileName);

                // Save the processed image
                CvInvoke.Imwrite(processedImagePath, image);

                // Pass the processed image path to the view
                ViewBag.ProcessedImagePath = "/upload/processed_" + uploadImage.FileName;
            }

            return View("Index");
        }

    }
}
