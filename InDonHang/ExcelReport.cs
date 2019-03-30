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
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

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
        public bool OK = true;

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

            try
            {
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
                else if (paper_type == "A5")
                {
                    ws.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA5;
                }
                else if (paper_type == "A3")
                {
                    ws.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA3;
                }
                //else if (paper_type == "A6")
                //{
                //    ws.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaper10x14;
                //}
                else
                {
                    ws.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                }


                ws.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, this.printObj, Type.Missing, Type.Missing, Type.Missing);    // Type.Missing

                app.Visible = false;
                wb.Save();
                wb.Close(false, Type.Missing, Type.Missing);
                app.Quit();

                Marshal.FinalReleaseComObject(ws);
                Marshal.FinalReleaseComObject(wb);
                Marshal.FinalReleaseComObject(app);
            } catch (Exception ex)
            {
                Marshal.FinalReleaseComObject(ws);
                Marshal.FinalReleaseComObject(wb);
                Marshal.FinalReleaseComObject(app);
                Console.WriteLine(ex.Message);
                this.OK = false;
            }
            
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

        public void Create(dynamic item, dynamic printObj, string filepath)
        {
            // Khởi tạo data table
            DataTable dt = new DataTable();

            string current_folder = Directory.GetCurrentDirectory();
            templateFile = new FileInfo(current_folder + @"\order_template.xlsx");

            //item.id = "test";

            //newFilePath = "D:\\export\\" + item.id + "_order.xlsx";
            //newFilePath = "D:\\export\\" + item.MaDH + ".xlsx";
            newFilePath = filepath;
            newFile = new FileInfo(newFilePath);

            // Load file excel và các setting ban đầu
            using (ExcelPackage package = new ExcelPackage(newFile, templateFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                string MaDH_base64String = (string)item.MaDHCode;
                    //"iVBORw0KGgoAAAANSUhEUgAAASIAAAAyAgMAAACFcT4KAAAACVBMVEX///8AAAAAAAB+UaldAAAAAXRSTlMAQObYZgAAAM1JREFUSInt0rEOxCAIBuCfgYVZ3weH7lxS38/Zpzyw9wLHdMlBjCW2frEItmBBNkRWTD62bE+XP8UnrO0L/nKtWIOnIs/rJb6E2BafoqSSSiqppJJKKumXpT8N40Y6wSClm6YyusGy0hjalZWZYR3W6M5IrTW6YGZdI8EEQkwE+xEUL405JA7c8++DtE+lcSQ+dSLLSr6Pz4wWZUuf6fzdM+LKQuKcFBVXv3fzLtCPlKv46QJFd4e93C5luyCkazrQ7z7H1GnZzqz49XgDWwL1nYkjwcoAAAAASUVORK5CYII=";

                int rowIndex = 3;
                int columnIndex = 0;


                worksheet.Cells["A3"].Value = (string)item.MaDH;
                worksheet.Cells["D3"].Value = (string)item.MaVanDonHang;

                // Insert Ma Don Hang
                var image_MaDH = Base64StringToBitmap(MaDH_base64String);
                ExcelPicture picture_MaDH = null;
                if (image_MaDH != null)
                {
                    picture_MaDH = worksheet.Drawings.AddPicture("pic" + rowIndex.ToString() + columnIndex.ToString(), image_MaDH);
                    picture_MaDH.SetPosition(rowIndex, 8, columnIndex, 30);
                    picture_MaDH.SetSize(270,70);
                }

                // Insert Ma Van Don
                string MaVanDonHang_base64String = (string)item.MaVanDonCode;
                    //= "iVBORw0KGgoAAAANSUhEUgAAAMoAAAAyAgMAAABgyqxPAAAACVBMVEX///8AAAAAAAB+UaldAAAAAXRSTlMAQObYZgAAALtJREFUSIntkbEOwyAMRA+pXjzD/5Ch+0UK/8fsr+y5XTqVrJUwijiMnzg7CEdMh4fPGQH3VJjhqWNqYWZKx/zCdbuZzWxmM5vZzDfzT2E22LoxRQdTLJkGkKwVTbXlLZbMAM5e+cAQY6Rl5neUJ8ohRsIatJsyy3bkn9dJMzs6iWrLfqpBHdSrSsgkwbpk2lCdPJkEznGVnmJlrnSoH0uXeiUHsUKyFO9Zm2YhZ3eZzz/NIXOMG8yOW/ECJ8c2H+ajGVsAAAAASUVORK5CYII=";

                int rowIndex2 = 3;
                int columnIndex2 = 3;

                var image_MaVanDonHang = Base64StringToBitmap(MaVanDonHang_base64String);
                ExcelPicture picture_MaVanDonHang = null;
                if (image_MaVanDonHang != null)
                {
                    picture_MaVanDonHang = worksheet.Drawings.AddPicture("pic" + rowIndex2.ToString() + columnIndex2.ToString(), image_MaVanDonHang);
                    picture_MaVanDonHang.SetPosition(rowIndex2, 8, columnIndex2, 30);
                    picture_MaVanDonHang.SetSize(270, 70);
                }

                //string url = "https://png.pngtree.com/element_our/png_detail/20180922/shirt-icon-design-vector-png_107390.jpg";
                //insert_image(ref worksheet, url, 7, 0);

                int row_index_image = 7 - 2; // Bu tru 2 dong cho lan chay dau tien

                //15 = 90/6 --- 90 Ma Hinh Anh / 6 Ma tren mot dong
                int total_row_in_template_image = 15;

                //JArray count_product_imageAA = (JArray)item.MaHinhAnh;
                int count_product_image = 0;
                //foreach (var itemMaHinhAnh in item.MaHinhAnh) { 
                //for (int i = 0; i < count_product_image; i++)

                int i = 0;
                foreach (var itemMHA in item.MaHinhAnh.Children())
                //foreach (int key in .Keys)
                {
                    count_product_image++;
                    //Console.WriteLine((string)itemMHA.MaSP);
                    //Console.WriteLine((string)itemMHA.MaHinhAnh);

                    int col_index = i % 6;
                    if (col_index == 0)
                    {
                        row_index_image = row_index_image + 2;  // Bu tru 7-2
                    }
                    col_index = col_index + 1;
                    worksheet.Cells[row_index_image, col_index].Value = (string)itemMHA.MaHinhAnh;

                    string url = (string)itemMHA.LinkAnh;
                    if (!String.IsNullOrEmpty(url))
                    {
                        insert_image(ref worksheet, url, row_index_image, col_index);
                        //worksheet.Cells[row_index + 1, col_index].Value = "Anh" + (i + 1).ToString();
                    }
                    i++;
                }


                int row_index_text = 39;    //Index dong dau tien trong index
                int count_product_text = 0;
                int total_row_in_template_text = 50; // 50*2 = 100 san pham - Set trong template

                try
                {
                    foreach (var itemMSP in item.MaSP.Children())
                    {
                        count_product_text++;
                    }
                } catch (Exception)
                {

                }
                

                int product_per_side = (count_product_text/2) + (count_product_text%2);

                int count_printed = 0;

                if (count_product_text > 0)
                {
                    foreach (var itemMSP in item.MaSP.Children())
                    {
                        //count_product_text++;
                        int col_index = 1; // A
                        int row_index_temp = row_index_text + count_printed;
                        count_printed++;
                        if (count_printed > product_per_side)
                        {
                            col_index = 4;
                            row_index_temp = row_index_text + (count_printed % (product_per_side + 1));
                        }
                        worksheet.Cells[row_index_temp, col_index].Value = count_printed.ToString();
                        worksheet.Cells[row_index_temp, col_index + 1].Value = (string)itemMSP.MaSP;
                        worksheet.Cells[row_index_temp, col_index + 2].Value = (string)itemMSP.MaHinhAnh;
                    }
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
                //if (count_product_image % 6 == 0)
                //{
                //    so_dong_con_lai_image++;
                //}
                // Go to empty row
                row_index_image = row_index_image + 2;
                //Console.WriteLine(so_dong_con_lai_image);
                //Console.WriteLine(row_index_image);
                worksheet.DeleteRow(row_index_image, so_dong_con_lai_image);
                // End delete above content

                package.Save();
                package.Dispose();
            }

            this.printObj = printObj;
            this.PrintExcel();
        }

        private Bitmap Base64StringToBitmap(string base64String)
        {
            if (String.IsNullOrEmpty(base64String)) return null;
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