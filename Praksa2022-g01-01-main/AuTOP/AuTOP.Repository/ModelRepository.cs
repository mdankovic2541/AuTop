using AuTOP.Common;
using AuTOP.Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Repository
{
    internal class ModelRepository : IModelRepository
    {
        private String connectionString = "Server=tcp:monoprojektdbserver.database.windows.net,1433;Initial Catalog=monoprojekt;Persist Security Info=False;User ID=kristijan;Password=Robinhoodr52600;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public async Task<List<ModelDomainModel>> GetAllModelsAsync(ModelFilter filter, Sorting sort, Paging paging)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            StringBuilder queryString = new StringBuilder("select * from Model where 1=1");
            if (!String.IsNullOrWhiteSpace(filter.Name));
            {
                queryString.Append($" and Name Like '%{filter.Name}%'");
            }
            if(filter.ManufacturerId.HasValue)
            {
                queryString.Append($" and ManufacturerId = '{filter.ManufacturerId}'");
            }

            if (!String.IsNullOrWhiteSpace(sort.SortBy))
            {
                queryString.Append($" order by { sort.SortBy} { sort.SortMethod}");
            }
            if (paging.DontPage == false)
            {
                queryString.Append($" offset { paging.GetStartElement()} rows fetch next {paging.Rpp} rows only;");
            }
            SqlCommand command = new SqlCommand(queryString.ToString(), connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet modelData = new DataSet();
            await Task.Run(() => adapter.Fill(modelData));
            List<ModelDomainModel> models = new List<ModelDomainModel>();
            if (modelData.Tables[0].Rows.Count == 0)
            {
                return models;
            }

            foreach (DataRow dataRow in modelData.Tables[0].Rows)
            {
                models.Add(new ModelDomainModel
                {
                    Id = Guid.Parse(Convert.ToString(dataRow["Id"])),
                    ManufacturerId = Guid.Parse(Convert.ToString(dataRow["ManufacturerId"])),
                    Name = Convert.ToString(dataRow["Name"]),
                    ImageURL = Convert.ToString(dataRow["ImageURL"]),
                    DateCreated = Convert.ToDateTime(dataRow["DateCreated"]),
                    DateUpdated = Convert.ToDateTime(dataRow["DateUpdated"])
                });
            }
            return models;

        }

        public async Task<ModelDomainModel> GetModelById(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = $"select * from Model where Id = '{id}';";
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ModelData = new DataSet();
            await Task.Run(() => adapter.Fill(ModelData));
            DataRow dataRow = ModelData.Tables[0].Rows[0];
            ModelDomainModel Model = new ModelDomainModel
            {
                Id = Guid.Parse(Convert.ToString(dataRow["Id"])),
                ManufacturerId = Guid.Parse(Convert.ToString(dataRow["ManufacturerId"])),
                Name = Convert.ToString(dataRow["Name"]),
                ImageURL = Convert.ToString(dataRow["ImageURL"]),
                DateCreated = Convert.ToDateTime(dataRow["DateCreated"]),
                DateUpdated = Convert.ToDateTime(dataRow["DateUpdated"])
            };
            return Model;
        }

        public async Task PostModelAsync(ModelDomainModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = $"insert into model values('{model.Id}','{model.ManufacturerId}','{model.Name}',@DateCreated,@DateUpdated,'{model.ImageURL}');";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.Add("@DateCreated", SqlDbType.DateTime);
            command.Parameters["@DateCreated"].Value = model.DateCreated;
            command.Parameters.Add("@DateUpdated", SqlDbType.DateTime);
            command.Parameters["@DateUpdated"].Value = model.DateUpdated;
            connection.Open();
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public async Task PutModelAsync(ModelDomainModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = $"update model set Name = {model.Name},ImageURL = '{model.ImageURL}',DateUpdated = @DateUpdated where Id = '{model.Id}'";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.Add("@DateUpdated", SqlDbType.DateTime);
            command.Parameters["@DateUpdated"].Value = model.DateUpdated;
            connection.Open();
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public async Task DeleteModelAsync(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = $"delete from model where Id = '{id}'";
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }


    }
}
