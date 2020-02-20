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
            var sql = "SELECT * From Student";
            //create another sqlobject called sqlcommand
            var command = new SqlCommand(sql, bcConnection.Connection);
            //execute it should return a sqlreader
            var reader = command.ExecuteReader();
            //if executes success have rows in reader
            if (!reader.HasRows) {
                Console.WriteLine("no rows from GetAllStudents()");
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
            var majorid = "";
            if (student.MajorId == null) {
                majorid = "NULL";
            } else {
                majorid = student.MajorId.ToString();//change to string but when put in without quotes below will become 5
            }
            var sql = $"insert into Student(id, Firstname, Lastname, SAT,GPA, MajorId) " +
                        $" values({student.Id}, '{student.Firstname}', '{student.Lastname}', {student.SAT}, {student.GPA}, {majorid}); ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                //error, something went wrong throw an exception
                throw new Exception("Insert failed");
            }
            return true;//tell caller it worked
        }
    }
}
