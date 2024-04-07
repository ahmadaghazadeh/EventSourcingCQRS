using Microsoft.EntityFrameworkCore;

namespace LoanApplications.Projections.Sql.Handlers.Model
{
    public class LoanApplicationContext : DbContext
    {
        public DbSet<LoanApplication> LoanApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
				"Server=localhost;Database=LoanApplication;User Id=sa;Password=Pass@word;Integrated Security=false;TrustServerCertificate=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}