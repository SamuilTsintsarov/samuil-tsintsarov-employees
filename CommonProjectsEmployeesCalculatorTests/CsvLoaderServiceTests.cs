using BusinessLogicLayer.Services.Implementation;
using Microsoft.AspNetCore.Http;

namespace CommonProjectsEmployeesCalculatorTests;

[TestFixture]
public class CsvLoaderServiceTests
{
    [Test]
    public void LoadCsv_ReturnsCorrectRecords()
    {
        // Arrange
        var service = new CsvLoaderService();
        var csvContent = "EmpID,ProjectID,DateFrom,DateTo\n1,100,2022-01-01,2022-01-05\n2,100,2022-01-03,2022-01-07";
        var file = new FormFile(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csvContent)), 0, csvContent.Length, "data", "test.csv");

        // Act
        var result = service.LoadCsv(file);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].EmpID, Is.EqualTo(1));
        Assert.That(result[0].ProjectID, Is.EqualTo(100));
        Assert.That(result[0].DateFrom, Is.EqualTo(new DateTime(2022, 1, 1)));
        Assert.That(result[0].DateTo, Is.EqualTo(new DateTime(2022, 1, 5)));
        Assert.That(result[1].EmpID, Is.EqualTo(2));
        Assert.That(result[1].ProjectID, Is.EqualTo(100));
        Assert.That(result[1].DateFrom, Is.EqualTo(new DateTime(2022, 1, 3)));
        Assert.That(result[1].DateTo, Is.EqualTo(new DateTime(2022, 1, 7)));
    }
}
