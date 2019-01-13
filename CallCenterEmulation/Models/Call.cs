using CallCenterEmulation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenterEmulation.Models
{
    public class Call
    {
        public int Id { get; set; }
        public int Length { get; set; }
        public bool IsActive { get; set; }
        public int? ManagingByUserId { get; set; }
    }
}
