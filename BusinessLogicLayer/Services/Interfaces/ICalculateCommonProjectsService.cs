using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services.Interfaces;

public interface ICalculateCommonProjectsService
{
    ICollection<CommonProject> CalculateCommonProjects(ICollection<EmployeeInformation> records);
}