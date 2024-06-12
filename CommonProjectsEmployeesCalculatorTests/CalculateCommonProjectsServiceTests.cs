using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services.Implementation;

namespace CommonProjectsEmployeesCalculatorTests
{
    [TestFixture]
    public class CalculateCommonProjectsServiceTests
    {
        [Test]
        public void CalculateCommonProjects_ReturnsCorrectResult()
        {
            // Arrange
            var service = new CalculateCommonProjectsService();
            var records = new List<EmployeeInformation>
            {
                new EmployeeInformation { EmpID = 1, ProjectID = 100, DateFrom = new DateTime(2022, 1, 1), DateTo = new DateTime(2022, 1, 5) },
                new EmployeeInformation { EmpID = 2, ProjectID = 100, DateFrom = new DateTime(2022, 1, 3), DateTo = new DateTime(2022, 1, 7) },
                new EmployeeInformation { EmpID = 3, ProjectID = 200, DateFrom = new DateTime(2022, 1, 4), DateTo = new DateTime(2022, 1, 8) }
            };

            // Act
            var result = service.CalculateCommonProjects(records);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));

            var commonProject = result.First();
            Assert.That(commonProject.EmployeeId1, Is.EqualTo(1));
            Assert.That(commonProject.EmployeeId2, Is.EqualTo(2));
            Assert.That(commonProject.ProjectID, Is.EqualTo(100));
            Assert.That(commonProject.DaysWorked, Is.EqualTo(2));
        }
    }
}