using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDK._01._01_PR_30.Classes
{
    public class Employees
    {
        public int      EmployeeID;
        public string   FullName;
        public int      Experience;
        public decimal  Salary;

        static public List<Employees> GetAll
        {
            get
            {
                List<Employees> Employees = new List<Employees>();

                using (MySqlConnection connection = Connection.GetConnection())
                {
                    try { connection.Open(); }
                    catch { return null; }
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Employees", connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employees.Add(
                                new Employees(
                                    reader.GetInt32("EmployeeID"),
                                    reader.GetString("FullName"),
                                    reader.GetInt32("Experience"),
                                    reader.GetDecimal("Salary")
                                    )
                                );
                        }
                    }
                }

                return Employees;
            }
        }

        public Employees(int EmployeeID, string FullName, int Experience, decimal Salary)
        {
            this.EmployeeID = EmployeeID;
            this.FullName = FullName;
            this.Experience = Experience;
            this.Salary = Salary;
        }

        public bool Update(string FullName, int Experience, decimal Salary)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@EmployeeID", this.EmployeeID},
                {"@FullName", FullName},
                {"@Experience", Experience},
                {"@Salary", Salary}
            };
            return Connection.ExecuteNonQuery("UPDATE Employees SET EmployeeID = @EmployeeID, FullName = @FullName, Experience = @Experience, Salary = @Salary WHERE EmployeeID = @EmployeeID", parameters);
        }

        public bool Insert(string FullName, int Experience, decimal Salary)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@EmployeeID", this.EmployeeID},
                {"@FullName", FullName},
                {"@Experience", Experience},
                {"@Salary", Salary}
            };

            return Connection.ExecuteNonQuery("INSERT INTO CarBrands (EmployeeID, FullName, Experience, Salary) VALUES (@EmployeeID, @FullName, @Experience, @Salary)", parameters);
        }

        public bool Delete()
        {
            var parameters = new Dictionary<string, object>
            {
                {"@EmployeeID", this.EmployeeID}
            };
            return Connection.ExecuteNonQuery("DELETE FROM Employees WHERE EmployeeID = @EmployeeID", parameters);
        }
    }
}
