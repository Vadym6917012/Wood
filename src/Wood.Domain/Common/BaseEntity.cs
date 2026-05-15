using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wood.Domain.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }

    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
