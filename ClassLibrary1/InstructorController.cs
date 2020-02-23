using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary {
    public class InstructorController {

        public static BcConnection bcConnection { get; set; }

        private static Instructor LoadInstructorInstance(SqlDataReader reader) {
            var instructor = new Instructor();
            instructor.Id = Convert.ToInt32(reader["Id"]);
            instructor.Firstname = reader["Firstname"].ToString();
            instructor.Lastname = reader["Lastname"].ToString();
            instructor.YearsExperience = Convert.ToInt32(reader["YearsExperience"]);
            instructor.IsTenured = Convert.ToBoolean(reader["IsTenured"]);
            return instructor;
        }
        
        private static void LoadInstructorValues (SqlCommand command, Instructor instructor) {
            command.Parameters.AddWithValue("@Id", instructor.Id);
            command.Parameters.AddWithValue("@Firstname", instructor.Firstname);
            command.Parameters.AddWithValue("@Lastname", instructor.Lastname);
            command.Parameters.AddWithValue("@YearsExperience", instructor.YearsExperience);
            command.Parameters.AddWithValue("@IsTenured", instructor.IsTenured);
           
        }
        private static SqlCommand LoadInstructorValues(string sql, Instructor instructor) {
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", instructor.Id);
            command.Parameters.AddWithValue("@Firstname", instructor.Firstname);
            command.Parameters.AddWithValue("@Lastname", instructor.Lastname);
            command.Parameters.AddWithValue("@YearsExperience", instructor.YearsExperience);
            command.Parameters.AddWithValue("@IsTenured", instructor.IsTenured);

            return command;

        }


        public static List<Instructor> GetAllInstructors() {
            var sql = "select * from Instructor;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                Console.WriteLine("No Instructors");
                reader.Close();
                reader = null;
                return new List<Instructor>();
            }
            var instructors = new List<Instructor>();
            while (reader.Read()) {
                var instructor = LoadInstructorInstance(reader);
               /* var instructor = new Instructor();
                instructor.Id = Convert.ToInt32(reader["Id"]);
                instructor.Firstname = reader["Firstname"].ToString();
                instructor.Lastname = reader["Lastname"].ToString();
                instructor.YearsExperience = Convert.ToInt32(reader["YearsExperience"]);
                instructor.IsTenured = Convert.ToBoolean(reader["IsTenured"]);*/
                instructors.Add(instructor);
            }
            reader.Close();
            reader = null;
            return instructors;
        }

        public static Instructor GetInstructorByPK(int id) {
            var sql = $"select * from Instructor where Instructor.id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", id);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                return null;
            }
            reader.Read();
            var instructor = LoadInstructorInstance(reader);
            reader.Close();
            reader = null;
            return instructor;
        }

        public static bool InsertInstructor (Instructor instructor) {
            var sql = $"insert into Instructor(id, Firstname,Lastname,YearsExperience,IsTenured) " +
                        $" VALUES(@Id, @Firstname, @LastName, @YearsExperience, @IsTenured);";
            var command = LoadInstructorValues(sql, instructor);
         //   var command = new SqlCommand(sql, bcConnection.Connection);
         //   LoadInstructorValues(command, instructor);
            //command.Parameters.AddWithValue("@Id", instructor.Id);
            //command.Parameters.AddWithValue("@Firstname", instructor.Firstname);
            //command.Parameters.AddWithValue("@Lastname", instructor.Lastname);
            //command.Parameters.AddWithValue("@YearsExperience", instructor.YearsExperience);
            //command.Parameters.AddWithValue("@IsTenured", instructor.IsTenured);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Instructor insert failed");
            }
            return true;
        }
        
        public static  bool UpdateInstructor(Instructor instructor) {
            var sql = "UPDATE Instructor Set " +
                        " Id = @Id, " +
                        " Firstname = @Firstname, " +
                        " Lastname = @Lastname, " +
                        " YearsExperience = @YearsExperience, " +
                        " IsTenured = @IsTenured " +
                        " where id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            LoadInstructorValues(command, instructor);
            //command.Parameters.AddWithValue("@Id", instructor.Id);
            //command.Parameters.AddWithValue("@Firstname", instructor.Firstname);
            //command.Parameters.AddWithValue("@Lastname", instructor.Lastname);
            //command.Parameters.AddWithValue("@YearsExperience", instructor.YearsExperience);
            //command.Parameters.AddWithValue("@IsTenured", instructor.IsTenured);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1){
                throw new Exception("Instructor Update Failed");
            }
            return true;
        }

        public static bool DeleteInstructor(Instructor instructor) {
            var sql = "DELETE from Instructor where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", instructor.Id);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Delete Instructor Failed");
            }
            return true;
        }
    }
}
