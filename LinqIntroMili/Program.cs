using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinqIntroMili
{
    public class Program
    {
        public static void Main(string[] args)
        {
            QueryEmployees();
            QueryTypes();
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
