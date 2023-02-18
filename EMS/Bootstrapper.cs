using Autofac;
using System.Collections.Generic;
using System.Linq;
using System;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using EMS.ViewModels;
using System.ComponentModel;
using IContainer = Autofac.IContainer;
using EMS.Views;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace EMS
{
    public class Bootstrapper : BootstrapperBase
    {
        private const string ModuleFilePrefix = "EMS";
        private IContainer _container;
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {   

            base.Configure();
            var builder = new ContainerBuilder();


            ConfigureContainer(builder);
            
            //Event aggregator is for raising events at one place and listening at another

            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            //Registering Modules
            builder.RegisterModule<EMS.Library.Module>();

            builder.RegisterType<EmployeeDetailsViewModel>().SingleInstance();

            //Get every type of class which ends with ViewModel
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                    //  must be a type that ends with ViewModel
                    .Where(type => type.Name.EndsWith("ViewModel"))
                    //  must be in a namespace ending with ViewModels
                    .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("ViewModels"))
                    //  registered as self
                    .AsSelf()
                    //  always create a new one
                    .InstancePerDependency();

            //  register views
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
              //  must be a type that ends with View
              .Where(type => type.Name.EndsWith("View"))
              //  must be in a namespace that ends in Views
              .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("Views"))
              //  registered as self
              .AsSelf()
              //  always create a new one
              .InstancePerDependency();

            _container = builder.Build();
            builder.RegisterInstance(_container);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return key == null ? _container.Resolve(service) : _container.ResolveKeyed(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance) => _container.InjectProperties(instance);

        protected void ConfigureContainer(Autofac.ContainerBuilder builder)
        {
            //  good place to register application types or custom modules
            builder.RegisterType<EmployeeDetailsView>().AsSelf().InstancePerDependency();
            builder.RegisterType<EmployeeListView>().AsSelf().InstancePerDependency();
        }

        private static string[] GetAllDllEntries()
        {
            var runtimeDir = AppDomain.CurrentDomain.BaseDirectory;
            var files = Directory.GetFiles(runtimeDir)
                            .Where(file => Regex.IsMatch(file, @"^.+\.(exe|dll)$")).Where(x =>
                            {
                                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(x);
                                return fileNameWithoutExtension.StartsWith(ModuleFilePrefix, StringComparison.Ordinal);
                            }).ToArray();
            return files;
        }
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return GetAllDllEntries().Select(Assembly.LoadFrom);
        }
        #region simpleContainerBootstrap

        //private SimpleContainer _container = new SimpleContainer();
        //public Bootstrapper()
        //{
        //    Initialize();
        //}

        //protected override void Configure()
        //{
        //    //Event aggregator is for raising events at one place and listening at another
        //    _container.Instance(_container);
        //    _container.Singleton<IWindowManager, WindowManager>()
        //              .Singleton<IEventAggregator, EventAggregator>();

        //    //Get every type of class which ends with ViewModel
        //    GetType().Assembly.GetTypes()
        //        .Where(type => type.IsClass)
        //        .Where(type => type.Name.EndsWith("ViewModel"))
        //        .ToList()
        //        .ForEach(viewModelType => _container.RegisterPerRequest(
        //            viewModelType, viewModelType.ToString(), viewModelType));
        //}

        //protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        //{
        //    DisplayRootViewForAsync<ShellViewModel>();
        //}

        //protected override object GetInstance(Type service, string key)
        //{
        //    return _container.GetInstance(service, key);
        //}

        //protected override IEnumerable<object> GetAllInstances(Type service)
        //{
        //    return _container.GetAllInstances(service);
        //}

        //protected override void BuildUp(object instance) => _container.BuildUp(instance);
        #endregion
    }
}
