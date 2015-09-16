using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Problems_01_06
{
    internal class Homework
    {
        private static void Main()

        {

            var context = new SoftUniEntities();

            //Problem 3. Database Search Queries

            //1.Find all employees who have projects started in the time period 2001 - 2003 (inclusive). 
            //Select the project's name, start date, end date and manager name.

            var allEmployees = context.Employees.Join(
                context.Employees,
                (e => e.ManagerID),
                (m => m.EmployeeID),
                (e, m) => new
                {
                    Employee = e,
                    ManagerName = m.FirstName + " " + m.LastName,
                    Projects =
                        e.Projects.Where(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003).
                            Select(x =>
                                new
                                {
                                    x.Name,
                                    x.StartDate,
                                    x.EndDate
                                })
                }
                ).ToList();


            foreach (var employee in allEmployees)
            {
                foreach (var project in employee.Projects)
                {
                    Console.WriteLine(project.Name + " " + project.StartDate + " - " + project.EndDate + " Manager:" +
                                      employee.ManagerName);
                }




                //2. Find all addresses, ordered by the number of employees who live there (descending), then by town name (ascending). 
                //Take only the first 10 addresses and select their address text, town name and employee count. 

                var addresses = context.Addresses.Join
                    (context.Towns,
                        a => a.TownID,
                        t => t.TownID,
                        (a, t) => new
                        {
                            Address = a.AddressText,
                            Town = t.Name,
                            EmployeeCount = context.Employees.Count(e => e.AddressID == a.AddressID)
                        }).OrderByDescending(a => a.EmployeeCount).ThenBy(a => a.Town).Take(10);

                foreach (var address in addresses)
                {
                    Console.WriteLine("{0}, {1} - {2} employees",
                        address.Address,
                        address.Town,
                        address.EmployeeCount);
                }




                //3. Get an employee by id (e.g. 147). Select only his/her first name, last name, job title and projects 
                //(only their names). The projects should be ordered by name (ascending).

                var totalEmployees = from emp in context.Employees
                    select new
                    {
                        emp.FirstName,
                        emp.LastName,
                        emp.JobTitle,
                        Projects = emp.Projects.Select(p => new {p.Name}).OrderBy(p => p.Name)
                    };

                foreach (var emp in totalEmployees)
                {
                    Console.WriteLine("{0} {1} - {2}, Projects: ",
                        emp.FirstName,
                        emp.LastName,
                        emp.JobTitle);
                    foreach (var project in employee.Projects)
                    {
                        Console.WriteLine(project.Name);
                    }
                    Console.WriteLine();
                }

                //4. Find all departments with more than 5 employees. Order them by employee count (ascending). 
                //Select the department name, manager name and first name, last name, hire date and job title of every employee.



                var departments = from dept in context.Departments.Join(
                    context.Employees,
                    (d => d.ManagerID),
                    (m => m.EmployeeID),
                    (d, m) => new
                    {
                        DepartmentName = d.Name,
                        ManagerName = m.LastName,
                        EmployeeCount = context.Employees.Count(e => e.DepartmentID == d.DepartmentID)
                    })
                    where dept.EmployeeCount > 5
                    orderby dept.EmployeeCount
                    select dept;

                foreach (var dept in departments)
                {
                    Console.WriteLine("--{0} - Manager: {1}, Employees: {2}",
                        dept.DepartmentName,
                        dept.ManagerName,
                        dept.EmployeeCount);

                }




//Problem 4. Native SQL Query

                //Using native SQL query

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                //var context = new SoftUniEntities();
                var workers =
                    context.Database.SqlQuery<Employee>(
                        "SELECT * FROM Employees e JOIN EmployeesProjects ep ON ep.EmployeeID = e.EmployeeID JOIN Projects p ON p.ProjectID = ep.ProjectID WHERE year(p.StartDate) = '2002'");
                stopwatch.Stop();
                Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                foreach (var worker in workers)
                {
                    Console.WriteLine(worker.FirstName + " " + worker.LastName);
                }

                //Using LINQ query

                Stopwatch stopwatch2 = new Stopwatch();
                stopwatch2.Start();
                var context2 = new SoftUniEntities();
                var employees2 =
                    context2.Employees.Select(
                        e => new {e.FirstName, e.LastName, Projects = e.Projects.Select(p => p.StartDate.Year == 2002)});

                stopwatch2.Stop();
                Console.WriteLine("Time elapsed: {0}", stopwatch2.Elapsed);
                foreach (var emp in employees2)
                {
                    Console.WriteLine(emp.FirstName + " " + emp.LastName);
                }


//Problem 6. Call a Stored Procedure

                var projects = context.usp_GetProjectsByEmployees("Ruth", "Ellerbrock");
                foreach (var project in projects)
                {

                    Console.WriteLine(project);

                }
            }
        }
    }
}
