using CoreWebAPIJWT.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPIJWT.Data
{
    public class ApplicationDBContext:DbContext
    {   //all db changes we can do
        //Adding db context class that access the data from database
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
        
        } 
        public DbSet<Villa> Villas { get; set; }
    }
}
