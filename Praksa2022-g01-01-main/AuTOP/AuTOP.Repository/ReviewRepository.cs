using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuTOP.Repository.Common;
using AuTOP.Model.Common;
using System.Data.SqlClient;
using AuTOP.Model;
using AuTOP.Common;

namespace AuTOP.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        static string connecitonString = "Server=tcp:monoprojektdbserver.database.windows.net,1433;" +
            "Initial Catalog=monoprojekt;Persist Security Info=False;User ID=matej;Password=Sifra1234;" +
            "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public async Task<List<Review>> GetAsync(ReviewFilter filter, Sorting sort, Paging paging)
        {
            List<Review> reviews = new List<Review>();
            using (SqlConnection connection = new SqlConnection(connecitonString))
            {
                SqlCommand command;
                StringBuilder queryString = new StringBuilder();
                queryString.Append("Select * from [Review] WHERE 1=1");

                if (filter != null)
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
                    Review review = new Review()
                    {
                        Id = (Guid)reader["Id"],
                        UserId = (Guid)reader["UserId"],
                        ModelVersionId = (Guid)reader["ModelVersionID"],
                        Comment = reader["Comment"].ToString(),
                        Rating = (int)reader["Rating"],
                        DateCreated = (DateTime)reader["DateCreated"],
                        DateUpdated = (DateTime)reader["DateUpdated"]
                    };
                    reviews.Add(review);
                }

                reader.Close();
                connection.Close();
            }
            return reviews;
        }

        public async Task<Review> GetByIdAsync(Guid reviewId)
        {
            Review review = new Review();
            using (SqlConnection connection = new SqlConnection(connecitonString))
            {
                string query = $"SELECT * FROM [Review] WHERE Id = '{reviewId}';";
                SqlCommand command;

                command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                reader.Read();


                review.Id = (Guid)reader["Id"];
                review.UserId = (Guid)reader["UserId"];
                review.ModelVersionId = (Guid)reader["ModelVersionID"];
                review.Comment = reader["Comment"].ToString();
                review.Rating = (int)reader["Rating"];
                review.DateCreated = (DateTime)reader["DateCreated"];
                review.DateUpdated = (DateTime)reader["DateUpdated"];

                reader.Close();
                connection.Close();
            }
            return review;
        }
        public async Task<bool> PostAsync(Review review)
        {
            if (review != null)
            {

                using (SqlConnection connection = new SqlConnection(connecitonString))
                {
                    SqlCommand command = new SqlCommand(
                      $"INSERT INTO [Review] VALUES (@Id,'{review.ModelVersionId}','{review.UserId}','{review.Comment}','{review.Rating}',@DateCreated,@DateUpdated)", connection);

                    connection.Open();
                    command.Parameters.AddWithValue("@Id", review.Id);
                    command.Parameters.AddWithValue("@DateCreated", review.DateCreated);
                    command.Parameters.AddWithValue("@DateUpdated", review.DateUpdated);
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
        public async Task<bool> PutAsync(Guid reveiwId, Review review)
        {
            if (reveiwId != null && review != null)
            {
                using (SqlConnection connection = new SqlConnection(connecitonString))
                {
                    SqlCommand command = new SqlCommand(
                      $"UPDATE [Review] SET Comment='{review.Comment}', Rating='{review.Rating}', DateUpdated=GETDATE() WHERE Id='{reveiwId}'", connection);

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

        public async Task<bool> DeleteAsync(Guid reviewId)
        {
            if (reviewId != null)
            {
                using (SqlConnection connection = new SqlConnection(connecitonString))
                {
                    SqlCommand command = new SqlCommand(
                      $"DELETE FROM [Review] WHERE Id='{reviewId}'", connection);

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

        private async Task<StringBuilder> AddFilter(ReviewFilter filter, StringBuilder queryString)
        {
            if (filter.ModelVersionId.HasValue)
            {
                queryString.Append($" AND ModelVersionId = '{filter.ModelVersionId}'");
            }

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
