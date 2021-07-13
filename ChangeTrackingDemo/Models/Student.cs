using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeTrackingDemo.Models
{
    /// <summary>
    /// Model to represent a student tuple in a database
    /// </summary>
    public class Student
    {
        public int StudentID { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")] 
        public string LastName { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Grade Level")] 
        public string GradeLevel { get; set; }
    }
}
