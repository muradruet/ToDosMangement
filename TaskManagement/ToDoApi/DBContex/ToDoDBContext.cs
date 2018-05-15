using System.Data.Entity;
using WebApi.DBModel;

namespace WebApi.DBContex
{
    public class ToDoDBContext : DbContext
    {
        public ToDoDBContext() : base("name=DB_A3B63F_ToDoDB")
        //public ToDoDBContext() : base("name=ToDoDB")
        {
            this.Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer<ToDoDBContext>(null);
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<DboUsers> users { get; set; }
        public virtual DbSet<DboTasks> tasks { get; set; }
        public virtual DbSet<DboUserTtasks> usersTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<DboTasks>()
                        .HasMany<DboUsers>(s => s.users)
                        .WithMany(c => c.tasks)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("taskId");
                            cs.MapRightKey("userId");
                            cs.ToTable("userstasks");
                        });
                        

            base.OnModelCreating(modelBuilder);
        }
    }
}