using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.EF
{
    public class Repository : IRepository
    {
        private employeeDBEntities _context;
        public employeeDBEntities context
        {
            get
            {
                return _context;
            }
        }

        public Repository(employeeDBEntities context)
        {
           _context = context;
        }
        public bool AddEmployee(employee emp)
        {
            if (emp != null)
            {
                context.employees.Add(emp);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteEmployee(int id)
        {
            IEnumerable<employee> findEmp = FindEmployee(id);
            employee emp = findEmp.FirstOrDefault();
            if (findEmp != null)
            {
                context.employees.Remove(emp);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<employee> FindEmployee(int id)
        {
            IEnumerable<employee> findEmp;

            findEmp = (from e in context.employees where e.empID == id.ToString() select e);

            if (findEmp != null)
            {
                return findEmp;
            }
            return null;
        }

        public IEnumerable<employee> GetAllEmployee()
        {
            var result = (from e in context.employees select e);

            if (result != null)
            {
                return result;
            }
            return null;
        }

        public bool UpdateEmployee(int id, employee emp)
        {
            IEnumerable<employee> getEmp = FindEmployee(id);
            employee findEmp = getEmp.FirstOrDefault();
            bool flag = false;
            if (findEmp != null)
            {
                if (emp.empName != null)
                {
                    findEmp.empName = emp.empName;
                    flag = true;
                }
                if (emp.empPhNo != null)
                {
                    findEmp.empPhNo = emp.empPhNo;
                    flag = true;
                }
                if (emp.empEmail != null)
                {
                    findEmp.empEmail = emp.empEmail;
                    flag = true;
                }
                if (emp.empAdd != null)
                {
                    findEmp.empAdd = emp.empAdd;
                    flag = true;
                }
                if (flag == true)
                {
                    context.SaveChanges();
                    return flag;
                }

            }
            return flag;
        }
    }
}
