using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class ServicesRegistration
    {
        /// <summary>
        /// Will register all services into the IServiceCollection present inside the method
        /// </summary>
        /// <param name="collection"></param>
        public static void RegisterServices(IServiceCollection collection)
        {
            collection.AddScoped<ICustomerService, CustomerService>();
            collection.AddScoped<IMeterReadingService, MeterReadingService>();
            collection.AddScoped<ICsvExportService, CsvExportService>();
        }
    }
}