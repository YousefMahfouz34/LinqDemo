using Linqe.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Linqe
{
    internal class Program
    {
        static LinqeContext _context = new ();
        static void Main(string[] args)
        {
            #region Filtering
            //take()
            Console.WriteLine("take");

            var empslist = _context.Empolyee.Take(2);
            foreach (var item in empslist)
            {
                Console.WriteLine($"empolyeeName is {item.Name},\t Age :{item.Age}");
            }
            //skip
            Console.WriteLine("skip");

            var empslist2 = _context.Empolyee.Skip(2);
            foreach (var item in empslist2)
            {
                Console.WriteLine($"empolyeeName is {item.Name},\t Age :{item.Age}");
            }
            Console.WriteLine("take and skip");

            var empslist3 = _context.Empolyee.Skip(2).Take(2);
            foreach (var item in empslist3)
            {
                Console.WriteLine($"empolyeeName is {item.Name},\t Age :{item.Age}");
            }
            Console.WriteLine("takewhile");
            //var empslist4 = _context.Empolyee.TakeWhile(e=> e.Age > 24).ToList();
            //foreach (var item in empslist4)
            //{
            //    Console.WriteLine($"empolyeeName is {item.Name},\t Age :{item.Age}");
            //}
            #endregion

            #region Projecting

            //select data and change 
            var res = _context.Empolyee.Select(e => new { Id = e.Id, Name = e.Name });
            foreach (var item in res)
            {
                Console.WriteLine($"Id = {item.Id}\t Name={item.Name}");
            }
            //---------------------------------------------------
            //Subqueries and joins in EF Core with select
            var emolyeelist = _context.Department.Select(d => new { d.Name, Empolyeename = (_context.Empolyee.Where(e => e.DeptId == d.Id).Select(e => e.Name).ToList()) });
            foreach (var item in emolyeelist)
            {
                Console.WriteLine(item.Name);
                foreach (var i in item.Empolyeename)
                {
                    Console.WriteLine(i);
                }
            }
            //-----------------------------selectmany
            string[] fullNames = { "Anne Williams", "John Fred Smith", "Sue Green" };
            //The benefit of SelectMany is that it yields a single flat result sequence.
            var fullnamesplitbyselectmany = fullNames.SelectMany(s => s.Split());
            foreach (var item in fullnamesplitbyselectmany)
            {
                Console.WriteLine(item);
            }
            var fullnamesplitbyselect = fullNames.Select(s => s.Split());
            foreach (var item in fullnamesplitbyselect)
            {
                foreach (var item1 in item)
                {
                    Console.WriteLine(item1);
                }

            }
            //------------------------------------------------------
            //joining with selectmany
            string[] players = { "Tom", "Jay", "Mary" };
            var query = from name1 in players
                        from name2 in players
                            // where name1 != name2
                        where name1.CompareTo(name2) > 0
                        orderby name1, name2
                        select name1 + " vs " + name2;
            foreach (var item in query)
            {
                Console.WriteLine(item);

            }
            #endregion
            #region Joinning
            var query2 = from e in _context.Empolyee
                         join
                          d in _context.Department
                         on e.DeptId equals d.Id
                         select e.Name + " in department  " + d.Name;
        
            var fluentuery = _context.Empolyee.Join(_context.Department, e => e.DeptId, d => d.Id, (e, d) => new {EmpName= e.Name,DeptName= d.Name });
            foreach (var item in query2)
            {
                Console.WriteLine(item);
                
            }
            foreach (var item in fluentuery)
            {
                Console.WriteLine(item);

            }


            #endregion
            #region ordering
            var orderdlist=_context.Empolyee.OrderBy(e=>e.Salary).ThenBy(e=>e.Name);
            foreach (var item in orderdlist)
            {
                Console.WriteLine($" id :{item.Id}\t,name :{item.Name}\t,salary:{item.Salary}");
                
            }
            #endregion
            #region Grouping
            var groupingquery = _context.Empolyee.GroupBy(e => e.DeptId).Select(e => new { Deptid = e.Key, count = e.Count(), Sumofsalary=e.Sum(s=>s.Salary) }).OrderByDescending(e=>e.count);
            foreach (var item in groupingquery)
            {
                Console.WriteLine($"DebtId :{item.Deptid }\t count :{item.count} \t sumofsalary :{item.Sumofsalary}");
                
            }
            foreach(var item in _context.Empolyee.ToList().Chunk(3))
            {
                Console.WriteLine(item);
                foreach (var item1 in item)
                {
                    Console.WriteLine(item1.Name);
                }
                
            }
            #endregion
            #region Set operator
            string[] seq1 = { "A", "b", "C" };
            string[] seq2 = { "a", "B", "c" };
            var union = seq1.UnionBy(seq2,c=>c.ToUpperInvariant());            foreach (var item in union)
            {
                Console.WriteLine(item);
                
            }            int[] seq1num = { 1, 2, 3 }, seq2num = { 3, 4, 5 };

            var commonality = seq1num.Intersect(seq2num);  //3
            var difference1 = seq1num.Except(seq2num);//1 , 2
            var difference2 = seq2num.Except(seq1num); // 4,5




            #endregion
            #region conversion methods
            int[] numar = {1,2,3,4,5,6,7};
            IEnumerable<string>enumlist=numar.OfType<string>();
            foreach (var item in enumlist)
            {
                Console.WriteLine(item);
            }
            //throw exception 
            //IEnumerable<string> enumlist2 = numar.Cast<string>();
            //foreach (var item in enumlist2)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion
            #region Element operators
            //var Firstempolyee = _context.Empolyee.FirstOrDefault(e => e.Salary > 50000);
            //Console.WriteLine(Firstempolyee);
            //throw new Exception
            //var Firstempolyee1 = _context.Empolyee.First(e => e.Salary > 50000);
            //Console.WriteLine(Firstempolyee1.Name);
            //var lastempolyee = _context.Empolyee.LastOrDefault(e => e.Salary == 20000 );
            //Console.WriteLine(lastempolyee.Name);
            var emp=_context.Empolyee.Min(e=>e.Salary);
            Console.WriteLine(emp.Value);



            #endregion
            #region Aggregation Methods            int countofempolyee = _context.Empolyee.Count();
             Console.WriteLine(countofempolyee);
             decimal sumsalary=(decimal)  _context.Empolyee.Sum(e=>e.Salary);
            Console.WriteLine(sumsalary);





            #endregion
            #region Quientfires
            bool hasAThree = new int[] { 2, 3, 4 }.Contains(3);
            Console.WriteLine(hasAThree);
            bool hasAThree2 = new int[] { 2, 3, 4 }.Any(n => n == 3); // true;
            var query5 = _context.Empolyee.Any(p => p.Salary > 1000);
            Console.WriteLine(query5);
            var query6 = _context.Empolyee.All(p => p.Salary > 20000);
            Console.WriteLine(query6);
            #endregion

        }
    }
}
