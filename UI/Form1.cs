using System.Data;
using System.Data.SqlClient;
using CustomerManager.Services;
using CustomerManager.Models;
using CustomerManager.Data;
namespace CustomerManager
{
    public partial class Form1 : Form
    {
        private CustomerService _service;
        string connectionString = "Server=HAMZA\\SQLEXPRESS;Database=CustomerDB;Trusted_Connection=True;TrustServerCertificate=True";
        public Form1()
        {
            InitializeComponent();

            var repo = new CustomerRepository("Server=HAMZA\\SQLEXPRESS;Database=CustomerDB;Trusted_Connection=True;TrustServerCertificate=True");
            _service = new CustomerService(repo);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            //first version
            //try
            //{
            //    using (SqlConnection con = new SqlConnection(connectionString))
            //    {
            //        //opening con
            //        con.Open();
            //        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Customers", con);
            //        DataTable dataTable = new DataTable();
            //        sqlDataAdapter.Fill(dataTable);
            //        dataGridView1.DataSource = dataTable;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}

            var customers = _service.GetCustomers();
            dataGridView1.DataSource = customers;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //first version
            //try
            //{
            //    using (SqlConnection con = new SqlConnection(connectionString))
            //    {
            //        string query = "INSERT INTO Customers(Name, Email, Phone) VALUES (@Name, @Email, @Phone)";
            //        using (SqlCommand cmd = new SqlCommand(query, con))
            //        {
            //            if (
            //                !string.IsNullOrWhiteSpace(txtName.Text) ||
            //                !string.IsNullOrWhiteSpace(txtEmail.Text) ||
            //                !string.IsNullOrWhiteSpace(txtPhone.Text)
            //                )
            //            {
            //                cmd.Parameters.AddWithValue("@Name", txtName.Text);
            //                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            //                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
            //                con.Open();
            //                cmd.ExecuteNonQuery();
            //            }
            //            else
            //            {
            //                MessageBox.Show("You cant add empty values");
            //                return;
            //            }
            //        }
            //    }
            //    btnLoad.PerformClick();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
            var customer = new Customer
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
            };
            _service.AddCustomer(customer);
            btnLoad.PerformClick();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //first version

            //try
            //{


            //    if (dataGridView1.CurrentRow != null)
            //    {
            //        using (SqlConnection con = new SqlConnection(connectionString))
            //        {
            //            string query = "DELETE FROM Customers WHERE Id = @Id";
            //            using (SqlCommand sqlCommand = new SqlCommand(query, con))
            //            {
            //                int Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            //                sqlCommand.Parameters.AddWithValue("@Id", Id);
            //                con.Open();
            //                sqlCommand.ExecuteNonQuery();
            //            }
            //        }
            //        btnLoad.PerformClick();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}

            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                _service.DeleteCustomer(id);
                btnLoad.PerformClick();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //first version
            //try
            //{
            //    if (dataGridView1.CurrentRow != null)
            //    {
            //        int Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            //        using (SqlConnection con = new SqlConnection(connectionString))
            //        {
            //            string query = "UPDATE Customers SET Name = @Name, Email = @Email, Phone = @Phone WHERE Id = @id";
            //            using (SqlCommand sqlCommand = new SqlCommand(query, con))
            //            {
            //                sqlCommand.Parameters.AddWithValue("@Id", Id);
            //                sqlCommand.Parameters.AddWithValue("@Name", txtName.Text);
            //                sqlCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
            //                sqlCommand.Parameters.AddWithValue("@Phone", txtPhone.Text);
            //                con.Open();
            //                sqlCommand.ExecuteNonQuery();
            //            }
            //        }

            //    }

            //    btnLoad.PerformClick();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}

            if (dataGridView1.CurrentRow != null)
            {
                int Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                var customer = new Customer
                {
                    Id = Id,
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text,
                };
                _service.UpdateCustomer(customer);
                btnLoad.PerformClick();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                txtPhone.Text = dataGridView1.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
            }
        }
    }
}
