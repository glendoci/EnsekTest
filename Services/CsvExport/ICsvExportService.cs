using Microsoft.AspNetCore.Http;

namespace Services
{
    public interface ICsvExportService
    {
        /// <summary>
        /// Method used to extract all the information from the csv file will require a read dto
        /// </summary>
        /// <param name="csv"></param>IFormFile
        IList<T> GetCsvData<T>(IFormFile csv) where T : class;
    }
}