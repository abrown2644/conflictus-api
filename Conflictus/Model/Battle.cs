using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Conflictus.Model
{
    public class Battle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }      
                
        public string Date { get; set; }
        public int Year { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }
        public Location Location { get; set; }
        public string ImageUrl { get; set; }
        public string Outcome { get; set; }
        public SideA SideA { get; set; }
        public SideB SideB { get; set; }
        public string Url { get; set; }        
        public int WarId { get; set; }
        public virtual War War { get; set; }
    }    

    public class Location
    {
        [Key]
        public int Id { get; set; }       
        [Required]
        public bool HasCoordinates { get; set; }

        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class SideA
    {
        public SideA()
        {
            Participants = new HashSet<Participant>();
        }
        [Key]
        public int Id { get; set; }
        public ICollection<Participant> Participants { get; set; }        
        public string Commanders { get; set; }
        public string Strength { get; set; }
        public string Losses { get; set; } //comma separated values OR / separated values...
        public bool Victory { get; set; }        
    }

    public class SideB
    {
        public SideB()
        {
            Participants = new HashSet<Participant>();
        }
        [Key]
        public int Id { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public string Commanders { get; set; }
        public string Strength { get; set; }
        public string Losses { get; set; }
        public bool Victory { get; set; }        
    }    
}
