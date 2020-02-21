using System;
using SqlLibrary;

namespace SqlLesson {
    class Program {
        static void Main(string[] args) {
           

            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection=true");

            MajorController.bcConnection = sqllib;

            var majors = MajorController.GetAllMajors();
            foreach(var major0 in majors) {
                Console.WriteLine(major0);
            }

            var major = MajorController.GetMajorByPK(5);
            if(major == null) {
                Console.WriteLine("No major found");
            } else {
                Console.WriteLine(major);
            }
            var newMajor = new Major {
                Id = 8,
                Description = "General",
                MinSat = 500
            };
            var success = MajorController.InsertMajor(newMajor);
            newMajor.MinSat = 600;
            success = MajorController.UpdateMajor(newMajor);

            var maj = MajorController.GetAllMajors();
            foreach (var majo in maj) {
                Console.WriteLine(majo);
            }

            success = MajorController.DeleteMajor(newMajor);
            foreach (var major0 in majors) {
                Console.WriteLine(major0);
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

            var students = StudentController.GetAllStudents();
            foreach (var stud in students) {
                Console.WriteLine(stud);
            }
            var student = StudentController.GetStudentByPK(100);
            if (student == null) {
                Console.WriteLine("Student not found");
            } else {
                Console.WriteLine(student);
            }
            /*
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
            }*/

            sqllib.Disconnect();
            

        }
    }
}
