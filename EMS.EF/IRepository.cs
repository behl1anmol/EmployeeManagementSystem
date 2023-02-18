using System.Collections.Generic;

namespace EMS.EF
{
    public interface IRepository
    {
        employeeDBEntities context
        {
            get;
        }

        bool AddEmployee(employee emp);
        bool DeleteEmployee(int id);
        IEnumerable<employee> FindEmployee(int id);
        IEnumerable<employee> GetAllEmployee();
        bool UpdateEmployee(int id, employee emp);
    }
}