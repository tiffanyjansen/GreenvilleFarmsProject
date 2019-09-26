using System.Data.Entity;

namespace GreenvilleFarmsProject.Models
{

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=Database")
        {
        }

        public virtual DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
