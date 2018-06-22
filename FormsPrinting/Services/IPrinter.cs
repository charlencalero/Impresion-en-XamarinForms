using System;
using Xamarin.Forms;

namespace FormsPrinting.Services
{
    public interface IPrinter
    {
        void Print(WebView viewToPrint);
    }
}
