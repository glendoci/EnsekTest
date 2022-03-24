using Domain.DataModels;
using Microsoft.EntityFrameworkCore;

namespace EnsekData
{
    public interface IDataContext 
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<MeterReading> MeterReadings { get; set; }
    }
}