using System;
using Android.Content;
using Android.OS;
using Android.Print;
using FormsPrinting.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DroidWebView = Android.Webkit.WebView;


[assembly: Dependency(typeof(FormsPrinting.Droid.Services.DroidPrinter))]
namespace FormsPrinting.Droid.Services
{
    public class DroidPrinter:IPrinter
    {
        public void Print(WebView viewToPrint)
        {
            //var renderer = Platform.CreateRenderer(viewToPrint);
            //var webView = renderer.ViewGroup.GetChildAt(0) as DroidWebView;

            //if (webView != null)
            //{
            //    var version = Build.VERSION.SdkInt;

            //    if (version >= BuildVersionCodes.Kitkat)
            //    {
            //        PrintDocumentAdapter documentAdapter = webView.CreatePrintDocumentAdapter();
            //        var printMgr = (PrintManager)Forms.Context.GetSystemService(Context.PrintService);
            //        printMgr.Print("Forms-EZ-Print", documentAdapter, null);
            //    }
            //}

            var droidViewToPrint = Platform.CreateRenderer(viewToPrint).ViewGroup.GetChildAt(0) as Android.Webkit.WebView;

            if (droidViewToPrint != null)
            {
                // Only valid for API 19+
                var version = Android.OS.Build.VERSION.SdkInt;

                if (version >= Android.OS.BuildVersionCodes.Kitkat)
                {
                    var printMgr = (PrintManager)Forms.Context.GetSystemService(Context.PrintService);
                    printMgr.Print("Forms-EZ-Print", droidViewToPrint.CreatePrintDocumentAdapter("ksoft"), null);
                }
            }
        }
    }
}