using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Kundbolaget.ViewModels
{
    public class FileUploadVM
    {
        [Required, FileExtensions(ErrorMessage = "Ogiltig fil")]
        public HttpPostedFileBase File { get; set; }
    }
}