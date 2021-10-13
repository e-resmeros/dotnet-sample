using api.Models;
using api.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace api.Config
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions options) :base(options)
        {
            
        }

        public DbSet<Device> Devices { get; }

        public DbSet<UserDevice> UserDevices { get; set; }
    }
}