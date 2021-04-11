using System;
using Conflictus.Areas.Identity.Data;
using Conflictus.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Conflictus.Areas.Identity.IdentityHostingStartup))]
namespace Conflictus.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ConflictusContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ConflictusContextConnection")));

                services.AddDefaultIdentity<ConflictusUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ConflictusContext>();
            });
        }
    }
}