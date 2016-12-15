using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Kundbolaget.ViewModels
{
    public class FileUploadVM
    {
        [Required(ErrorMessage = "Du måste välja en fil"), FileExtensions(ErrorMessage = "Ogiltig fil")]
        public HttpPostedFileBase File { get; set; }
    }
}