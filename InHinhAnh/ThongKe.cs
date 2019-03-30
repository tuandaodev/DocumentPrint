using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InHinhAnh
{
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
            this.CenterToScreen();

            try
            {
                string ThongKe_URL = Global.API_URL + "/print-thong-ke";
                string result = this.GET(ThongKe_URL);

                dynamic thongkeObj = JsonConvert.DeserializeObject(result);
                this.text_thongkengay.Text = thongkeObj.ngay;
                this.text_thongkethang.Text = thongkeObj.thang;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        // Get order
        public string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}
