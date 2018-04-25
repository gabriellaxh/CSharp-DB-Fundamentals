using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Mankind
{
    class Startup
    {
        static void Main(string[] args)
        {
            try
            {
                var student = Console.ReadLine().Split();
                var studentInfo = new Student(student[0], 
                                              student[1], 
                                              student[2]);

                var worker = Console.ReadLine().Split();
                var workerInfo = new Worker(worker[0], 
                                            worker[1], 
                                            decimal.Parse(worker[2]), 
                                            decimal.Parse(worker[3]));

                Console.WriteLine(studentInfo);
                Console.WriteLine();
                Console.WriteLine(workerInfo);
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
