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
    public class BodyShapeRepository : IBodyShapeRepository
    {
        string connectionString = "Server=tcp:monoprojektdbserver.database.windows.net,1433;Initial Catalog=monoprojekt;Persist Security Info=False;User ID=matej;Password=Sifra1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public async Task<List<BodyShape>> GetAllAsync(BodyShapeFilter filter, Sorting sort,Paging paging)
        {
            List<BodyShape> bodyShapes = new List<BodyShape>();
           
            StringBuilder queryString = new StringBuilder("select * from BodyShape where 1=1");
            if (!String.IsNullOrWhiteSpace(filter.Name)) 
            {
                queryString.Append($" and Name Like '%{filter.Name}%");
            }
            if (!String.IsNullOrWhiteSpace(sort.SortBy))
            {
                queryString.Append($" order by { sort.SortBy} { sort.SortMethod}");
            }
            if (paging.DontPage == false)
            {
                queryString.Append($" offset { paging.GetStartElement()} rows fetch next {paging.Rpp} rows only;");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand myCommand = new SqlCommand(queryString.ToString(), connection);
                SqlDataReader myReader = await myCommand.ExecuteReaderAsync();

                while (myReader.Read())
                {
                    BodyShape bodyShape = new BodyShape();

                    bodyShape.Id = (Guid)myReader["Id"];
                    bodyShape.Name = myReader["Name"].ToString();
                    bodyShape.DateCreated = (DateTime)myReader["DateCreated"];
                    bodyShape.DateUpdated = (DateTime)myReader["DateUpdated"];

                    bodyShapes.Add(bodyShape);
                }
                myReader.Close();
                connection.Close();
                return bodyShapes;

            }

        }
            public async Task<BodyShape> GetByIdAsync(Guid id)
        {
            string queryString = $"SELECT * FROM BodyShape WHERE id='{id}';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand myCommand = new SqlCommand(queryString, connection);
                SqlDataReader myReader = await myCommand.ExecuteReaderAsync();
                BodyShape bodyShape = new BodyShape();
                while (myReader.Read())
                {

                    bodyShape.Id = (Guid)myReader["Id"];
                    bodyShape.Name = myReader["Name"].ToString();
                    bodyShape.DateCreated = (DateTime)myReader["DateCreated"];
                    bodyShape.DateUpdated = (DateTime)myReader["DateUpdated"];
                }
                connection.Close();
                return bodyShape;

            }
        }
    }
}
