using EmployeeSPApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeSPApp.DAL
{
    public class EmployeeDAL
    {
        private readonly string _connectionString;

        public EmployeeDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Employee> GetAllEmployees()
        {
            var list = new List<Employee>();
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_GetAllEmployees", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new Employee
                {
                    EmpId = Convert.ToInt32(rdr["EmpId"]),
                    Name = rdr["Name"].ToString(),
                    Department = rdr["Department"].ToString(),
                    Salary = Convert.ToDecimal(rdr["Salary"])
                });
            }
            return list;
        }

        public void InsertEmployee(Employee emp)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_InsertEmployee", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Department", emp.Department);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateEmployee(Employee emp)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Department", emp.Department);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteEmployee(int empId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@EmpId", empId);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public Employee GetEmployeeById(int empId)
        {
            Employee emp = new();
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employee Where EmpId = @EmpId", con);
            cmd.Parameters.AddWithValue("@EmpId", empId);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                emp.EmpId = Convert.ToInt32(rdr["EmpId"]);
                emp.Name = rdr["Name"].ToString();
                emp.Department = rdr["Department"].ToString();
                emp.Salary = Convert.ToDecimal(rdr["Salary"]);
            }
            return emp;
        }
    }
}
