using System.Collections.Generic;

namespace Kundbolaget.JsonEntityModels
{
    public class SubOrder
    {
        public string deliverTo { get; set; }
        public string deliverDate { get; set; }
        public List<OrderRow> orderedProducts { get; set; }
    }
}