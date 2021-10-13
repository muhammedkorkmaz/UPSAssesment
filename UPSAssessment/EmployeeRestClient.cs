using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// System.Net.Http and Newtonsoft.Json nuget packages are used to perform the rest service operations

namespace UPSAssessment
{
    public class EmployeeRestClient
    {
        private static readonly HttpClient m_Client = new HttpClient();
        private static readonly string m_Url = "https://gorest.co.in/public/v1/users";

        public EmployeeRestClient()
        {
            m_Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");
        }

        /// <summary>
        /// Read Employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> ReadEmployees()
        {
            try
            {
                var result = Task.Run(() => m_Client.GetStringAsync(m_Url)).Result;
                EmployeeModel model = new EmployeeModel();
                model = JsonConvert.DeserializeObject<EmployeeModel>(result);

                return model.Data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Read Employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> ReadEmployees(int pageNumber)
        {
            try
            {
                string url = string.Format("{0}?page={1}", m_Url, pageNumber);

                var result = Task.Run(() => m_Client.GetStringAsync(url)).Result;
                EmployeeModel model = new EmployeeModel();
                model = JsonConvert.DeserializeObject<EmployeeModel>(result);

                return model.Data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Read Employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> SearchEmployeesByName(string name)
        {
            try
            {
                string url = string.Format("{0}?first_name={1}", m_Url, name);

                var result = Task.Run(() => m_Client.GetStringAsync(url)).Result;
                EmployeeModel model = new EmployeeModel();
                model = JsonConvert.DeserializeObject<EmployeeModel>(result);

                return model.Data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="employee"></param>
        public string AddNewEmployee(Employee employee)
        {
            try
            {
                var cont = JsonConvert.SerializeObject(employee);
                var content = new StringContent(cont.ToString(), Encoding.UTF8, "application/json");

                var result = Task.Run(() => m_Client.PostAsync(m_Url, content)).Result;

                return result.ReasonPhrase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employee"></param>
        public string UpdateEmployee(Employee employee)
        {
            try
            {
                var cont = JsonConvert.SerializeObject(employee);
                var content = new StringContent(cont.ToString(), Encoding.UTF8, "application/json");

                var result = Task.Run(() => m_Client.PutAsync(m_Url, content)).Result;

                return result.ReasonPhrase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteEmployee(long id)
        {
            try
            {
                string url = string.Format("{0}/{1}", m_Url, id);
                var result = Task.Run(() => m_Client.DeleteAsync(url)).Result;

                return result.ReasonPhrase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
