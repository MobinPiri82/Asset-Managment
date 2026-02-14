using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NutShell
{
    public class Person
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public int NationalID { get; set; }
        public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}
