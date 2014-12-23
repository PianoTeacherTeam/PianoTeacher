using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PianoTeacher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App CurrentApp { get { return App.Current as App; } }

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exp=e.Exception;
            if (exp != null)
            {
                e.Handled = true;
                MessageBox.Show(exp.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
