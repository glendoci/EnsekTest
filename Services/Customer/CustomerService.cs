using Domain.DataModels;
using Domain.Dtos;
using EnsekData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        private readonly ICsvExportService _csvExportService;

        /// <summary>
        /// Dependency injection 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="csvExportService"></param>
        public CustomerService(DataContext context, ICsvExportService csvExportService)
        {
            _context = context;
            _csvExportService = csvExportService;
        }

        public IList<Customer> GetAll()
        {
            var x = _context.Customers.ToList();
            return x;
        }

        /// <summary>
        /// Method used to save or update customers from a csv into the db
        /// </summary>
        /// <param name="csv"></param>
        public async Task<int> SaveOrUpdate(IFormFile csv)
        {
            var customers = _csvExportService.GetCsvData<CustomerCsvReadDto>(csv);

            AddAccountsAndCustomersToContext(customers);

            var changedData = _context.SaveChanges();

            return changedData;
        }

        /// <summary>
        /// Method used to add accounts and customer to the context so they can be created
        /// </summary>
        /// <param name="customersDto"></param>
        private void AddAccountsAndCustomersToContext(IList<CustomerCsvReadDto> customersDto)
        {
            foreach (var customerDto in customersDto)
            {
                var customer = MapCustomerCsvReadDtoToModel(customerDto);

                if (customer?.Account?.Id != null)
                {
                    var accountEntity = _context.Accounts.FirstOrDefault(x => x.Id == customer.Account.Id);

                    if (accountEntity == null && customer?.Account != null)
                    {
                        _context.Accounts.Add(customer.Account);
                        _context.Customers.Add(customer);
                    }
                }
            }
        }

        /// <summary>
        /// Method used to do map from CustomerCsvReadDto to CustomerCsvReadDto
        /// </summary>
        /// <param name="customerReadDto"></param>
        /// <returns></returns>
        private Customer MapCustomerCsvReadDtoToModel(CustomerCsvReadDto customerReadDto)
        {
            int accId;
            int.TryParse(customerReadDto.AccountId, out accId);

            return new Customer()
            {
                Account = new Account() { Id = accId },
                FirstName = customerReadDto.FirstName,
                LastName = customerReadDto.LastName
            };
        }
    }
}