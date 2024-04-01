using Linqe.Context;

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

            #endregion



        }
    }
}
