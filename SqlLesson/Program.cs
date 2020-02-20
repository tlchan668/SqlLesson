using System;
using SqlLibrary;

namespace SqlLesson {
    class Program {
        static void Main(string[] args) {
           

            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection=true");
            var student = new Student(sqllib);
            var students = student.GetAllStudents();

            foreach(var stud in students) {
                Console.WriteLine($"student id {stud.Id}, name {stud.Firstname} {stud.Lastname}");
            }

            sqllib.Disconnect();
        }
    }
}
