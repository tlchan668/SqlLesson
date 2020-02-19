using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary {
    public class Student {

        //properties
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int SAT { get; set; }
        public double GPA { get; set; }
        public int MajorId { get; set; }

        //needs connection private not accessed outside
        private BcConnection bcConnection;

        //create a method to get all students
        public List<Student> GetAllStudents() {
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
            return students;
        }

        public Student() { }
        public Student(BcConnection connection) {
            bcConnection = connection;
        }

    }
}
