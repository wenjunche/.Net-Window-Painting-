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
using System.Drawing;
using Openfin.WPF;
using Openfin.Desktop;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private bool showOF = true;
        private Openfin.Desktop.RuntimeOptions runtimeOptions;
        private ApplicationOptions appOptions;
        const string RuntimeVersion = "20.91.63.5";
        const string AppUuid = "hyper-grid-uuid";
        const string AppName = "hyper-grid";


        public MainWindow()
        {
            InitializeComponent();

            runtimeOptions = new Fin.RuntimeOptions
            {
                Version = RuntimeVersion,
                EnableRemoteDevTools = true,
                RuntimeConnectTimeout = 20000,
                Arguments = "--disable-gpu",
                SecurityRealm = Guid.NewGuid().ToString(),
                RemoteDevToolsPort = 9090,
            };

            var fin = Fin.Runtime.GetRuntimeInstance(runtimeOptions);

            appOptions = new Openfin.Desktop.ApplicationOptions("clock", "clock", "https://boring-einstein-340ab6.netlify.app/?test=Hello_world!");
            appOptions.MainWindowOptions.SetProperty("backgroundThrottling ", false);

            OpenFinEmbeddedView.Initialized += OpenFinEmbeddedView_Initialized;
            OpenFinEmbeddedView.Ready += OpenFinEmbeddedView_Ready;
            OpenFinEmbeddedView.Initialize(runtimeOptions, appOptions);


        }

        private void OpenFinEmbeddedView_Ready(object sender, EventArgs e)
        {
            OpenFinEmbeddedView.IsVisibleChanged += OpenFinEmbeddedView_IsVisibleChanged;
        }

        private void OpenFinEmbeddedView_Initialized(object sender, EventArgs e)
        {
           //Bug, this does not fire.
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var windowOptions = new Openfin.Desktop.WindowOptions(System.Guid.NewGuid().ToString(), "https://install-staging.openfin.co/health/");

            
                 
            try
            {
                var win = new OfWindow();

                // OpenFinEmbeddedView.OpenfinApplication.close(false, null, (err) =>
                // {
                //     Console.WriteLine(err);
                // });

                win.Loaded += (senderr, ee) =>
                {
                    try
                    {

                        win.OpenFinEmbeddedView.Initialize(runtimeOptions, this.OpenFinEmbeddedView.OpenfinApplication, new Openfin.Desktop.WindowOptions(System.Guid.NewGuid().ToString(), "https://install-staging.openfin.co/health/"));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                };

                win.Show();
            }
            catch
            {

            }
        }

        private void OpenFinEmbeddedView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (OpenFinEmbeddedView.IsVisible)
            {
                OpenFinEmbeddedView.OpenfinWindow.show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var win = new OfWindow();

            win.Loaded += (senderr, ee) =>
            {
                try
                {

                    var uuid = System.Guid.NewGuid().ToString();
                    var application = new Openfin.Desktop.ApplicationOptions(uuid, uuid, "https://boring-einstein-340ab6.netlify.app/?test=Hello_world!");
                    win.OpenFinEmbeddedView.Initialize(runtimeOptions, application);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            };


            win.Show();
        }

      
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int randomQueryStringParam = r.Next(100000, 1000000000);
       
            QueryString.Content = randomQueryStringParam;

            // *** Embedded view replacement code ***
            // This code news up a new instance of the embedded view
            OpenFinEmbeddedView = new EmbeddedView();
            OpenFinEmbeddedView.Initialize(runtimeOptions, appOptions);
            OpenFinEmbeddedView.OpenfinWindow.navigate($"https://boring-einstein-340ab6.netlify.app/?test={randomQueryStringParam}");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (showOF)
            {

                OpenFinEmbeddedView.Visibility = Visibility.Hidden;
            }
            else {
                OpenFinEmbeddedView.Visibility = Visibility.Visible;
                //OpenFinEmbeddedView.OpenfinWindow.show();
                

            }

            showOF = !showOF;
        }
    }
}
