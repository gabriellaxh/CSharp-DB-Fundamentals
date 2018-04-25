using System;
using System.Collections.Generic;

namespace EmployeeFullInformation
{

    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employees>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }

        public Employees Manager { get; set; }
        public ICollection<Employees> Employees { get; set; }
    }
}
