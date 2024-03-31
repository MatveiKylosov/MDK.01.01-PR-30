using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MDK._01._01_PR_30.Classes
{
    public class CarBrands
    {
        public string BrandName { get; set; }
        public string CountryOrigin;
        public string ManufacturerFactory;
        public string Address;

        static public List<CarBrands> GetAll
        {
            get
            {
                List<CarBrands> carBrands = new List<CarBrands>();

                using (MySqlConnection connection = Connection.GetConnection())
                {
                    try { connection.Open(); }
                    catch { return null; }

                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM CarBrands", connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carBrands.Add(
                                new CarBrands(
                                    reader.GetString("BrandName"),
                                    reader.GetString("CountryOrigin"),
                                    reader.GetString("ManufacturerFactory"),
                                    reader.GetString("Address")
                                    )
                                );
                        }
                    }
                }

                return carBrands;
            }
        }

        public CarBrands(string BrandName, string CountryOrigin, string ManufacturerFactory, string Address)
        {
            this.BrandName = BrandName;
            this.CountryOrigin = CountryOrigin;
            this.ManufacturerFactory = ManufacturerFactory;
            this.Address = Address;
        }

        public bool Update(string BrandName, string CountryOrigin, string ManufacturerFactory, string Address)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@BrandName", BrandName},
                {"@CountryOrigin", CountryOrigin},
                {"@ManufacturerFactory", ManufacturerFactory},
                {"@Address", Address}
            };
            return Connection.ExecuteNonQuery("UPDATE CarBrands SET BrandName = @BrandName, CountryOrigin = @CountryOrigin, ManufacturerFactory = @ManufacturerFactory, Address = @Address WHERE BrandName = @BrandName", parameters);
        }

        static public bool Insert(string BrandName, string CountryOrigin, string ManufacturerFactory, string Address)
        {
            var parameters = new Dictionary<string, object>
            {
                {"@BrandName", BrandName},
                {"@CountryOrigin", CountryOrigin},
                {"@ManufacturerFactory", ManufacturerFactory},
                {"@Address", Address}
            };
            return Connection.ExecuteNonQuery("INSERT INTO CarBrands (BrandName, CountryOrigin, ManufacturerFactory, Address) VALUES (@BrandName, @CountryOrigin, @ManufacturerFactory, @Address)", parameters);
        }

        public bool Delete()
        {
            var parameters = new Dictionary<string, object>
            {
                {"@BrandName", this.BrandName}
            };
            return Connection.ExecuteNonQuery("DELETE FROM CarBrands WHERE BrandName = @BrandName", parameters);
        }
    }
}
