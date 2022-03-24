using Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public interface IMeterReadingService
    {
        Task<ResultDto> SaveOrUpdate(IFormFile csv);
    }
}