using Domain.DataModels;
using Domain.Dtos;
using EnsekData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly DataContext _context;
        private readonly ICsvExportService _csvExportService;

        public MeterReadingService(DataContext context, ICsvExportService csvExportService)
        {
            _context = context;
            _csvExportService = csvExportService;
        }

        /// <summary>
        /// Method used to save or update meter readings from a csv into the db
        /// </summary>
        /// <param name="accountsCsv"></param>
        public async Task<ResultDto> SaveOrUpdate(IFormFile csv)
        {
            var meterReadingsDto = _csvExportService.GetCsvData<MeterReadingCsvReadDto>(csv);

            var failureCount = AddAccountsAndCustomersToContext(meterReadingsDto);
            var changedData = _context.SaveChanges();


            return new ResultDto { FailureCount = failureCount, SuccessCount = changedData };
        }

        private int AddAccountsAndCustomersToContext(IList<MeterReadingCsvReadDto> meterReadingsDto)
        {
            var failureCount = 0;
            var tmep = new List<MeterReadingCsvReadDto>();
            foreach (var meterReadingDto in meterReadingsDto)
            {
                var meterReading = MapMeterReadingCsvReadDtoToModel(meterReadingDto);
                var isValid = IsAValidMeterReading(meterReading.Account.Id, meterReading.Date, meterReadingDto.MeterReadValue);

                if (isValid)
                {
                    var accountEntity = _context.Accounts.Include(x => x.MeterReadings).Include(x => x.Customers).FirstOrDefault(x => x.Id == meterReading.Account.Id);

                    meterReading.Account = accountEntity; // had to do this to avoid ef trying to insert a new account with the same Id
                    _context.MeterReadings.Add(meterReading);
                }
                else
                {
                    failureCount++;
                    tmep.Add(meterReadingDto);
                }
            }

            return failureCount;
        }

        // For efficienty could return the accountEntity instead of bool and null if not vial so that I wouldn't have to do a second read above  but for now keep it simple
        private bool IsAValidMeterReading(int mrAccId, DateTime mrDate, string? mrValue)
        {
            // Get account on which the reading will be associated to if no account the reading is not valid
            var accountEntity = _context.Accounts.Include(x => x.MeterReadings).FirstOrDefault(x => x.Id == mrAccId);

            // Making sure the exact meter reading doesn't already exist if it does the reading is not valid 
            var meterReadingAlreadyExists = accountEntity?.MeterReadings?.Any(x => x.Account.Id == mrAccId && x.Date == mrDate && x.Value == x.Value) ?? false;

            // If there are other meter reading it will make sure the current one is not older than the most up to date reading
            var isNewerReading = accountEntity?.MeterReadings?.Count > 0 ? accountEntity?.MeterReadings?.OrderByDescending(x => x.Date).FirstOrDefault()?.Date < mrDate : true;

            var formatValid = HasValidFormat(mrValue);

            return accountEntity != null && !meterReadingAlreadyExists && isNewerReading && formatValid;
        }

        // Could have done an extension method if I decided to keep it as a string and not decimal 
        public bool HasValidFormat(string? meterReading)
        {
            if (meterReading == null)
            {
                return false;
            }

            if (meterReading?.Length == 0)
            {
                return false;
            }

            decimal value;
            if (!decimal.TryParse(meterReading, out value))
            {
                return false;
            }

            return true;
        }

        public MeterReading MapMeterReadingCsvReadDtoToModel(MeterReadingCsvReadDto meterReadingDto)
        {
            int accId;
            int.TryParse(meterReadingDto.AccountId, out accId);

            DateTime date;
            DateTime.TryParse(meterReadingDto.MeterReadingDateTime, out date);

            decimal value;
            decimal.TryParse(meterReadingDto.MeterReadValue, out value);

            return new MeterReading()
            {
                Account = new Account { Id = accId },
                Date = date,
                Value = value.ToString("00000") ?? string.Empty
            };
        }
    }
}