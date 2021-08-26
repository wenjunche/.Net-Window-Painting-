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


namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Openfin.Desktop.RuntimeOptions runtimeOptions;

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
                Arguments = "--disable-features=CookiesWithoutSameSiteMustSecure,SameSiteByDefaultCookies",
                SecurityRealm = Guid.NewGuid().ToString(),
                RemoteDevToolsPort = 9090,
            };

            var fin = Fin.Runtime.GetRuntimeInstance(runtimeOptions);

            var appOptions = new Openfin.Desktop.ApplicationOptions("clock", "clock", "https://install-staging.openfin.co/health/");

            OpenFinEmbeddedView.Initialize(runtimeOptions, appOptions);
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var win = new OfWindow();

            win.Loaded += (senderr, ee) =>
            {
                try
                {

                    var uuid = System.Guid.NewGuid().ToString();
                    var application = new Openfin.Desktop.ApplicationOptions(uuid, uuid, "https://install-staging.openfin.co/health/");
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
            OpenFinEmbeddedView.OpenfinWindow.navigate("https://install-staging.openfin.co/health/?q=1");   
        }
    }
}
