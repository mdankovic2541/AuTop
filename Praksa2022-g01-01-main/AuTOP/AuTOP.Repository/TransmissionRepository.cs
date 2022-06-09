using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Repository
{
    public class TransmissionRepository : ITransmissionRepository
    {
        string connectionString = "Server=tcp:monoprojektdbserver.database.windows.net,1433;Initial Catalog = monoprojekt; Persist Security Info=False;User ID = matej; Password=Sifra1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
        public async Task<List<Transmission>> GetAllAsync(TransmissionFilter filter, Sorting sort, Paging paging)
        {

           
            StringBuilder queryString = new StringBuilder("select * from Transmission where 1=1");
            if (!String.IsNullOrWhiteSpace(filter.Name)) 
            {
                queryString.Append($" and Name Like '%{filter.Name}%");
            }
            if (filter.Gears != 0)
            {
                queryString.Append($" and Gears = {filter.Gears}");
            }

            if (!String.IsNullOrWhiteSpace(sort.SortBy))
            {
                queryString.Append($" order by { sort.SortBy} { sort.SortMethod}");
            }
            if (paging.DontPage == false)
            {
                queryString.Append($" offset { paging.GetStartElement()} rows fetch next {paging.Rpp} rows only;");
            }
            List<Transmission> transmissions = new List<Transmission>();
           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand myCommand = new SqlCommand(queryString.ToString(), connection);
                SqlDataReader myReader = await myCommand.ExecuteReaderAsync();

                while (myReader.Read())
                {
                    Transmission transmission = new Transmission();

                    transmission.Id = (Guid)myReader["Id"];
                    transmission.Name = myReader["Name"].ToString();
                    transmission.Gears = int.Parse(myReader["Gears"].ToString());
                    transmission.DateCreated = (DateTime)myReader["DateCreated"];
                    transmission.DateUpdated = (DateTime)myReader["DateUpdated"];

                    transmissions.Add(transmission);
                }
                myReader.Close();
                connection.Close();
                return transmissions;

            }

        }


        public async Task<Transmission> GetByIdAsync(Guid id)
        {
            string queryString = $"SELECT * FROM Transmission WHERE id='{id}';";

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {

                connection.Open();
                SqlCommand myCommand = new SqlCommand(queryString, connection);
                SqlDataReader myReader = await myCommand.ExecuteReaderAsync();
                Transmission transmission = new Transmission();
                while (myReader.Read())
                {

                    transmission.Id = (Guid)myReader["Id"];
                    transmission.Name = myReader["Name"].ToString();
                    transmission.Gears = int.Parse(myReader["Gears"].ToString());
                    transmission.DateCreated = (DateTime)myReader["DateCreated"];
                    transmission.DateUpdated = (DateTime)myReader["DateUpdated"];
                }
                connection.Close();
                return transmission;

            }
        }
    }
}
