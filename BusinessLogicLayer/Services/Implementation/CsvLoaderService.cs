using System.Globalization;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services.Interfaces;
using CsvHelper;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Services.Implementation;

public class CsvLoaderService : ICsvLoaderService
{
    public List<EmployeeInformation> LoadCsv(IFormFile file)
    {
        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = new List<EmployeeInformation>();

            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var record = new EmployeeInformation
                {
                    EmpID = csv.GetField<int>(csv.HeaderRecord.FirstOrDefault(h => h.Trim().Equals("EmpID", StringComparison.OrdinalIgnoreCase))),
                    ProjectID = csv.GetField<int>(csv.HeaderRecord.FirstOrDefault(h => h.Trim().Equals("ProjectID", StringComparison.OrdinalIgnoreCase))),
                    DateFrom = ParseDate(csv.GetField(csv.HeaderRecord.FirstOrDefault(h => h.Trim().Equals("DateFrom", StringComparison.OrdinalIgnoreCase)))),
                    DateTo = ParseNullableDate(csv.GetField(csv.HeaderRecord.FirstOrDefault(h => h.Trim().Equals("DateTo", StringComparison.OrdinalIgnoreCase))))
                };

                records.Add(record);
            }

            return records;
        }
    }

    private DateTime ParseDate(string dateString)
    {
        if (DateTime.TryParse(dateString, out var date))
        {
            return date;
        }
        throw new FormatException($"Unable to parse date: {dateString}");
    }

    private DateTime? ParseNullableDate(string dateString)
    {
        if (string.IsNullOrEmpty(dateString))
        {
            return null;
        }

        if (DateTime.TryParse(dateString, out var date))
        {
            return date;
        }
        return DateTime.Today;
    }
}