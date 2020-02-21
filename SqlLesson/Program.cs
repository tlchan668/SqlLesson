using System;
using SqlLibrary;

namespace SqlLesson {
    class Program {
        static void Main(string[] args) {
           

            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection=true");

            MajorController.bcConnection = sqllib;

            var majors = MajorController.GetAllMajors();
            foreach(var major in majors) {
                Console.WriteLine(major);
            }

            //var student = new Student(sqllib);//getting bcconnection into our student 
            StudentController.bcConnection = sqllib;//setting this prop connection for whole class

            //var newStudent = new Student {
            //    Id = 891,
            //    Firstname = "Wilma",
            //    Lastname = "Flintsone",
            //    SAT = 1100,
            //    GPA = 3.00,
            //    MajorId = 1
            // };

            //var success = StudentController.InsertStudent(newStudent);


            var student = StudentController.GetStudentByPK(100);
            if (student == null) {
                Console.WriteLine("Student not found");
            } else {
                Console.WriteLine(student);
            }

            //student.Firstname = "Charlie";//property change
            //student.Lastname = "Chan";
            //var success = StudentController.UpdateStudent(student);
            Console.WriteLine($"u{student}");

            var studentToDelete = new Student {
                Id = 999
            };

            //success = StudentController.DeleteStudent(891);

            var students = StudentController.GetAllStudents();
            foreach (var student0 in students) {
                Console.WriteLine(student0);
            }

            sqllib.Disconnect();
            

        }
    }
}
