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
   public  class MotorRepository : IMotorRepository
    {
        string connectionString = "Server=tcp:monoprojektdbserver.database.windows.net,1433;Initial Catalog = monoprojekt; Persist Security Info=False;User ID = matej; Password=Sifra1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
        public async Task<List<Motor>> GetAllAsync(MotorFilter filter,Sorting sort ,Paging paging)
        {
            
            StringBuilder queryString = new StringBuilder("select * from Motor where 1=1");
            if (!String.IsNullOrWhiteSpace(filter.Name)) 
            {
                queryString.Append($" and Name Like '%{filter.Name}%");
            }
            if (filter.Year > 0)
            {
                queryString.Append($" and Year = {filter.Year}");
            }
            if (!String.IsNullOrWhiteSpace(filter.Type)) 
            {
                queryString.Append($" and Type Like '%{filter.Type}%");
            }
            if (filter.EngineSize > 0)
            {
                queryString.Append($" and EngineSize = {filter.EngineSize}");
            }
            if (filter.MaxHP > 0)
            {
                queryString.Append($" and MaxHP = {filter.MaxHP}");
            }

            if (!String.IsNullOrWhiteSpace(sort.SortBy))
            {
                queryString.Append($" order by { sort.SortBy} { sort.SortMethod}");
            }
            if (paging.DontPage == false)
            {
                queryString.Append($" offset { paging.GetStartElement()} rows fetch next {paging.Rpp} rows only;");
            }

            List<Motor> motors = new List<Motor>();
           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand myCommand = new SqlCommand(queryString.ToString(), connection);
                SqlDataReader myReader = await myCommand.ExecuteReaderAsync();

                while (myReader.Read())
                {
                    Motor motor = new Motor();

                    motor.Id = (Guid)myReader["Id"];
                    motor.Year = int.Parse(myReader["Year"].ToString());
                    motor.Type = myReader["Type"].ToString();
                    motor.MaxHP = int.Parse(myReader["MaxHP"].ToString());
                    motor.EngineSize = int.Parse(myReader["EngineSize"].ToString());
                    motor.Name = myReader["Name"].ToString();
                    motor.DateCreated = (DateTime)myReader["DateCreated"];
                    motor.DateUpdated = (DateTime)myReader["DateUpdated"];

                    motors.Add(motor);
                }
                myReader.Close();
                connection.Close();
                return motors;

            }

        }


        public async Task<Motor> GetByIdAsync(Guid id)
        {
            string queryString = $"SELECT * FROM Motor WHERE id='{id}';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand myCommand = new SqlCommand(queryString, connection);
                SqlDataReader myReader = await myCommand.ExecuteReaderAsync();
                Motor motor = new Motor();
                while (myReader.Read())
                {
                    motor.Id = (Guid)myReader["Id"];
                    motor.Year = int.Parse(myReader["Year"].ToString());
                    motor.Type = myReader["Type"].ToString();
                    motor.MaxHP = int.Parse(myReader["MaxHP"].ToString());
                    motor.EngineSize = int.Parse(myReader["EngineSize"].ToString());
                    motor.Name = myReader["Name"].ToString();
                    motor.DateCreated = (DateTime)myReader["DateCreated"];
                    motor.DateUpdated = (DateTime)myReader["DateUpdated"];
                }
                connection.Close();
                return motor;

            }
        }
            public async Task<bool> PostAsync(Motor motor)
            {

                string insertSql = @"INSERT INTO Motor(Year, Type, MaxHP, EngineSize,Name,DateCreated,DateUpdated)
                     Values(@Year, @Type, @MaxHP, @EngineSize, @Name, @DateCreated, @DateUpdated)";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (var com = new SqlCommand(insertSql, connection))
                    {
                        com.Parameters.AddWithValue("@Year", motor.Year);
                        com.Parameters.AddWithValue("@Type", motor.Type);
                        com.Parameters.AddWithValue("@MaxHP", motor.MaxHP);
                        com.Parameters.AddWithValue("@EngineSize", motor.EngineSize);
                        com.Parameters.AddWithValue("@Name", motor.Name);
                        com.Parameters.AddWithValue("@DateCreated", motor.DateCreated);
                        com.Parameters.AddWithValue("@DateUpdated", motor.DateUpdated);
                        connection.Open();
                        await com.ExecuteNonQueryAsync();
                    }

                    connection.Close();
                    return true;



                }
            }

            public async Task<bool> PutAsync(Motor motor)
            {
            

                string insertSql = @"UPDATE motor SET Year=@Year,Type=@Type, MaxHP = @MaxHP, EngineSize = @EngineSize, Name=@Name ,DateUpdated=@DateUpdated WHERE Id=@Id";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                   
                        using (var com = new SqlCommand(insertSql, connection))
                        {
                            com.Parameters.AddWithValue("@Id", motor.Id);
                            com.Parameters.AddWithValue("@Year", motor.Year);
                            com.Parameters.AddWithValue("@Type", motor.Type);
                            com.Parameters.AddWithValue("@MaxHP", motor.MaxHP);
                            com.Parameters.AddWithValue("@EngineSize", motor.EngineSize);
                            com.Parameters.AddWithValue("@Name", motor.Name);
                            com.Parameters.AddWithValue("@DateCreated", motor.DateCreated);
                            com.Parameters.AddWithValue("@DateUpdated", motor.DateUpdated);
                            connection.Open();
                            await com.ExecuteNonQueryAsync();
                        }

                        connection.Close();
                        return true;
                                   
                }
            }

            public async Task<bool> DeleteAsync(Guid Id)
            {
                string queryString = $"DELETE FROM Motor WHERE Id='{Id}';";

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

            
        
    

