using Caliburn.Micro;
using EMS.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EMS.ViewModels
{
    public class EmployeeDetailsViewModel : Screen, IHandle<object>
    {

        private readonly IEventAggregator _eventAggregator;
        private EmployeeModel _employee;

        public EmployeeModel Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                NotifyOfPropertyChange(() => Employee);
            }
        }

        public EmployeeDetailsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _employee = new EmployeeModel();
        }

        public Task HandleAsync(object message, CancellationToken cancellationToken)
        {
            // Do something with the message data
            Employee = (EmployeeModel)message;

            return Task.CompletedTask;
        }

        //protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        //{
        //    if (close)
        //    {
        //        if (GetView() is UserControl root)
        //        {
        //            foreach (var child in GetVisualChildren(root))
        //            {
        //                if (child is TextBlock textBlock)
        //                {
        //                    textBlock.Text = "";
        //                }
        //            }
        //        }
        //    }
        //    return Task.CompletedTask;
        //}
        //private static IEnumerable<DependencyObject> GetVisualChildren(DependencyObject parent)
        //{
        //    for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parent, i);
        //        yield return child;

        //        foreach (var visualChild in GetVisualChildren(child))
        //        {
        //            yield return visualChild;
        //        }
        //    }
        //}


        #region commented
        //private string _empID;
        //private string _empName;
        //private string _empPhNo;
        //private string _empEmail;
        //private string _empAdd;

        //public string EmpID
        //{

        //    get => _empID;
        //    set
        //    {

        //        _empID = value;
        //        NotifyOfPropertyChange(() => EmpID);
        //    }
        //}

        //public string EmpName
        //{

        //    get => _empName;
        //    set
        //    {

        //        _empName = value;
        //        NotifyOfPropertyChange(() => EmpName);
        //    }
        //}

        //public string EmpPhNo
        //{

        //    get => _empPhNo;
        //    set
        //    {

        //        _empPhNo = value;
        //        NotifyOfPropertyChange(() => EmpPhNo);
        //    }
        //}

        //public string EmpEmail
        //{

        //    get => _empEmail;
        //    set
        //    {

        //        _empEmail = value;
        //        NotifyOfPropertyChange(() => EmpEmail);
        //    }
        //}

        //public string EmpAdd
        //{

        //    get => _empAdd;
        //    set
        //    {

        //        _empAdd = value;
        //        NotifyOfPropertyChange(() => EmpAdd);
        //    }
        //}
        #endregion
    }
}
