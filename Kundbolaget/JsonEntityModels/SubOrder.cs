using System.Collections.Generic;

namespace Kundbolaget.JsonEntityModels
{
    public class SubOrder
    {
        public int deliverTo { get; set; }
        public string deliverDate { get; set; }
        public List<OrderRow> orderedProducts { get; set; }
    }
}