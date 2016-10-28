using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqIntroMili
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Employee> employees = new List<Employee>()
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

            foreach (var employee in query)
            {
                Console.WriteLine(employee.Name);
            }
        }
    }
}
