using System.Collections.Generic;

namespace Kundbolaget.JsonEntityModels
{
    public class OrderFile
    {
        public string companyId { get; set; }
        public int customerOrderFileId { get; set; }
        public List<SubOrder> orders { get; set; }
    }
}