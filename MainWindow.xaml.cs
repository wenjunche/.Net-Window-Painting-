using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fin = Openfin.Desktop;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string RuntimeVersion = "20.91.63.5";
        const string AppUuid = "hyper-grid-uuid";
        const string AppName = "hyper-grid";

        public MainWindow()
        {
            InitializeComponent();

            var runtimeOptions = new Fin.RuntimeOptions
            {
                Version = RuntimeVersion,
                EnableRemoteDevTools = true,
                RuntimeConnectTimeout = 20000
            };

            var fin = Fin.Runtime.GetRuntimeInstance(runtimeOptions);

            var appOptions = new Openfin.Desktop.ApplicationOptions("clock", "clock", "https://www.timeanddate.com/worldclock/");

            OpenFinEmbeddedView.Initialize(runtimeOptions, appOptions);
        }
    }
}
