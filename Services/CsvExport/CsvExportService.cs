using Microsoft.AspNetCore.Http;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;

namespace Services
{
    public class CsvExportService : ICsvExportService
    {
        /// <summary>
        /// Method used to extract all the information from the csv file will require a read dto
        /// </summary>
        /// <param name="csv"></param>IFormFile
        public IList<T> GetCsvData<T>(IFormFile csv) where T : class
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            using (var file = new CsvReader(new StreamReader(csv.OpenReadStream()), config))
            {
                return file.GetRecords<T>().ToList();
            }
        }
    }
}