using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EMS.Library.Service;

namespace EMS.Library
{
    public class Module : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterModule<EMS.EF.Module>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
        }
    }
}
