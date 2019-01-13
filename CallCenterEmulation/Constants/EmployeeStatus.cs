using CallCenterEmulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenterEmulation.Constants
{
    public enum EmployeeStatus
    {
        Busy,
        Free
    }

    public class OperatorsList
    {
        public List<Operator> operators = new List<Operator> {
            new Operator{ Id = 1, Name = "John", Type = Constants.EmployeeType.Operator, Status = Constants.EmployeeStatus.Free },
            new Operator{ Id = 2, Name = "Mark", Type = Constants.EmployeeType.Operator, Status = Constants.EmployeeStatus.Free }
    };

    }
}
