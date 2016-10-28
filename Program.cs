using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Diagnostics;


namespace LinqIntro
{
    class Program
    {
        public static void Main()
        {
            QuerySqlServer();
            QueryXml();
            QueryEmployees();
            QueryTypes();
        }

        private static void QuerySqlServer()
        {
            MovieReviewsDataContext dc = new MovieReviewsDataContext();

            IEnumerable<Movie> topMovies =
                from m in dc.Movies
                where m.ReleaseDate.Year > 2006
                orderby m.Reviews.Average(r => r.Rating) descending
                select m;

            foreach (Movie movie in topMovies)
            {
                Console.WriteLine(movie.Title);
            }
        }

            
        private static void QueryXml()
        {
            XDocument doc = new XDocument(
                new XElement("Processes",
                    from p in Process.GetProcesses()
                    orderby p.ProcessName ascending
                    select new XElement("Process",
                        new XAttribute("Name", p.ProcessName),
                        new XAttribute("PID", p.Id))));

            IEnumerable<int> pids =
                from e in doc.Descendants("Process")
                where e.Attribute("Name").Value == "devenv"
                orderby (int)e.Attribute("PID") ascending
                select (int)e.Attribute("PID");

            foreach (int id in pids)
            {
                Console.WriteLine(id);
            }
        }

        private static void QueryTypes()
        {
            IEnumerable<string> publicTypes =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsPublic
                select t.FullName;

            foreach (string name in publicTypes)
            {
                Console.WriteLine(name);
            }
        }

        private static void QueryEmployees()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee { ID=1, Name="Scott", HireDate=new DateTime(2002, 3, 5) },
                new Employee { ID=2, Name="Poonam", HireDate=new DateTime(2002, 10, 15) },
                new Employee { ID=3, Name="Paul", HireDate=new DateTime(2007, 10, 11) }
            };

            IEnumerable<Employee> query =
                from e in employees
                where e.HireDate.Year < 2005
                orderby e.Name
                select e;

            employees.Add(new Employee { ID = 4, Name = "Linda", HireDate = new DateTime(2003, 6, 11) });

            foreach (Employee e in query)
            {
                Console.WriteLine(e.Name);
            }
        }
    }
}
