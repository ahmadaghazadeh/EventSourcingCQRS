using Microsoft.EntityFrameworkCore;

namespace LoanApplications.Projections.Sql.Handlers.Model
{
    public class LoanApplicationContext : DbContext
    {
        public DbSet<LoanApplication> LoanApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
				"data source=.;User Id=sa;Password=Pass@word;initial catalog=LoanApplicationProjection");
            base.OnConfiguring(optionsBuilder);
        }
    }
}