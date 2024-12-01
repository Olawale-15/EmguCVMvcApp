namespace EmguCVMvcApp.Models
{
    public class ImageProcessingModel
    {
        public IFormFile UploadedImage { get; set; }
        public string ProcessedImagePath { get; set; } = default!;
    }
}
