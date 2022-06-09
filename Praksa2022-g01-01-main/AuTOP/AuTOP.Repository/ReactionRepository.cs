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
    public class ReactionRepository : IReactionRepository
    {
        private string connectionString = "Server=tcp:monoprojektdbserver.database.windows.net,1433;Initial Catalog = monoprojekt; Persist Security Info=False;User ID = matej; Password=Sifra1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
        public async Task<Reaction> GetUserReaction(Guid userId,Guid reviewId)
        {
            string queryString = $"select * from Reaction where UserId = '{userId}' and ReviewId = '{reviewId}'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
            DataSet reactionData = new DataSet();
            adapter.Fill(reactionData);
            if(reactionData.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            DataRow dataRow = reactionData.Tables[0].Rows[0];
            Reaction reaction = new Reaction
            {
                UserId = Guid.Parse(Convert.ToString(dataRow["UserId"])),
                ReviewId = Guid.Parse(Convert.ToString(dataRow["ReviewId"])),
                IsLiked = Convert.ToBoolean(dataRow["IsLiked"]),
                DateCreated = Convert.ToDateTime(dataRow["DateCreated"]),
                DateUpdated = Convert.ToDateTime(dataRow["DateUpdated"])
            };
            return reaction;

            
        }
        public async Task<double> GetLikePercentage(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = $"SELECT COUNT(CASE WHEN IsLiked = 1 THEN 1 END) AS Likes,COUNT(CASE WHEN IsLiked = 0 THEN 1 END) AS Dislikes FROM Reaction where ReviewId = '{id}';";
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
            DataSet reactData = new DataSet();
            adapter.Fill(reactData);
            DataRow dataRow = reactData.Tables[0].Rows[0];
            int Likes = Convert.ToInt32(dataRow["Likes"]);
            int Dislikes = Convert.ToInt32(dataRow["Dislikes"]);
            if(Likes + Dislikes == 0)
            {
                return 0;
            }
            return (Likes / (Likes + Dislikes)) * 100;



        }

        public async Task<bool> PostAsync(Reaction reaction)
        {     
           
            string insertSql = @"INSERT INTO Reaction(UserId, ReviewId, IsLiked, DateCreated, DateUpdated)
                     Values(@UserId, @ReviewId, @IsLiked, @DateCreated, @DateUpdated)";
            
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               using (var com = new SqlCommand(insertSql, connection))
                    {
                        com.Parameters.AddWithValue("@UserId", reaction.UserId);
                        com.Parameters.AddWithValue("@ReviewId", reaction.ReviewId);
                        com.Parameters.AddWithValue("@IsLiked", reaction.IsLiked);
                        com.Parameters.AddWithValue("@DateCreated", reaction.DateCreated);
                        com.Parameters.AddWithValue("@DateUpdated", reaction.DateUpdated);
                        connection.Open();
                        await com.ExecuteNonQueryAsync();
                    }
                   
                    connection.Close();
                    return true;

                

            }
        }

        public async Task<bool> PutAsync(Reaction reaction)
        {            
            reaction.DateUpdated = DateTime.UtcNow;

            string insertSql = @"UPDATE Reaction SET IsLiked=@IsLiked, DateUpdated=@DateUpdated WHERE UserId=@UserId AND ReviewId=@ReviewId";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                    using (var com = new SqlCommand(insertSql, connection))
                    {
                        com.Parameters.AddWithValue("@UserId", reaction.UserId);
                        com.Parameters.AddWithValue("@ReviewId", reaction.ReviewId);
                        com.Parameters.AddWithValue("@IsLiked", reaction.IsLiked);
                        com.Parameters.AddWithValue("@DateUpdated", reaction.DateUpdated);
                        connection.Open();
                        await com.ExecuteNonQueryAsync();
                    }

                    connection.Close();
                    return true;

                
            }
        }

        public async Task<bool> DeleteAsync(Guid userId, Guid reviewId)
        {
            string queryString = $"DELETE FROM Reaction WHERE UserId='{userId}' AND ReviewId='{reviewId}';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                    connection.Open();
                    SqlCommand myCommand = new SqlCommand(queryString, connection);
                    await myCommand.ExecuteNonQueryAsync();
                    connection.Close();
                    return true;
               

            }

        }

    }
}
