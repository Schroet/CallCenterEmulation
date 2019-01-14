using CallCenterEmulation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenterEmulation.Models
{
    public class BaseEmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public DateTime? stop;
        public virtual EmployeeStatus Status { get; set; }
        public virtual EmployeeType Type { get; set; }
    }
}
