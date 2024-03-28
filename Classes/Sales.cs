using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDK._01._01_PR_30.Classes
{
    public class Sales
    {
        public int SaleID;
        public int CustomersID;
        public int EmployeeID;
        public int CarID;
        public DateTime DateSale;

        static public List<Sales> GetAll
        {
            get
            {
                List<Sales> Sales = new List<Sales>();

                using (MySqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Sales", connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sales.Add(
                                new Sales(
                                    reader.GetInt32("SaleID"),
                                    reader.GetInt32("CustomersID"),
                                    reader.GetInt32("EmployeeID"),
                                    reader.GetInt32("CarID"),
                                    reader.GetDateTime("DateSale")
                                    )
                                );
                        }
                    }
                }

                return Sales;
            }
        }

        public Sales(int SaleID, int CustomersID, int EmployeeID, int CarID, DateTime DateSale)
        {
            this.CustomersID = CustomersID;
            this.EmployeeID = EmployeeID;
            this.CarID = CarID;
            this.DateSale = DateSale;
        }

        public bool Update(int CustomersID, int EmployeeID, int CarID, DateTime DateSale)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@SaleID", SaleID},
                {"@CustomersID", CustomersID},
                {"@EmployeeID", EmployeeID},
                {"@CarID", CarID},
                {"@DateSale", DateSale}
            };
            return Connection.ExecuteNonQuery("UPDATE Sales SET SaleID = @SaleID, CustomersID = @CustomersID, EmployeeID = @EmployeeID, CarID = @CarID, DateSale = @DateSale WHERE SaleID = @SaleID", parameters);
        }

        public bool Insert(int CustomersID, int EmployeeID, int CarID, DateTime DateSale)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@SaleID", SaleID},
                {"@CustomersID", CustomersID},
                {"@EmployeeID", EmployeeID},
                {"@CarID", CarID},
                {"@DateSale", DateSale}
            };
            return Connection.ExecuteNonQuery("INSERT INTO Sales (SaleID, CustomersID, EmployeeID, CarID, DateSale) VALUES (@SaleID, @CustomersID, @EmployeeID, @CarID, @DateSale)", parameters);
        }

        public bool Delete()
        {
            var parameters = new Dictionary<string, object>
            {
                {"@SaleID", this.SaleID}
            };
            return Connection.ExecuteNonQuery("DELETE FROM Sales WHERE SaleID = @SaleID", parameters);
        }
    }
}
