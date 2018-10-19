using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntryProject.Model
{
    [Table("Employees")]
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Duty> Duties { get; set; }
    }


    [Table("Duties")]
    public class Duty
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        [ForeignKey(typeof(Employee))]
        public int EmployeeId { get; set; }

        [ManyToOne]      // Many to one relationship with Stock
        public Employee Stock { get; set; }
    }


}
