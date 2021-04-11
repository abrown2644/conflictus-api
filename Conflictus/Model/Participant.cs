using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Conflictus.Model
{
    public class Participant
    {
        public Participant()
        {
            SideAs = new HashSet<SideA>();
            SideBs = new HashSet<SideB>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }        
        public string FlagUrl { get; set; }

        public ICollection<War> Wars { get; set; }
        [JsonIgnore]
        public virtual ICollection<SideA> SideAs { get; set; }
        [JsonIgnore]
        public virtual ICollection<SideB> SideBs { get; set; }
    }
}
