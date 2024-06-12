using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Services.Interfaces;

public interface ICsvLoaderService
{
    List<EmployeeInformation> LoadCsv(IFormFile file);
}