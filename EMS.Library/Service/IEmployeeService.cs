using System.Collections.Generic;
using EMS.Library.Models;

namespace EMS.Library.Service
{
    public interface IEmployeeService
    {
        List<EmployeeModel> GetAllEmployees();
    }
}