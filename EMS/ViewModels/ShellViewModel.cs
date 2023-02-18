using System;
using System.Windows.Media.Animation;
using Caliburn.Micro;
using EMS.Library.Service;

namespace EMS.ViewModels
{
    public class ShellViewModel : Conductor<Object>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEventAggregator _eventAggregator;
        private readonly EmployeeDetailsViewModel _employeeDetailsViewModel;

        public ShellViewModel(IEmployeeService employeeService, IEventAggregator eventAggregator, 
                              EmployeeDetailsViewModel employeeDetailsViewModel)
        {
            _employeeService = employeeService;
            _eventAggregator = eventAggregator;
            _employeeDetailsViewModel = employeeDetailsViewModel;
            LoadEmployeePage();
        }

        public void LoadEmployeePage()
        {
            ActivateItemAsync(new EmployeeListViewModel(_employeeService, _eventAggregator, _employeeDetailsViewModel));
        }

        public void LoadEmployeeDetailsPage()
        {
            ActivateItemAsync(new EmployeeDetailsViewModel(_eventAggregator));
        }

    }
}
