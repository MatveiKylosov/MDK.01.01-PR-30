using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDK._01._01_PR_30.Classes
{
    public class Customers
    {
        public int CustomersID;
        public string FullName;
        public string PassportDetails;
        public string Address;
        public string city;
        public DateTime DateOfBirth; 
        public bool Gender;

        static public List<Customers> GetAll
        {
            get
            {
                List<Customers> Customers = new List<Customers>();

                using (MySqlConnection connection = Connection.GetConnection())
                {
                    try { connection.Open(); }
                    catch { return null; }
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Customers", connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customers.Add(
                                new Customers(
                                    reader.GetInt32("CustomersID"),
                                    reader.GetString("FullName"),
                                    reader.GetString("PassportDetails"),
                                    reader.GetString("Address"),
                                    reader.GetString("city"),
                                    reader.GetDateTime("DateOfBirth"),
                                    reader.GetBoolean("Gender")
                                    )
                                );
                        }
                    }
                }

                return Customers;
            }
        }

        public Customers(int CustomersID, string FullName, string PassportDetails, string Address, string city, DateTime DateOfBirth, bool Gender)
        {
            this.CustomersID = CustomersID;
            this.FullName = FullName;
            this.PassportDetails = PassportDetails;
            this.Address = Address;
            this.city = city;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
        }

        public bool Update(string FullName, string PassportDetails, string Address, string city, DateTime DateOfBirth, bool Gender)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@CustomersID", CustomersID},
                {"@FullName", FullName},
                {"@PassportDetails", PassportDetails},
                {"@Address", Address},
                {"@city", city},
                {"@DateOfBirth", DateOfBirth},
                {"@Gender", Gender}
            };
            return Connection.ExecuteNonQuery("UPDATE Customers SET FullName = @FullName, PassportDetails = @PassportDetails, Address = @Address, city = @city, DateOfBirth = @DateOfBirth, Gender = @Gender WHERE CustomersID = @CustomersID", parameters);
        }

        static public bool Insert(string FullName, string PassportDetails, string Address, string city, DateTime DateOfBirth, bool Gender)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@FullName", FullName},
                {"@PassportDetails", PassportDetails},
                {"@Address", Address},
                {"@city", city},
                {"@DateOfBirth", DateOfBirth},
                {"@Gender", Gender}
            };
            return Connection.ExecuteNonQuery("INSERT INTO Customers (FullName, PassportDetails, Address, city, DateOfBirth, Gender) VALUES (@FullName, @PassportDetails, @Address, @city, @DateOfBirth, @Gender)", parameters);
        }

        public bool Delete()
        {
            var parameters = new Dictionary<string, object>
            {
                {"@CustomersID", this.CustomersID}
            };
            return Connection.ExecuteNonQuery("DELETE FROM Customers WHERE CustomersID = @CustomersID", parameters);
        }
    }
}
