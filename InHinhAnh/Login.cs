using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace InHinhAnh
{
    public partial class Login : Form
    {

        private static readonly HttpClient client = new HttpClient();
        
        public Login()
        {
            if (IsProcessOpen())
            {
                MessageBox.Show("Application is already running.");
                Application.Exit();
                Environment.Exit(0);
                return;
            }

            InitializeComponent();
            this.CenterToScreen();

        }

        public static bool IsProcessOpen()
        {
            string process_name = typeof(Program).Assembly.GetName().Name.ToString();
            if (Process.GetProcessesByName(process_name).Length > 1)
            {
                // Is running
                return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var values = new Dictionary<string, string>
                {
                   { "username", this.input_username.Text },
                   { "password", this.input_password.Text }
                };

            var content = new FormUrlEncodedContent(values);

            try
            {

                var response = client.PostAsync(Global.API_URL + "/dang-nhap", content).Result;

                var result = response.Content.ReadAsStringAsync().Result;

                dynamic LoginOjbect = JsonConvert.DeserializeObject(result);

                Console.WriteLine(LoginOjbect.MaNhanVien);
                Console.WriteLine(LoginOjbect.TenNhanVien);
                Console.WriteLine(LoginOjbect.tokenkey);

                if (!String.IsNullOrEmpty((string)LoginOjbect.tokenkey))
                {
                    Properties.Settings.Default.MaNhanVien = (string)LoginOjbect.MaNhanVien;
                    Properties.Settings.Default.TenNhanVien = (string)LoginOjbect.TenNhanVien;
                    Properties.Settings.Default.tokenkey = (string)LoginOjbect.tokenkey;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.Save();

                    try
                    {
                        this.Hide();
                        var form2 = new MainForm();
                        form2.Closed += (s, args) => this.Close();
                        form2.Show();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi khi load form chính. Vui lòng thử lại.");
                        this.Close();
                        Application.Exit();
                        Environment.Exit(0);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Username hoặc mật khẩu không đúng. Vui lòng thử lại.");
                throw;
            }
        }

        private void check_login()
        {
            Console.WriteLine("[" + Properties.Settings.Default.tokenkey + "]");
            Console.WriteLine(Properties.Settings.Default.MaNhanVien);
            Console.WriteLine(Properties.Settings.Default.TenNhanVien);

            if (!String.IsNullOrEmpty((string)Properties.Settings.Default.tokenkey))
            {
                this.Hide();
                var form2 = new MainForm();
                form2.Closed += (s, args) => this.Close();
                form2.Show();
            }
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            check_login();
        }
    }
}
