using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Library.Models;
using EMS.Library.Service;
using System.Threading;
using System.Windows.Controls;

namespace EMS.ViewModels
{
    public class EmployeeListViewModel : Screen
    {
        private BindableCollection<EmployeeModel> _employees;
        private readonly IEmployeeService _employeeService;
        private EmployeeModel _selectedEmployee;
        private readonly IEventAggregator _eventAggregator;
        private EmployeeDetailsViewModel _employeeDetailsViewModel;

        public EmployeeListViewModel(IEmployeeService employeeService, IEventAggregator eventAggregator, EmployeeDetailsViewModel employeeDetailsViewModel)
        {
            _employeeService = employeeService;
            _employees = new BindableCollection<EmployeeModel>();
            _eventAggregator = eventAggregator;
            _employeeDetailsViewModel = employeeDetailsViewModel;
        }

        public IEmployeeService EmployeeService => _employeeService;

        public EmployeeDetailsViewModel EmployeeDetails
        {
            get => _employeeDetailsViewModel;
            set => _employeeDetailsViewModel = value;
        }

        public BindableCollection<EmployeeModel> Employees
        {
            get => _employees;
            set => _employees = value;
        }
        public EmployeeModel SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                NotifyOfPropertyChange(() => SelectedEmployee);
            }
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            try
            {
                var employeesFromService = EmployeeService.GetAllEmployees();

                employeesFromService.ForEach(e => Employees.Add(new EmployeeModel()
                {
                    empAdd = e.empAdd,
                    empName = e.empName,
                    empEmail = e.empEmail,
                    empID = e.empID,
                    empPhNo = e.empPhNo
                }));
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        //{
        //    EmployeeDetails.DeactivateAsync(true);
        //    return Task.CompletedTask;
        //}

        public void EmployeesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _emp = (ComboBox)sender;
            _eventAggregator.PublishOnUIThreadAsync(_emp.SelectedItem);
        }

    }
}
