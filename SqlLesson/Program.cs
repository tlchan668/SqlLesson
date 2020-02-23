using System;
using SqlLibrary;

namespace SqlLesson {
    class Program {
        static void Main(string[] args) {
           

            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection=true");

            InstructorController.bcConnection = sqllib;
            var instructors = InstructorController.GetAllInstructors();
            foreach(var instrctor in instructors) {
                Console.WriteLine(instrctor);
            }

            var instructor = InstructorController.GetInstructorByPK(10);
            Console.WriteLine(instructor);

            var NewInstruct = new Instructor {
                Id = 803,
                Firstname = "Katarina",
                Lastname = "Chan",
                YearsExperience = 1,
                IsTenured = false
            };
            var success = InstructorController.InsertInstructor(NewInstruct);

            var instructors1 = InstructorController.GetAllInstructors();
            foreach (var instrctor in instructors1) {
               Console.WriteLine(instrctor);
            }

            NewInstruct.Lastname = "Figart";

            success = InstructorController.UpdateInstructor(NewInstruct);
            instructors1 = InstructorController.GetAllInstructors();
            foreach (var instrctor in instructors1) {
                Console.WriteLine(instrctor);
            }

            success = InstructorController.DeleteInstructor(NewInstruct);
            instructors1 = InstructorController.GetAllInstructors();
            foreach (var instrctor in instructors1) {
                Console.WriteLine(instrctor);
            }

            MajorController.bcConnection = sqllib;

            /*var majors = MajorController.GetAllMajors();
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
            */
            //var student = new Student(sqllib);//getting bcconnection into our student 
            /*StudentController.bcConnection = sqllib;//setting this prop connection for whole class

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

            var newStudent = new Student {
                Id = 891,
                Firstname = "Wilma",
                Lastname = "Flintsone",
                SAT = 1100,
                GPA = 3.00,
                MajorId = 1
            };
            var success = StudentController.InsertStudent(newStudent);

            newStudent.Firstname = "Charlie";//property change
            newStudent.Lastname = "Chan";
            success = StudentController.UpdateStudent(newStudent);
            Console.WriteLine($"update {newStudent}");

            /*var studentToDelete = new Student {
                Id = 891
            };*/
            /*
            success = StudentController.DeleteStudent(891);
           
            var students1 = StudentController.GetAllStudents();
            foreach (var student0 in students1) {
                Console.WriteLine(student0);
            }
            */
            sqllib.Disconnect();
            

        }
    }
}
