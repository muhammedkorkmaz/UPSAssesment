using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPSAssessment
{
    public partial class Form1 : Form
    {

        private Employee m_FocussedData;
        EmployeeRestClient m_RestClient = new EmployeeRestClient();

        private int m_PageNumber = 1;

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            cbGender.DataSource = Enum.GetValues(typeof(Gender));
            cbStatus.DataSource = Enum.GetValues(typeof(Status));

            read();
        }

        private void read()
        {
            try
            {
                dgvEmployees.DataSource = m_RestClient.ReadEmployees(m_PageNumber);
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        private void dgvEmployees_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            m_FocussedData = (Employee)dgvEmployees.CurrentRow.DataBoundItem;

            tbName.Text = m_FocussedData.name;
            tbEmail.Text = m_FocussedData.email;

            cbGender.SelectedItem = m_FocussedData.gender;
            cbStatus.SelectedItem = m_FocussedData.status;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validate())
                {
                    return;
                }

                screenToModel();

                if (m_FocussedData.id == null)
                {
                    Random rnd = new Random();
                    m_FocussedData.id = rnd.Next();
                    string result = m_RestClient.AddNewEmployee(m_FocussedData);

                    showInfo(result);
                }
                else
                {
                    string result = m_RestClient.UpdateEmployee(m_FocussedData);

                    showInfo(result);
                }

                clear();
                read();
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        void screenToModel()
        {
            if (m_FocussedData == null)
                m_FocussedData = new Employee();

            m_FocussedData.name = tbName.Text;
            m_FocussedData.email = tbEmail.Text;
            m_FocussedData.gender = Convert.ToString(cbGender.SelectedValue);
            m_FocussedData.status = Convert.ToString(cbStatus.SelectedValue);

        }

        private bool validate()
        {
            if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbEmail.Text))
            {
                showWarning("Please fill all the informations.");
                return false;
            }
            return true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_FocussedData != null && m_FocussedData.id != null)
                {
                    string result = m_RestClient.DeleteEmployee(Convert.ToInt64(m_FocussedData.id));

                    showInfo(result);
                }
                else
                {
                    showWarning("Please select the row you want to delete with double click.");
                }

                clear();
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            tbName.Text = "";
            tbEmail.Text = "";

            cbGender.SelectedItem = "";
            cbStatus.SelectedItem = "";

            m_FocussedData = null;

            m_PageNumber = 1;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            read();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tbName.Text))
                {
                    dgvEmployees.DataSource = m_RestClient.SearchEmployeesByName(tbName.Text);
                }
                else
                {
                    showWarning("Please type the name you want to search.");
                }

                clear();
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            m_PageNumber += 1;
            read();

            tbPageNumber.Text = m_PageNumber.ToString();

        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (m_PageNumber <= 1)
            {
                return;
            }
            m_PageNumber -= 1;
            read();

            tbPageNumber.Text = m_PageNumber.ToString();

        }

        private void showInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void showError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgvEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            m_FocussedData = (Employee)dgvEmployees.CurrentRow.DataBoundItem;

            tbName.Text = m_FocussedData.name;
            tbEmail.Text = m_FocussedData.email;

            cbGender.SelectedItem = m_FocussedData.gender;
            cbStatus.SelectedItem = m_FocussedData.status;
        }
    }
}
