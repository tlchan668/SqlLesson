using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary {
    public class StudentController {

        public static BcConnection bcConnection { get; set; }

        //create a method to get all students
        public static List<Student> GetAllStudents() {
            //issue a sql statment to give all students
            //turn into join view with major
            var sql = "SELECT * From Student s Left Join Major m on m.id = s.MajorId";
            //create another sqlobject called sqlcommand
            var command = new SqlCommand(sql, bcConnection.Connection);
            //execute it should return a sqlreader
            var reader = command.ExecuteReader();
            //if executes success have rows in reader
            if (!reader.HasRows) {
                Console.WriteLine("no rows from GetAllStudents()");
                reader.Close();
                reader = null;
                return new List<Student>();//returns an emply list
            }
            var students = new List<Student>();
            while (reader.Read()) {
                //while getting next row keep on
                var student = new Student();
                student.Id = Convert.ToInt32(reader["Id"]);//gives value as string
                student.Firstname = reader["Firstname"].ToString();
                student.Lastname = reader["Lastname"].ToString();
                student.SAT = Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToDouble(reader["GPA"]);
                // student.MajorId = Convert.ToInt32(reader["MajorId"]);
                if (Convert.IsDBNull(reader["Description"])) {
                    student.Major = null;
                } else {
                    var major = new Major {
                        Description = reader["Description"].ToString(),
                        MinSat = Convert.ToInt32(reader["MinSat"])
                    };
                    student.Major = major;
                }
                //add to collection students
                students.Add(student);
            }
            reader.Close();
            reader = null;
            return students;
        }

        //create another static method to bring back one student with primary key
        public static Student GetStudentByPK(int id) {
            var sql = $"SELECT * from Student Where Id = {id}";//if sting would be '{id}'
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                return null;
            }
            reader.Read();
            var student = new Student();
            student.Id = Convert.ToInt32(reader["Id"]);//gives value as string
            student.Firstname = reader["Firstname"].ToString();
            student.Lastname = reader["Lastname"].ToString();
            student.SAT = Convert.ToInt32(reader["SAT"]);
            student.GPA = Convert.ToDouble(reader["GPA"]);
            // student.MajorId = Convert.ToInt32(reader["MajorId"]);
            reader.Close();
            reader = null; 
            return student;
        }

        //insert data add a record
        public static bool InsertStudent(Student student) {
            var majorid = "";//not necessary with parameterized
            if (student.MajorId == null) {
                majorid = "NULL";
            } else {
                majorid = student.MajorId.ToString();//change to string but when put in without quotes below will become 5
            }
            //this works but has potential problems. do not use interpolated string=>parameters
            /*var sql = $"insert into Student(id, Firstname, Lastname, SAT,GPA, MajorId) " +
                        $" values({student.Id}, '{student.Firstname}', '{student.Lastname}', {student.SAT}, {student.GPA}, {majorid}); ";*/
            var sql = $"insert into Student(id, Firstname, Lastname, SAT,GPA, MajorId) " +
                        $" values(@Id, @Firstname, @Lastname, @SAT, @GPA, @Majorid); ";
            //sql parameters
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@Firstname", student.Firstname);
            command.Parameters.AddWithValue("@Lastname", student.Lastname);
            command.Parameters.AddWithValue("@SAT", student.SAT);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@MajorId", student.MajorId ?? Convert.DBNull);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                //error, something went wrong throw an exception
                throw new Exception("Insert failed");
            }
            return true;//tell caller it worked
        }

        public static bool UpdateStudent(Student student) {
            //want to read from student then change what values you want and then update
            var sql = "UPDATE Student Set " +
                        " Firstname = @Firstname, " +
                        " Lastname = @Lastname, " +
                        " SAT = @SAT, " +
                        " GPA = @GPA, " +
                        " MajorId = @MajorId " +
                        " where id = @Id; ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@Firstname", student.Firstname);
            command.Parameters.AddWithValue("@Lastname", student.Lastname);
            command.Parameters.AddWithValue("@SAT", student.SAT);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@MajorId", student.MajorId ?? Convert.DBNull);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                //error, something went wrong throw an exception
                throw new Exception("Update failed");
            }
            return true;//tell caller it worked
        }

        public static bool DeleteStudent(Student student) {
            var sql = "DELETE from Student " +
                        " where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1){
                throw new Exception("Delete Failed");
            }
            return true;
        }

        public static bool DeleteStudent (int id) {
            var student = GetStudentByPK(id);
            if (student == null) {
                return false;
            }
            var success = DeleteStudent(student);
            return true;
        }
    }
}
