using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace CashFlow.Infrastructure.DataAccess;

internal class CashFlowDbContext: DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;database=cashflowdb;Uid=root;Pwd=Enk1227*;";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 45));


        optionsBuilder.UseMySql(connectionString, serverVersion);
    }

}
