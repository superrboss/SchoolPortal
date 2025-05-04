using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.Models
{
    [Owned]
    public class OfficeAssignment
    {
        public string Location { get; set; } = null!;
    }
}
