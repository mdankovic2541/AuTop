using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Repository
{
    public class ModelVersionRepository : IModelVersionRepository
    {

        private String connectionString = "Server=tcp:monoprojektdbserver.database.windows.net,1433;Initial Catalog=monoprojekt;Persist Security Info=False;User ID=kristijan;Password=Robinhoodr52600;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public async Task<List<ModelVersion>> GetAllModelVersions(ModelVersionFilter filter,Sorting sort, Paging paging)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            StringBuilder queryString = new StringBuilder("select * from ModelVersion where 1=1");

            if (!String.IsNullOrWhiteSpace(filter.Name))
            {
                queryString.Append($" and Name Like '%{filter.Name}%'");
            }
            
            if(filter.ModelId.HasValue)
            {
                queryString.Append($" and ModelId = '{filter.ModelId}'");
            }
            if (filter.PowerFrom != 0 || filter.PowerTo != 0)
            {
                if (filter.PowerFrom == 0)
                {
                    queryString.Append($" and MotorId = (select Id from [dbo].[Motor] where Id = [dbo].[ModelVersion].MotorId and MaxHP < {filter.PowerTo} )");
                }
                if (filter.PowerTo == 0)
                {
                    queryString.Append($" and MotorId = (select Id from [dbo].[Motor] where Id = [dbo].[ModelVersion].MotorId and MaxHP > {filter.PowerFrom} )");
                }
                if (filter.PowerFrom != 0 && filter.PowerTo != 0)
                    queryString.Append($" and MotorId = (select Id from [dbo].[Motor] where Id = [dbo].[ModelVersion].MotorId and MaxHP > {filter.PowerFrom} and MaxHP < {filter.PowerTo}  )");
            }
            if (filter.YearFrom != 0 || filter.YearTo != 0)
            {
                if (filter.YearFrom == 0)
                {
                    queryString.Append($" and Year < {filter.YearTo}");
                }
                if (filter.YearTo == 0)
                {
                    queryString.Append($" and Year > {filter.YearFrom}");
                }
            }

            if (filter.TransmissionId.HasValue)
            {
                queryString.Append($" and TransmissionId = '{filter.TransmissionId}'");
            }
            if (filter.BodyShapeId.HasValue)
            {
                queryString.Append($" and BodyShapeId = '{filter.BodyShapeId}'");
            }

            if (!String.IsNullOrWhiteSpace(sort.SortBy))
            {
                queryString.Append($" order by { sort.SortBy} { sort.SortMethod}");
            }
            if (paging.DontPage == false)
            {
                queryString.Append($" offset {paging.GetStartElement()} rows fetch next {paging.Rpp} rows only;");
            }
            SqlCommand command = new SqlCommand(queryString.ToString(), connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet modelVersionData = new DataSet();
            await Task.Run(() => adapter.Fill(modelVersionData));
            List<ModelVersion> modelVersions = new List<ModelVersion>();
            if (modelVersionData.Tables[0].Rows.Count == 0)
            {
                return modelVersions;
            }

            foreach (DataRow dataRow in modelVersionData.Tables[0].Rows)
            {
                modelVersions.Add(new ModelVersion
                {
                    Name = Convert.ToString(dataRow["Name"]),
                    Id = Guid.Parse(Convert.ToString(dataRow["Id"])),
                    ModelId = Guid.Parse(Convert.ToString(dataRow["ModelId"])),
                    MotorId = Guid.Parse(Convert.ToString(dataRow["MotorId"])),
                    BodyShapeId = Guid.Parse(Convert.ToString(dataRow["BodyShapeId"])),
                    TransmissionId = Guid.Parse(Convert.ToString(dataRow["TransmissionId"])),
                    FuelConsumption = Convert.ToDecimal(dataRow["FuelConsumption"]),
                    Year = Convert.ToInt32(dataRow["Year"]),
                    Acceleration = Convert.ToDecimal(dataRow["Acceleration"]),
                    Doors = Convert.ToInt32(dataRow["Doors"]),
                    DateCreated = Convert.ToDateTime(dataRow["DateCreated"]),
                    DateUpdated = Convert.ToDateTime(dataRow["DateUpdated"])
                });
            }
            return modelVersions;

        }
        public async Task<ModelVersion> GetModelVersionById(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = $"select * from ModelVersion where Id = '{id}';";
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet modelVersionData = new DataSet();
            await Task.Run(() => adapter.Fill(modelVersionData));
            DataRow dataRow = modelVersionData.Tables[0].Rows[0];
            ModelVersion modelVersion = new ModelVersion
            {
                Name = Convert.ToString(dataRow["Name"]),
                Id = Guid.Parse(Convert.ToString(dataRow["Id"])),
                ModelId = Guid.Parse(Convert.ToString(dataRow["ModelId"])),
                MotorId = Guid.Parse(Convert.ToString(dataRow["MotorId"])),
                BodyShapeId = Guid.Parse(Convert.ToString(dataRow["BodyShapeId"])),
                TransmissionId = Guid.Parse(Convert.ToString(dataRow["TransmissionId"])),
                FuelConsumption = Convert.ToDecimal(dataRow["FuelConsumption"]),
                Year = Convert.ToInt32(dataRow["Year"]),
                Acceleration = Convert.ToDecimal(dataRow["Acceleration"]),
                Doors = Convert.ToInt32(dataRow["Doors"]),
                DateCreated = Convert.ToDateTime(dataRow["DateCreated"]),
                DateUpdated = Convert.ToDateTime(dataRow["DateUpdated"])
            };
            return modelVersion;
        }

        public async Task PostModelVersionAsync(ModelVersion modelVersion)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = $"insert into ModelVersion values('{modelVersion.Id}','{modelVersion.ModelId}','{modelVersion.MotorId}','{modelVersion.BodyShapeId}','{modelVersion.TransmissionId}','{modelVersion.FuelConsumption}',{modelVersion.Year},{modelVersion.Acceleration},{modelVersion.Doors},@DateCreated,@DateUpdated,'{modelVersion.Name}');";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.Add("@DateCreated",SqlDbType.DateTime);
            command.Parameters["@DateCreated"].Value = modelVersion.DateCreated;
            command.Parameters.Add("@DateUpdated", SqlDbType.DateTime);
            command.Parameters["@DateUpdated"].Value = modelVersion.DateUpdated;
            connection.Open();
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public async Task PutModelVersionAsync(ModelVersion modelVersion)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = $"update ModelVersion  set ModelId = '{modelVersion.ModelId},MotorId = '{modelVersion.MotorId}',BodyShapeId = '{modelVersion.BodyShapeId}',TransmissionId = '{modelVersion.TransmissionId}',FuelConsumption = {modelVersion.FuelConsumption},Acceleration = {modelVersion.Acceleration},DateUpdated = @DateUpdated,Name = '{modelVersion.Name}'";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.Add("@DateUpdated", SqlDbType.DateTime);
            command.Parameters["@DateUpdated"].Value=modelVersion.DateUpdated;
            connection.Open();
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }
        public async Task DeleteModelVersionAsync(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = $"delete from ModelVersion where Id = '{id}'";
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

    }
}

