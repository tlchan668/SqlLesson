using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SqlLibrary {
    public class MajorController {

        public static BcConnection bcConnection { get; set; }

        private static Major LoadMajorInstance(SqlDataReader reader) {
            var major = new Major();
            major.Id = Convert.ToInt32(reader["Id"]);
            major.Description = reader["Description"].ToString();
            major.MinSat = Convert.ToInt32(reader["MinSat"]);
            return major;
        }

        public static List<Major> GetAllMajors() {
            var sql = "Select * from Major; ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                Console.WriteLine("No rows for GetAllMajors");
                return new List<Major>();
            }
            var majors = new List<Major>();
            while (reader.Read()) {
                var major = LoadMajorInstance(reader);
               /* var major = new Major();
                major.Id = Convert.ToInt32(reader["Id"]);
                major.Description = reader["Description"].ToString();
                major.MinSat = Convert.ToInt32(reader["MinSat"]);  refactoring*/
                majors.Add(major);
            }
            reader.Close();
            reader = null;
            return majors;
        }

        public static Major GetMajorByPK(int id) {
            var sql = $"Select * from major where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", id);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                return null;
            }
            reader.Read();
            var major = LoadMajorInstance(reader);
            /*var major = new Major();
            major.Id = Convert.ToInt32(reader["Id"]);
            major.Description = reader["Description"].ToString();
            major.MinSat = Convert.ToInt32(reader["MinSat"]);*/
            reader.Close();
            reader = null;
            return major;
        }

        public static bool InsertMajor(Major major) {
            var sql = $"INSERT into Major (id, Description, MinSat) " +
                        $" values(@Id, @Description, @MinSat); ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", major.Id);
            command.Parameters.AddWithValue("@Description", major.Description);
            command.Parameters.AddWithValue("@MinSat", major.MinSat);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Major Insert failed");
            }
            return true;
        }

        public static bool UpdateMajor(Major major) {
            var sql = "UPDATE Major set " +
                        " Id = @Id, Description = @Description, MinSat = @Minsat where id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", major.Id);
            command.Parameters.AddWithValue("@Description", major.Description);
            command.Parameters.AddWithValue("@MinSat", major.MinSat);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Major Insert failed");
            }
            return true;
        }

        public static bool DeleteMajor(Major major) {
            var sql  = "Delete from Major " +
                          "where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue(@"Id", major.Id);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Major Insert failed");
            }
            return true;
        }     

        public static bool DeleteMajor (int id) {
            var major = GetMajorByPK(id);
            if(major == null) {
                return false;
            }
            var success = DeleteMajor(major);
            return true;
        }


        
    }
}
