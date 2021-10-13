using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPSAssessment
{
    public class EmployeeModel
    {
        public Meta Meta { get; set; }
        public List<Employee> Data { get; set; }
    }

    public class Employee
    {
        public long? id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
    }

    public class Meta
    {
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public long Total { get; set; }
        public long Pages { get; set; }
        public long Page { get; set; }
        public long Limit { get; set; }
        public Links Links { get; set; }
    }

    public class Links
    {
        public object Previous { get; set; }
        public Uri Current { get; set; }
        public Uri Next { get; set; }
    }

    public enum Gender { female, male };

    public enum Status { active, inactive };

}
