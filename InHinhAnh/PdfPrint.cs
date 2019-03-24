using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using PdfiumViewer;
using System.Drawing.Printing;

namespace InHinhAnh
{
    class PDFPrint
    {
        dynamic printName;
        string file_path;

        PrinterSettings printSettings = new PrinterSettings();
        string paper_type = "A4";

        public PDFPrint(string paper_type)
        {
            this.paper_type = paper_type;
        }

        public void Print(dynamic item, dynamic printName, string file_path)
        {
            this.file_path = file_path;
            this.printName = printName;

            using (var document = PdfDocument.Load(file_path))
            {
                using (var printDocument = document.CreatePrintDocument())
                {
                    printDocument.PrinterSettings = this.printSettings;
                    printDocument.PrinterSettings.PrinterName = this.printName;

                    for (int i = 0; i < printSettings.PaperSizes.Count; i++)
                    {
                        if (printSettings.PaperSizes[i].PaperName.Contains(this.paper_type))
                        {
                            this.printSettings.DefaultPageSettings.PaperSize = printSettings.PaperSizes[i];
                            break;
                        }
                    }

                    printDocument.PrinterSettings.PrintFileName = item.MaHinhAnh + ".pdf";
                    printDocument.DocumentName = item.MaHinhAnh + ".pdf";

                    printDocument.PrintController = new StandardPrintController();
                    printDocument.Print();
                }
            }
        }
    }
}
