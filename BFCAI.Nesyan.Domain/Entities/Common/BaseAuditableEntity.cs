using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Domain.Entities.Common
{
    public class BaseAuditableEntity<TKey>:BaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } 
        public string? LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } 
    }
}
