using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EDMFrame.Exceptions;

namespace EDMFrame.Utils
{
    public class PdfCreatorHelper : IDisposable
    {
        private PDFCreator.clsPDFCreator _PDFCreator;
        PDFCreator.clsPDFCreatorError pErr;
        private bool ReadyState = false;
        private string namePrinter;

        public PdfCreatorHelper(): this("PDFCreator")
        {
        }

        public PdfCreatorHelper(String namePrinter)
        {
            this.namePrinter = namePrinter;
            pErr = new PDFCreator.clsPDFCreatorError();

            _PDFCreator = new PDFCreator.clsPDFCreator();
            _PDFCreator.eError += new PDFCreator.__clsPDFCreator_eErrorEventHandler(_PDFCreator_eError);
            _PDFCreator.eReady += new PDFCreator.__clsPDFCreator_eReadyEventHandler(_PDFCreator_eReady);

            String parameters = "/NoProcessingAtStartup";

            if (!_PDFCreator.cStart(parameters, false))
            {
               throw new Exception(pErr.Description);
            }
        }

        public FileInfo Print(String file, String directoryDest)
        {
            return Print(file, directoryDest, string.Empty);
        }

        public FileInfo Print(String file, String directoryDest, String fileName)
        {
            PDFCreator.clsPDFCreatorOptions opt;
            String fname = Guid.NewGuid().ToString().Replace("-", "");
            if (!string.IsNullOrEmpty(fileName))
            {
                fname = fileName.Replace(".pdf", "").Replace(".PDF", "");
            } 

            if (!_PDFCreator.cIsPrintable(file))
            {
                throw new Exception("File '" + file + "' is not printable!");
            }
            opt = _PDFCreator.cOptions;
            opt.UseAutosave = 1;
            opt.UseAutosaveDirectory = 1;
            opt.AutosaveDirectory = directoryDest + @"\";
            opt.AutosaveFormat = 0;
            opt.AutosaveFilename = fname;
            _PDFCreator.cOptions = opt;
            _PDFCreator.cClearCache();
            _PDFCreator.cDefaultPrinter = this.namePrinter;
            _PDFCreator.cPrintFile(file);
            _PDFCreator.cPrinterStop = false;

            while (!ReadyState)
            {
            //    Application.DoEvents();
            }
            _PDFCreator.cPrinterStop = true;

            FileInfo fi = new FileInfo(Path.Combine(directoryDest, fname + ".pdf"));
            if (!fi.Exists)
            {
                throw new PDFConverterException();
            }
            return fi;
        }

        private void _PDFCreator_eReady()
        {
            //toolStripStatusLabel1.Text = "Status: \"" + _PDFCreator.cOutputFilename + "\" was created!";
            _PDFCreator.cPrinterStop = true;
            ReadyState = true;
        }

        private void _PDFCreator_eError()
        {
            throw new Exception(_PDFCreator.cError.Description);
        }

        public void Dispose()
        {
            _PDFCreator.cClose();
            while (_PDFCreator.cProgramIsRunning)
            {
                //Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            }
            _PDFCreator = null;
            pErr = null;
        }
    }
}
