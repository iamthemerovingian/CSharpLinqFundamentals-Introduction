using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqIntroMili
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //QueryEmployees();
            //QueryTypes();
            //QueryXML();
            QuerySqlServer();
        }

        private static void QuerySqlServer()
        {
            
        }

        private static void QueryXML()
        {
            XDocument doc = new XDocument(
                new XElement("Processes",
                from p in Process.GetProcesses()
                orderby p.ProcessName ascending
                select new XElement("Process",
                    new XAttribute("Name", p.ProcessName),
                    new XAttribute("PID", p.Id))));

            IEnumerable<int> pid =
                from e in doc.Elements("Processes").Elements("Process")
                where e.Attribute("Name").Value == "devenv"
                orderby (int)e.Attribute("PID") ascending
                select (int)e.Attribute("PID");

            foreach (var item in pid)
            {
                Console.WriteLine(item);
            }
        }

        private static void QueryTypes()
        {
            IEnumerable<string> publicTypes =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsPublic
                select t.FullName;

            foreach (var process in publicTypes)
            {
                Console.WriteLine(process);
            }
        }

        private static void QueryEmployees()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee {ID=1, Name="Scott", HireDate=new DateTime(2002,3,5) },
                new Employee {ID=2, Name="Poonam", HireDate=new DateTime(2002,10,15) },
                new Employee {ID=3, Name="Paul", HireDate=new DateTime(2007,10,11) },
            };

            IEnumerable<Employee> query =
                from e in employees
                where e.HireDate.Year < 2005
                orderby e.HireDate
                select e;

            Console.WriteLine("-------------before adding-----------");
            foreach (var employee in query)
            {
                Console.WriteLine(employee.Name);
            }

            employees.Add(new Employee { ID = 4, Name = "Linda", HireDate = new DateTime(2003, 6, 11) });
            Console.WriteLine("-------------After Adding adding-----------");

            foreach (var employee in query)
            {
                Console.WriteLine(employee.Name);
            }
        }
    }
}
