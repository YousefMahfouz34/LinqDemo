namespace Linqe
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
      public virtual ICollection<Empolyee> Empolyees{ get; set; }
    }
}
