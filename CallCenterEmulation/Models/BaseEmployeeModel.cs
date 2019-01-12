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
        public EmployeeStatus Status { get; set; }
        public EmployeeType Type { get; set; }
    }
}
