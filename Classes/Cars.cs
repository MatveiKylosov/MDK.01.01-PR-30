using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDK._01._01_PR_30.Classes
{
    public class Cars
    {
        public int      CarID;
        public string   Name;
        public string   Stamp;
        public int      YearProduction;
        public string   Colour;
        public string   Category;
        public decimal  Price;

        static public List<Cars> GetAll
        {
            get
            {
                List<Cars> Cars = new List<Cars>();

                using (MySqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Cars", connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cars.Add(
                                new Cars(
                                    reader.GetInt32("CarID"),
                                    reader.GetString("Name"),
                                    reader.GetString("Stamp"),
                                    reader.GetInt32("YearProduction"),
                                    reader.GetString("Colour"),
                                    reader.GetString("Category"),
                                    reader.GetDecimal("Price")
                                    )
                                );
                        }
                    }
                }

                return Cars;
            }
        }

        public Cars(int CarID, string Name, string Stamp, int YearProduction, string Colour, string Category, decimal Price)
        {
            this.CarID = CarID;
            this.Name = Name;
            this.Stamp = Stamp;
            this.YearProduction = YearProduction;
            this.Colour = Colour;
            this.Category = Category;
            this.Price = Price;
        }

        public bool Update(string Name, string Stamp, int YearProduction, string Colour, string Category, decimal Price)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@CarID", this.CarID},
                {"@Name", Name},
                {"@Stamp", Stamp},
                {"@YearProduction", YearProduction},
                {"@Colour", Colour},
                {"@Category", Category},
                {"@Price", Price}
            };

            return Connection.ExecuteNonQuery("UPDATE Cars SET CarID = @CarID, Name = @Name, Stamp = @Stamp, YearProduction = @YearProduction, Colour = @Colour, Category = @Category, Price = @Price WHERE CarID = @CarID", parameters);
        }

        public bool Insert(string Name, string Stamp, int YearProduction, string Colour, string Category, decimal Price)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@CarID", this.CarID},
                {"@Name", Name},
                {"@Stamp", Stamp},
                {"@YearProduction", YearProduction},
                {"@Colour", Colour},
                {"@Category", Category},
                {"@Price", Price}
            };
            return Connection.ExecuteNonQuery("INSERT INTO Car (CarID, Name, Stamp, YearProduction, Colour, Category, Price) VALUSE (@CarID, @Name, @Stamp, @YearProduction, @Colour, @Category, @Price)", parameters);
        }

        public bool Delete()
        {
            var parameters = new Dictionary<string, object>
            {
                {"@CarID", this.CarID}
            };
            return Connection.ExecuteNonQuery("DELETE FROM Cars WHERE CarID = @CarID", parameters);
        }
    }
}
