using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Bootstrapper _bootstrapper;

        public App()
        {
            _bootstrapper = new Bootstrapper();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _bootstrapper.Initialize();
            base.OnStartup(e);
        }
    }
}
