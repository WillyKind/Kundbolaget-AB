using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.ViewModels
{
    public class ManageProductInfosViewModel
    {
        public ProductInfo ProductInfo { get; set; }
        public Container[] Containers { get; set; }
        public ProductGroup[] ProductGroups { get; set; }
        public Volume[] Volumes { get; set; }
    }
}