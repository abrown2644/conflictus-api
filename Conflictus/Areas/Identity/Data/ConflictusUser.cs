using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Conflictus.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ConflictusUser class
    public class ConflictusUser : IdentityUser
    {
        //[PersonalData]
        //[Column(TypeName = "nvarchar(100)")]
        //public string Name { get; set; }
    }
}
