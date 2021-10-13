using Microsoft.VisualStudio.TestTools.UnitTesting;
using UPSAssessment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;

namespace UPSAssessment.Tests
{
    [TestClass()]
    public class EmployeeRestClientTests
    {
        EmployeeRestClient m_RestClient;

        #region Initialize

        [TestInitialize]
        public void Initialize()
        {
            m_RestClient = new EmployeeRestClient();
        }

        #endregion

        [TestMethod()]
        public void ReadEmployeesTest()
        {
            var result = m_RestClient.ReadEmployees();
            result.Count().ShouldBeGreaterThan(0);
        }

        [TestMethod()]
        public void ReadEmployeesTest1()
        {
            var result = m_RestClient.ReadEmployees(1);
            result.Count().ShouldBeGreaterThan(0);
        }

        [TestMethod()]
        public void AddNewEmployeeTest()
        {
            Employee employee = new Employee()
            {
                name = "mali",
                email = "mali@mali.com",
                gender = "female",
                status = "active"
            };

            var result=m_RestClient.AddNewEmployee(employee);

            // Because that data saved before and is not letting to create with same data
            result.ShouldBe("Unprocessable Entity");
        }
    }
}