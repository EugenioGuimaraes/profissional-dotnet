namespace Profissional.Models.Domain
{
    public class Employee
    {  
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string RG { get; set; }
        public string Adress { get; set; }
        public decimal Salary { get; set; }
    }
}
