using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuTOP.Model.Common;
using AuTOP.Model;
using AuTOP.Repository.Common;
using System.Data;
using AuTOP.Common;

namespace AuTOP.Repository 
{
    public class UserRepository : IUserRepository
    {
        static string connecitonString = "Server=tcp:monoprojektdbserver.database.windows.net,1433;" +
            "Initial Catalog=monoprojekt;Persist Security Info=False;User ID=matej;Password=Sifra1234;" +
            "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public async Task<List<IUser>> GetAsync(UserFilter filter, Sorting sort, Paging paging)
        {
            List<IUser> users = new List<IUser>();
            using (SqlConnection connection = new SqlConnection(connecitonString))
            {
                
                SqlCommand command;
                StringBuilder queryString = new StringBuilder(); 
                queryString.Append("Select * from [User] WHERE 1=1");

                //TODO:
                //3 metode: jedna za filtere, druga za sorter, treća za paging

                if(filter!=null)
                {
                    await AddFilter(filter, queryString);
                }

                if (sort != null)
                {
                    await AddSorting(sort, queryString);
                }

                if (paging != null)
                {
                    await AddPaging(paging, queryString);
                }

                command = new SqlCommand(queryString.ToString(), connection);
                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    User user = new User()
                    {
                        Id = (Guid)reader["Id"],
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Email = reader["Email"].ToString(),
                        DateCreated = (DateTime)reader["DateCreated"],
                        DateUpdated = (DateTime)reader["DateUpdated"]
                    };
                    users.Add(user);
                }

                reader.Close();
                connection.Close();
            }
            return users;
        }

        public async Task<IUser> GetByIdAsync(Guid userId)
        {
            User user = new User();
            using (SqlConnection connection = new SqlConnection(connecitonString))
            {
                string query = $"SELECT * FROM [User] WHERE Id = '{userId}';";
                SqlCommand command;

                command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows != false)
                {
                    reader.Read();

                    user.Id = (Guid)reader["Id"];
                    user.Username = reader["Username"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.DateCreated = (DateTime)reader["DateCreated"];
                    user.DateUpdated = (DateTime)reader["DateUpdated"];

                    reader.Close();
                    connection.Close();
                    return user;
                }
                else
                {
                    return null;
                }
            }            
        }

        public async Task<bool> PostAsync(User user)
        {
            if (user != null)
            {
                using (SqlConnection connection = new SqlConnection(connecitonString))
                {
                    SqlCommand command = new SqlCommand(
                      $"INSERT INTO [User] VALUES (@Id,'{user.Username}','{user.Password}','{user.Email}','761B13B6-699D-45EF-9EFB-E31D352BC476',@DateCreated,@DateUpdated)", connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@DateCreated", user.DateCreated);
                    command.Parameters.AddWithValue("@DateUpdated", user.DateUpdated);
                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> PutAsync(Guid userId, User user)
        {
            if (userId != null && user != null)
            {
                using (SqlConnection connection = new SqlConnection(connecitonString))
                {
                    SqlCommand command = new SqlCommand(
                      $"UPDATE [User] SET Username='{user.Username}', Password='{user.Password}', Email='{user.Email}', DateUpdated=@DateUpdated WHERE Id='{userId}'", connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@DateUpdated", user.DateUpdated);
                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            if (userId != null)
            {
                using (SqlConnection connection = new SqlConnection(connecitonString))
                {
                    SqlCommand command = new SqlCommand(
                      $"DELETE FROM [User] WHERE Id='{userId}'", connection);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Guid> GetIdbyName(string name)
        {
            SqlConnection connection = new SqlConnection(connecitonString);
            string querryString = $"Select Id from [User] where Username = '{name}'";
            SqlDataAdapter adapter = new SqlDataAdapter(querryString, connection);
            DataSet userData = new DataSet();
            await Task.Run(() => adapter.Fill(userData));
            DataRow dataRow = userData.Tables[0].Rows[0];
            return Guid.Parse(Convert.ToString(dataRow["Id"]));            
        }

        private async Task<StringBuilder> AddFilter(UserFilter filter,StringBuilder queryString)
        {
            if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
            {
                queryString.Append($" AND (Username = '{ filter.SearchQuery}' OR Email = '{ filter.SearchQuery}')");

            }
            //if (filter.RoleId.HasValue)
            //{
            //    queryString.Append($" AND RoleId = '{ filter.RoleId}' ");
            //}
            return await Task.FromResult(queryString);
        }

        private async Task<StringBuilder> AddSorting(Sorting sorting, StringBuilder queryString)
        {
            if (!string.IsNullOrWhiteSpace(sorting.SortMethod) && !string.IsNullOrWhiteSpace(sorting.SortBy))
            {
                queryString.Append($" ORDER BY '{sorting.SortBy}' {sorting.SortMethod}");

            }
            return await Task.FromResult(queryString);
        }

        private async Task<StringBuilder> AddPaging(Paging paging, StringBuilder queryString)
        {
            if (paging.Page > 0)
            {
                queryString.Append($" OFFSET {paging.GetStartElement()} ROWS FETCH NEXT {paging.Rpp} ROWS ONLY");

            }
            return await Task.FromResult(queryString);
        }
    }
}
