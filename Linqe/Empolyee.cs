using System.ComponentModel.DataAnnotations.Schema;

namespace Linqe
{
    public class Empolyee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Salary { get; set; }
        public int? Age { get; set; }
        [ForeignKey(nameof(Department))]
        public int? DeptId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
