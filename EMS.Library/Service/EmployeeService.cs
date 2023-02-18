using System.Collections.Generic;
using System.Linq;
using EMS.EF;
using EMS.Library.Models;

namespace EMS.Library.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository _repository;

        public EmployeeService(IRepository repository)
        {
            _repository = repository;
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> _employees = new List<EmployeeModel>();
            _repository.GetAllEmployee().ToList()
                              .ForEach(emp => _employees.Add(new EmployeeModel()
                              {
                                  empPhNo = emp.empPhNo,
                                  empAdd = emp.empAdd,
                                  empID = emp.empID,
                                  empName = emp.empName,
                                  empEmail = emp.empEmail
                              }));
            return _employees;
        }
    }
}
