using CallCenterEmulation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenterEmulation.Data
{
    public class CallCenterContext : DbContext
    {

        public CallCenterContext(DbContextOptions<CallCenterContext> options)
           : base(options)
        { }

        public DbSet<Operator> Operators { get; set; }
        public DbSet<EmployeeStatus> EmployeeStatus { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }


        //public CallCenterContext()
        //{
        //}

        //public CallCenterContext() : base(options) { }

        //public static CallCenterContext Create()
        //{
        //    return new CallCenterContext();
        //}
    }
}
