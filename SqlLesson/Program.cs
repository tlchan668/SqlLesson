using System;
using SqlLibrary;

namespace SqlLesson {
    class Program {
        static void Main(string[] args) {
           

            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection=true");
            //var student = new Student(sqllib);//getting bcconnection into our student 
            StudentController.bcConnection = sqllib;//setting this prop connection for whole class

            var newStudent = new Student {
                Id = 888,
                Firstname = "Crazy",
                Lastname = "Eights",
                SAT = 600,
                GPA = 0.00,
                MajorId = null
             };

            var success = StudentController.InsertStudent(newStudent);


            var student = StudentController.GetStudentByPK(888);
            if (student == null) {
                Console.WriteLine("Student not found");
            } else {
                Console.WriteLine(student);
            }

            var students = StudentController.GetAllStudents();
            foreach (var student0 in students) {
                Console.WriteLine(student0);
            }

            sqllib.Disconnect();
            

        }
    }
}
