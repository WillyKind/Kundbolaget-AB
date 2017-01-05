using System.ComponentModel.DataAnnotations;
using System.Web;
using Kundbolaget.JsonEntityModels;

namespace Kundbolaget.ViewModels
{
    public class FileUploadViewModel
    {
        [Required(ErrorMessage = "Du måste välja en fil"), FileExtensions(ErrorMessage = "Ogiltig fil")]
        public HttpPostedFileBase File { get; set; }

        public OrderFile OrderFile { get; set; }
    }
}