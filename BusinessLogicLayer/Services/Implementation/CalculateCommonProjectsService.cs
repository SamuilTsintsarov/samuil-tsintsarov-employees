using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services.Interfaces;

namespace BusinessLogicLayer.Services.Implementation;

public class CalculateCommonProjectsService : ICalculateCommonProjectsService
{
    public ICollection<CommonProject> CalculateCommonProjects(ICollection<EmployeeInformation> records)
    {
        var results = new List<CommonProject>();

        var groupedByProject = records.GroupBy(r => r.ProjectID);

        foreach (var group in groupedByProject)
        {
            var employees = group.ToList();

            for (int i = 0; i < employees.Count; i++)
            {
                for (int j = i + 1; j < employees.Count; j++)
                {
                    var emp1 = employees[i];
                    var emp2 = employees[j];

                    var overlapDays = CalculateOverlapDays(emp1, emp2);

                    if (overlapDays > 0)
                    {
                        results.Add(new CommonProject
                        {
                            EmployeeId1 = emp1.EmpID,
                            EmployeeId2 = emp2.EmpID,
                            ProjectID = emp1.ProjectID,
                            DaysWorked = overlapDays
                        });
                    }
                }
            }
        }

        return results;
    }

    private int CalculateOverlapDays(EmployeeInformation employee1, EmployeeInformation employee2)
    {
        var start = employee1.DateFrom > employee2.DateFrom ? employee1.DateFrom : employee2.DateFrom;
        var end = (employee1.DateTo ?? DateTime.Today) < (employee2.DateTo ?? DateTime.Today) ? (employee1.DateTo ?? DateTime.Today) : (employee2.DateTo ?? DateTime.Today);

        var overlap = (end - start).Days;

        return overlap > 0 ? overlap : 0;
    }
}