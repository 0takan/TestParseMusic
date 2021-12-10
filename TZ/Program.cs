using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TZ
{
    public class Program
    {
        static void Main(string[] args)
        {
            var JsonList = JsonConvert.DeserializeObject<List<Model>>(File.ReadAllText("data.json"));
            var departmentList = JsonList.Select(x => x.department).ToList();
            var departmentUnique = departmentList.Distinct().ToList();
            int departmentCount = departmentUnique.Count();
            departmentUnique.Sort();

            int list = 1;
            Console.WriteLine("Choose department for report:");
            foreach (var department in departmentUnique)
            {
                Console.WriteLine($"{list} -> {department}");
                list++;
            }
            try
            {
                int numOfDepartment = Convert.ToInt32(Console.ReadLine());
                if (numOfDepartment < 0 || numOfDepartment > departmentCount)
                {
                    throw new Exception("ErrorValue");
                }
                //Task1
                float sum = 0;
                var salary = JsonList.Where(x => x.department == departmentUnique[numOfDepartment - 1]).Select(x => x.salary);
                foreach (float i in salary)
                {
                    sum += i;
                }
                Console.WriteLine("-------------------- Salary sum --------------------");
                Console.WriteLine($"Salary sum in {departmentUnique[numOfDepartment - 1]} departament is {sum}\n");
                //Task2
                Console.WriteLine("-------------------- Employee with max salary --------------------");
                var salaryMax = JsonList.Find(x => x.salary == JsonList.Max(x => x.salary));
                Console.WriteLine($"Max salary in {departmentUnique[numOfDepartment - 1]} departament got {salaryMax.first_name} {salaryMax.last_name} ->" +
                            $" {salaryMax.salary}\n");
                //Task3
                Console.WriteLine("-------------------- Employee with min salary --------------------");
                var salaryMin = JsonList.Find(x => x.salary == JsonList.Min(x => x.salary));
                Console.WriteLine($"Min salary in {departmentUnique[numOfDepartment - 1]} departament got {salaryMin.first_name} {salaryMin.last_name} ->" +
                            $" {salaryMin.salary}\n");
                //Task4
                Console.WriteLine("-------------------- Employees by position --------------------\n");
                Console.WriteLine($"{departmentUnique[numOfDepartment - 1]} contains next position:\n");
                foreach (var position in JsonList.Where(x => x.department == departmentUnique[numOfDepartment - 1]).Select(x => x.position).Distinct())
                {
                    var a = JsonList.Where(x => x.position == position && x.department == departmentUnique[numOfDepartment - 1]);
                    Console.WriteLine("  "  + position);
                    foreach (var item in a)
                    {
                        Console.WriteLine($"\t\t{item.first_name} {item.last_name} -> {item.salary}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fals value. Try again. Time{0}",
                         DateTime.Now.ToString(" HH:mm:ss"), ex);
            }
            //keep it simple =)
        }
    }
}
