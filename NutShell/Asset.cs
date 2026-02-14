using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NutShell
{
    public class Asset
    {
        [Required]
        public int Id {  get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public long Value {  get; set; }
        public int OwnerId { get; set; }
        public virtual Person Owner { get; set; }

    }
}
