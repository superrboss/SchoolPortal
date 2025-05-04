using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.Interface
{
    public interface IAuditable
    {
        string? CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string? UpdatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
