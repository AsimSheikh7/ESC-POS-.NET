using System.IO;

namespace ESCPOS_NET.Printers
{
    public class USBPrinter : BasePrinter
    {
        private readonly FileStream _rfile;
        private readonly FileStream _wfile;
        public USBPrinter(string usbPath)
            : base()
        {
            //keeping separate file streams performs better
            //while using 1 file stream printers were having intermittent issues while printing
            //your milege may vary
            _rfile = DeviceFile.OpenDevice(usbPath, FileAccess.Read);
            _wfile = DeviceFile.OpenDevice(usbPath, FileAccess.Write);
            Writer = new BinaryWriter(_wfile);
            Reader = new BinaryReader(_rfile);
        }

        ~USBPrinter()
        {
            Dispose(false);
        }

        protected override void OverridableDispose()
        {
            _rfile?.Close();
            _rfile?.Dispose();
            _wfile?.Close();
            _wfile?.Dispose();
        }
    }
}
