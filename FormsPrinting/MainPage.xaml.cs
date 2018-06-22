using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsPrinting.Templates;
using FormsPrinting.Services;
using Xamarin.Forms;

namespace FormsPrinting
{
    public partial class MainPage : ContentPage
    {
        public static Random _random = new Random();
        List<Producto> _productos = new List<Producto>();
        int _order = _random.Next(500);

        public MainPage()
        {
            InitializeComponent();
            var printButton = new ToolbarItem { Text = "Print" };
            printButton.Clicked += Handle_Clicked;
            ToolbarItems.Add(printButton);
        }

        // Código de https://codemilltech.com/xamarin-forms-e-z-print
		protected void Handle_Clicked(object sender, System.EventArgs e)
		{
            var order = new Orden
            {
                Cliente = ClientName.Text,
                IdOrden = _order++,
                Productos =_productos
            };

            var template = new TicketTemplate();
            template.Model = order;

            var rendered = template.GenerateString();

            // Create a source for the webview
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = rendered;

            // Create and populate the Xamarin.Forms.WebView
            var browser = new WebView();
            browser.Source = htmlSource;

            var printService = DependencyService.Get<IPrinter>();
            var printServiceUWP = DependencyService.Get<IPrinterUWP>();

            if (Device.RuntimePlatform == Device.Windows)
            {
            printServiceUWP.Print(htmlSource.Html);
            }
            else
            {
           printService.Print(browser);
            }
            


            _productos.Clear();
            ClientName.Text = "";
            Total.Text = "0";
        }

        void RemoveProduct(object sender, EventArgs e)
        {
            if (_productos.Count > 0)
            {
                _productos.RemoveAt(_productos.Count - 1);
            }
            var total = _productos.Sum(p => p.Total);
            Total.Text = total.ToString();
        }

        void AddProduct(object sender, EventArgs e)
        {
            var price = (_random.NextDouble() + 0.1) * 150;
            var product = new Producto
            {
                Nombre = "Producto " + price,
                Total = price
            };

            _productos.Add(product);

            var total = _productos.Sum(p => p.Total);
            Total.Text = total.ToString();
        }
    }
}
