using P01_StudentSystem.Data;
using System;

namespace P01_StudentSystem.App
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new StudentSystemContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
