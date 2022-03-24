using Domain.DataModels;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Method used to save or update customers from a csv into the db
        /// </summary>
        /// <param name="csv"></param>
        /// <returns></returns>
        Task<int> SaveOrUpdate(IFormFile csv);

        /// <summary>
        /// Get's all the customers
        /// </summary>
        /// <returns></returns>
        IList<Customer> GetAll();
    }
}