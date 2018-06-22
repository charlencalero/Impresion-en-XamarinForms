using System;
using FormsPrinting.iOS.Services;
using FormsPrinting.Services;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(IosPrinter))]

namespace FormsPrinting.iOS.Services
{
    public class IosPrinter : IPrinter
    {
        public void Print(WebView viewToPrint)
        {
            var renderer = Platform.CreateRenderer(viewToPrint);
            var webView = renderer.NativeView as UIWebView;

            var printInfo = UIPrintInfo.PrintInfo;
            printInfo.OutputType = UIPrintInfoOutputType.General;
            printInfo.JobName = "Xamarin.Forms printing";
            printInfo.Orientation = UIPrintInfoOrientation.Portrait;
            printInfo.Duplex = UIPrintInfoDuplex.None;

            var printController = UIPrintInteractionController.SharedPrintController;

            printController.PrintInfo = printInfo;
            printController.ShowsPageRange = true;
            printController.PrintFormatter = webView.ViewPrintFormatter;

            printController.Present(true, (printInteractionController, completed, error) => { });
        }
    }
}