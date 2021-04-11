using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Conflictus.Model
{
    public class War
    {        
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Date { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<Battle> Battles { get; set; }   
        
    }
}
