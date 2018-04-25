namespace SalesStartUp
{
    using P03_SalesDatabase;
    using System;

    class StartUp
    {
        static void Main(string[] args)
        {
            using(var db = new SalesContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
