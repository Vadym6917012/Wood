using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wood.Domain.Common;

namespace Wood.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Grade {  get; set; } = string.Empty;
        public string Dimensions { get; set; } = string.Empty;
        public decimal PricePerCubicMeter { get; set; }
        public decimal PricePerPiece { get; set; }
        public string Unit { get; set; } = "м³";
        public bool InStock { get; set; } = true;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null;
    }
}
