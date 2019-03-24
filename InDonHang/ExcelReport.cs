using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using System.IO;
using System.Data;
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Net;

namespace InDonHang
{
    class ExcelReport
    {
        FileInfo newFile;
        string newFilePath;
        FileInfo templateFile;
        ExcelPackage xlPackage;
        DataSet _ds;
        dynamic printObj;
        string paper_type = "A4";

        public ExcelReport(string paper_type)
        {
            this.paper_type = paper_type;
            //Console.WriteLine("Init Excel Report");
        }


        public void PrintExcel()
        {
            //var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Open(newFilePath,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

            ws.PageSetup.PrintHeadings = false;
            ws.PageSetup.BlackAndWhite = false;
            ws.PageSetup.PrintGridlines = false;
            ws.PageSetup.Zoom = false;
            ws.PageSetup.FitToPagesWide = 1;
            ws.PageSetup.FitToPagesTall = 1;
            ws.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;

            if (this.paper_type == "A4")
            {
                ws.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
            }
            else
            {
                ws.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA5;
            }

            ws.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, this.printObj, Type.Missing, Type.Missing, Type.Missing);    // Type.Missing

            app.Visible = false;

            wb.Save();
            wb.Close(false, Type.Missing, Type.Missing);
            app.Quit();
        }

        private void insert_image(ref ExcelWorksheet worksheet, string imgUrl, int rowIndex, int columnIndex)
        {
            if (String.IsNullOrEmpty(imgUrl)) return;

            try
            {
                using (var wc = new WebClient())
                {
                    using (var imgStream = new MemoryStream(wc.DownloadData(imgUrl)))
                    {
                        using (var objImage = Image.FromStream(imgStream))
                        {
                            ExcelPicture picture = null;
                            picture = worksheet.Drawings.AddPicture("pic" + rowIndex.ToString() + columnIndex.ToString(), objImage);
                            picture.SetPosition(rowIndex, 8, columnIndex-1, 18);
                            picture.SetSize(70, 60);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Create(dynamic item, dynamic printObj)
        {
            // Khởi tạo data table
            DataTable dt = new DataTable();
            templateFile = new FileInfo("D:\\order_template.xlsx");

            //item.id = "test";

            //newFilePath = "D:\\export\\" + item.id + "_order.xlsx";
            newFilePath = "D:\\export\\test_order.xlsx";
            newFile = new FileInfo(newFilePath);


            // Load file excel và các setting ban đầu
            using (ExcelPackage package = new ExcelPackage(newFile, templateFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                string base64String = "iVBORw0KGgoAAAANSUhEUgAAASIAAAAyAgMAAACFcT4KAAAACVBMVEX///8AAAAAAAB+UaldAAAAAXRSTlMAQObYZgAAAM1JREFUSInt0rEOxCAIBuCfgYVZ3weH7lxS38/Zpzyw9wLHdMlBjCW2frEItmBBNkRWTD62bE+XP8UnrO0L/nKtWIOnIs/rJb6E2BafoqSSSiqppJJKKumXpT8N40Y6wSClm6YyusGy0hjalZWZYR3W6M5IrTW6YGZdI8EEQkwE+xEUL405JA7c8++DtE+lcSQ+dSLLSr6Pz4wWZUuf6fzdM+LKQuKcFBVXv3fzLtCPlKv46QJFd4e93C5luyCkazrQ7z7H1GnZzqz49XgDWwL1nYkjwcoAAAAASUVORK5CYII=";

                int rowIndex = 3;
                int columnIndex = 0;

                // Insert Ma Don Hang
                var image = Base64StringToBitmap(base64String);
                ExcelPicture picture = null;
                if (image != null)
                {
                    picture = worksheet.Drawings.AddPicture("pic" + rowIndex.ToString() + columnIndex.ToString(), image);
                    picture.SetPosition(rowIndex, 8, columnIndex, 30);
                    picture.SetSize(270,70);
                }

                // Insert Ma Van Don
                string base64String2 = "iVBORw0KGgoAAAANSUhEUgAAAMoAAAAyAgMAAABgyqxPAAAACVBMVEX///8AAAAAAAB+UaldAAAAAXRSTlMAQObYZgAAALtJREFUSIntkbEOwyAMRA+pXjzD/5Ch+0UK/8fsr+y5XTqVrJUwijiMnzg7CEdMh4fPGQH3VJjhqWNqYWZKx/zCdbuZzWxmM5vZzDfzT2E22LoxRQdTLJkGkKwVTbXlLZbMAM5e+cAQY6Rl5neUJ8ohRsIatJsyy3bkn9dJMzs6iWrLfqpBHdSrSsgkwbpk2lCdPJkEznGVnmJlrnSoH0uXeiUHsUKyFO9Zm2YhZ3eZzz/NIXOMG8yOW/ECJ8c2H+ajGVsAAAAASUVORK5CYII=";

                int rowIndex2 = 3;
                int columnIndex2 = 3;

                var image2 = Base64StringToBitmap(base64String2);
                ExcelPicture picture2 = null;
                if (image2 != null)
                {
                    picture = worksheet.Drawings.AddPicture("pic" + rowIndex2.ToString() + columnIndex2.ToString(), image2);
                    picture.SetPosition(rowIndex2, 8, columnIndex2, 30);
                    picture.SetSize(270, 70);
                }

                //string url = "https://png.pngtree.com/element_our/png_detail/20180922/shirt-icon-design-vector-png_107390.jpg";
                //insert_image(ref worksheet, url, 7, 0);

                int row_index_image = 7 - 2; // Bu tru 2 dong cho lan chay dau tien

                //15 = 90/6 --- 90 Ma Hinh Anh / 6 Ma tren mot dong
                int total_row_in_template_image = 15;

                int count_product_image = 21;
                for (int i = 0; i < count_product_image; i++)
                {
                    int col_index = i % 6;
                    if (col_index == 0)
                    {
                        row_index_image = row_index_image + 2;  // Bu tru 7-2
                    }
                    col_index = col_index + 1;
                    worksheet.Cells[row_index_image, col_index].Value = "TEST Title" + (i + 1).ToString();

                    string url = "http://www.how-to-draw-cartoons-online.com/image-files/xhow-to-draw-sonic.gif.pagespeed.ic.MhqtKIS1HE.png";
                    insert_image(ref worksheet, url, row_index_image, col_index);
                    //worksheet.Cells[row_index + 1, col_index].Value = "Anh" + (i + 1).ToString();
                }


                int row_index_text = 39;    //Index dong dau tien trong index
                int count_product_text = 13;
                int total_row_in_template_text = 50; // 50*2 = 100 san pham - Set trong template

                int product_per_side = (count_product_text/2) + (count_product_text%2);

                int count_printed = 0;
                for (int i = 0; i < count_product_text; i++)
                {
                    int col_index = 1; // A
                    int row_index_temp = row_index_text + count_printed;
                    count_printed++;
                    if (count_printed > product_per_side)
                    {
                        col_index = 4;
                        row_index_temp = row_index_text + (count_printed % (product_per_side+1));
                    }
                    worksheet.Cells[row_index_temp, col_index].Value = count_printed.ToString();
                    worksheet.Cells[row_index_temp, col_index + 1].Value = "TenSP" + (count_printed).ToString();
                    worksheet.Cells[row_index_temp, col_index + 2].Value = "MaHinhAnh" + (count_printed).ToString();
                }

                // Start Delete above content
                //15 = 90/6 --- 90 Ma Hinh Anh / 6 Ma tren mot dong
                int so_dong_con_lai_text = total_row_in_template_text - count_product_text / 2;
                // Go to empty row
                int row_index_text_last = row_index_text + count_product_text/2 + 1;
                //Console.WriteLine(so_dong_con_lai_text);
                //Console.WriteLine(row_index_text_last);
                worksheet.DeleteRow(row_index_text_last, so_dong_con_lai_text);
                // End delete above content

                // Start Delete above content
                int so_dong_con_lai_image = (total_row_in_template_image - (int)count_product_image / 6) * 2 - 2;
                // Go to empty row
                row_index_image = row_index_image + 2;
                //Console.WriteLine(so_dong_con_lai_image);
                //Console.WriteLine(row_index_image);
                worksheet.DeleteRow(row_index_image, so_dong_con_lai_image);
                // End delete above content

                package.Save();
                package.Dispose();
            }

            //this.printObj = printObj;
            //this.PrintExcel();
        }

        private Bitmap Base64StringToBitmap(string base64String)
        {
            var bitmapData = Convert.FromBase64String(FixBase64ForImage(base64String));
            var streamBitmap = new System.IO.MemoryStream(bitmapData);
            var bitmap = new Bitmap((Bitmap)Image.FromStream(streamBitmap));
            return bitmap;
        }

        private string FixBase64ForImage(string Image)
        {
            var sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", String.Empty);
            sbText.Replace(" ", String.Empty);
            return sbText.ToString();
        }
    }
}