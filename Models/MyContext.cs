using Microsoft.EntityFrameworkCore;

namespace SpaceCrabs.Models
{
    public class MyContext :DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
    }
}