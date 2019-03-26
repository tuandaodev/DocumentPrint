using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Printing;
using System.Management;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;

namespace InHinhAnh
{
    
    /// <summary>
    /// Description of Main.
    /// </summary>
    public partial class MainForm : Form
	{
		private string foldername;
		private bool StopPrint;
		private bool Printting;
		private int interval;
		delegate void ShowInfoCallBack(string msg);
		private ShowInfoCallBack ShowCB;
        private string paper_type;
        private int CountError = 0;
        NotifyIcon trayIcon = new NotifyIcon();

        //PrinterSettings printSettings = new PrinterSettings();

        private static readonly HttpClient client = new HttpClient();

        private const int SW_SHOWNA = 4;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public MainForm()
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
            this.notifyIcon1.Text = "Tự Động In Đơn Hàng";

            init_combo_size();

            StopPrint = false;
            Printting = false;
            interval = 30;
            ShowCB = new ShowInfoCallBack(ShowInfo);

            CheckApplicationToStartup();

            // Initialize Tray Icon
            TrayIcon.ContextMenu = new ContextMenu(new MenuItem[]
            {
                    new MenuItem("Hiện Tự động In", ShowMainForm),
                    new MenuItem("Thống Kê", ShowThongKe),
                    new MenuItem("Thoát và Đăng xuất", DangXuat),
                    new MenuItem("Thoát", ExitApp)
            });

            TrayIcon.Visible = true;

            LoadSettings();
        }

        private void init_combo_size()
        {
            //PaperSize pkSize;
            //for (int i = 0; i < printSettings.PaperSizes.Count; i++)
            //{
            //    pkSize = printSettings.PaperSizes[i];
            //    this.selected_parper.Items.Add(pkSize);
            //}

            PrintServer myPS = new PrintServer();
            PrintQueueCollection myPrintQueues = myPS.GetPrintQueues();

            int check_print = 0;
            foreach (PrintQueue pq in myPrintQueues)
            {
                pq.Refresh();
                string temp = "";
                //check_print = this.SpotTroubleUsingProperties(ref temp, pq);
                //if (check_print == 0 && !pq.IsOffline)
                if (!pq.IsOffline)
                {
                   this.checkedList_mayin.Items.Add(pq.FullName, false);
                }

            }// end for each print queue

        }

        private void LoadSettings()
        {
            this.text_user.Text = "User: " + Properties.Settings.Default.TenNhanVien;

            foldername = Properties.Settings.Default.PrintFolderPath;
            if (!String.IsNullOrEmpty(foldername))
            {
                this.labelFolder.Text = foldername;
            } else
            {
                this.labelFolder.Text = "";
            }

            if (!String.IsNullOrEmpty((string)Properties.Settings.Default.TimeDelay))
            {
                this.textBoxInterval.Text = (string)Properties.Settings.Default.TimeDelay;
            } else
            {
                this.textBoxInterval.Text = "30";
            }

            if (!String.IsNullOrEmpty((string)Properties.Settings.Default.PaperSize))
            {
                this.selected_parper.SelectedIndex = Int32.Parse(Properties.Settings.Default.PaperSize);
            } else
            {
                this.selected_parper.SelectedIndex = -1;
            }

            if (Properties.Settings.Default.MinimizeStartup != null)
            {
                this.checkBox_minimizeStartup.Checked = (bool)Properties.Settings.Default.MinimizeStartup;
            } else
            {
                this.checkBox_minimizeStartup.Checked = false;
            }

            bool check_setting_listmayin = false;
            try
            {
                if (Properties.Settings.Default.ListMayIn != null)
                {
                    check_setting_listmayin = true;
                }
            }
            catch (Exception ex)
            {
                check_setting_listmayin = false;
            }

            StringCollection listMayInDaChon = null;
            if (check_setting_listmayin == true && Properties.Settings.Default.ListMayIn.Count > 0)
            {
                listMayInDaChon = Properties.Settings.Default.ListMayIn;
            } else
            {
                this.checkedList_mayin.ClearSelected();
            }

            if (listMayInDaChon != null && listMayInDaChon.Count > 0)
            {
                for (int i=0; i <= this.checkedList_mayin.Items.Count - 1; i++)
                {
                    if (listMayInDaChon.Contains(this.checkedList_mayin.Items[i].ToString()))
                    {
                        this.checkedList_mayin.SetItemChecked(i, true);
                    }
                }
            }
        }

        public void ShowMainForm(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            ShowWindow(this.Handle, SW_SHOWNA);
            this.TopMost = true;
        }

        public void ShowThongKe(object sender, EventArgs e)
        {
            ThongKe frm = new ThongKe();
            frm.Show();
        }

        public void DangXuat(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Reload();

            Application.Exit();
            Environment.Exit(0);
        }

        public void ExitApp(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        public NotifyIcon TrayIcon
        {
            get { return trayIcon; }
            set { trayIcon = value; }
        }

        //Not Use
        public List<string> PrintCheckAll()
        {
            List<string> list_print = new List<string>();

            PrintServer myPS = new PrintServer();
            PrintQueueCollection myPrintQueues = myPS.GetPrintQueues();

            Console.WriteLine("\n\nKiem tra trang thai may in:\n\n");

            int check_print = 0;
            foreach (PrintQueue pq2 in myPrintQueues)
            {
                Console.WriteLine(pq2.FullName);

                PrintQueue pq = new PrintQueue(myPS, pq2.FullName, PrintSystemDesiredAccess.AdministratePrinter);
                pq.Refresh();

                string statusReport = pq.FullName + ": ";
                check_print = this.SpotTroubleUsingProperties(ref statusReport, pq);
                if (check_print == 0)
                {
                    list_print.Add(pq.FullName);
                } else {
                    if (!pq.IsOffline && pq.NumberOfJobs > 0)
                    {
                        try
                        {
                            pq.Resume();
                            SetText("Đã cố gắng resume " + pq.NumberOfJobs + " bản in: " + pq2.FullName);
                        }
                        catch (Exception)
                        {
                        }
                        
                    }
                    Console.WriteLine(statusReport);
                }
            }// end for each print queue

            return list_print;
        }

        public bool PrintCheckOne(string print_name)
        {

            PrintServer myPS = new PrintServer();
            PrintQueueCollection myPrintQueues = myPS.GetPrintQueues();

            int check_print = 0;
            bool result = false;
            foreach (PrintQueue pq in myPrintQueues)
            {

                //PrintCapabilities printCapabilites = pq.GetPrintCapabilities();

                if (pq.FullName.ToLower() == print_name.ToLower())
                {
                    pq.Refresh();

                    //PrintJobInfoCollection jobs = pq.GetPrintJobInfoCollection();
                    //foreach (PrintSystemJobInfo theJob in jobs)
                    //{
                    //    Console.WriteLine("AAA:" + theJob.Name);
                    //}

                    string statusReport = pq.FullName + ": ";
                    check_print = this.SpotTroubleUsingProperties(ref statusReport, pq);
                    if (check_print == 0)
                    {
                        return true;
                        //break;
                    } else
                    {
                        SetText(statusReport);
                    }
                }
                
            }// end for each print queue

            return result;
        }

        public int SpotTroubleUsingProperties(ref String statusReport, PrintQueue pq)
        {
            int result = 0;
            if (pq.HasPaperProblem)
            {
                statusReport = statusReport + "Has a paper problem. ";
                result++;
            }
            if (!(pq.HasToner))
            {
                statusReport = statusReport + "Is out of toner. ";
                result++;
            }
            if (pq.IsDoorOpened)
            {
                statusReport = statusReport + "Has an open door. ";
                result++;
            }
            if (pq.IsInError)
            {
                statusReport = statusReport + "Is in an error state. ";
                result++;
            }
            if (pq.IsNotAvailable)
            {
                statusReport = statusReport + "Is not available. ";
                result++;
            }
            if (pq.IsOffline)
            {
                statusReport = statusReport + "Is off line. ";
                result++;
            }
            if (pq.IsOutOfMemory)
            {
                statusReport = statusReport + "Is out of memory. ";
                result++;
            }
            if (pq.IsOutOfPaper)
            {
                statusReport = statusReport + "Is out of paper. ";
                result++;
            }
            if (pq.IsOutputBinFull)
            {
                statusReport = statusReport + "Has a full output bin. ";
                result++;
            }
            if (pq.IsPaperJammed)
            {
                statusReport = statusReport + "Has a paper jam. ";
                result++;
            }
            if (pq.IsPaused)
            {
                statusReport = statusReport + "Is paused. ";
                result++;
            }
            if (pq.IsTonerLow)
            {
                statusReport = statusReport + "Is low on toner. ";
                result++;
            }
            if (pq.NeedUserIntervention)
            {
                statusReport = statusReport + "Needs user intervention. ";
                result++;
            }

            return result;
            // Check if queue is even available at this time of day
            // The following method is defined in the complete example.
            //ReportAvailabilityAtThisTime(ref statusReport, pq);

        }//end SpotTroubleUsingProperties

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


        void Button1Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			if(fbd.ShowDialog() == DialogResult.OK)
			{
				foldername = fbd.SelectedPath;
				this.labelFolder.Text = foldername;

                Properties.Settings.Default.PrintFolderPath = foldername;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Save();

                Console.WriteLine(foldername);
                Properties.Settings.Default.Reload();
                Console.WriteLine("[" + Properties.Settings.Default.PrintFolderPath + "]");
            }
			else
			{
				this.labelFolder.Text = "";
			}
		}

        public void PrintMain()
        {
            try
            {
                // Resume error printer
                PrintCheckAll();
            }
            catch (Exception)
            {
                Console.WriteLine("Co loi trong qua trinh resume.");
            }

            List<string> list_print = new List<string>();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            
            SetText("Kiểm tra máy in:");

            foreach (ManagementObject printer in searcher.Get())
            {
                string printerName = printer["Name"].ToString().ToLower();

                if (printer["WorkOffline"].ToString().ToLower().Equals("false") && !printer["DetectedErrorState"].ToString().ToLower().Equals(""))
                {
                    if (this.checkedList_mayin.CheckedItems.Contains(printer["Name"].ToString()))
                    {
                        list_print.Add(printerName);
                        SetText(" - " + printer["Name"].ToString() + ": Is Ready.");
                    } else
                    {
                        SetText(" - " + printer["Name"].ToString() + ": Is Ready, but not in Listed.");
                    }
                } else
                {
                    SetText(" - " + printer["Name"].ToString() + ": Is Offline or Error.");
                }
            }

            //text view
            SetText("Đã lấy được " + list_print.Count + " máy in.");

            string[] printers = list_print.ToArray();

            var values = new Dictionary<string, string>
                {
                   { "MaNhanVien", (string)Properties.Settings.Default.MaNhanVien },
                   { "TokenKey", (string)Properties.Settings.Default.tokenkey }
                };
            var content = new FormUrlEncodedContent(values);

            dynamic OrdersOjbect = null;
            try
            {
                Console.WriteLine(Global.API_URL + "/print-danh-sach-anh-san-pham");
                var response = client.PostAsync(Global.API_URL + "/print-danh-sach-anh-san-pham", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                OrdersOjbect = JsonConvert.DeserializeObject(result);
            } catch (Exception ex)
            {
                try
                {
                    SetText("Có lỗi trong quá trình lấy danh sách đơn hàng. Thử lại.");

                    var response = client.PostAsync(Global.API_URL + "/print-danh-sach-anh-san-pham", content).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    OrdersOjbect = JsonConvert.DeserializeObject(result);
                } catch (Exception ex2)
                {
                    
                }
            }

            try
            {
                if (OrdersOjbect.isEmpty == 1)
                {
                    SetText("Không có sản phẩm cần in.");
                    return;
                }
            }

            catch (Exception ex)
            {

            }

            try
            {
                PDFPrint pdfPrint = new PDFPrint(this.paper_type);

                int count_excel = 0;
                foreach (var item in OrdersOjbect)
                {
                    count_excel++;
                    Console.WriteLine("{0} {1} {2} {3}\n", item.id, item.MaDH, item.MaHinhAnh, item.MaHinhAnh + ".pdf");

                    string file_path = this.foldername + @"\" + item.MaHinhAnh + ".pdf";
                    if (!File.Exists(file_path))
                    {
                        SetText("[ERROR] Lỗi không tồn tại file " + file_path);
                        continue;
                    }

                    bool get_printer_ready = false;
                    int select_printer = 0;
                    bool stop_print = false;

                    while (get_printer_ready == false && select_printer == 0 && list_print.Count > 0)
                    {
                        if (list_print.Count == 0)
                        {
                            stop_print = true;
                            break;
                        }

                        int temp_select_printer = count_excel % (list_print.Count);
                        bool check_print_ready_single = this.PrintCheckOne(list_print[temp_select_printer]);
                        if (check_print_ready_single == false)
                        {
                            list_print.RemoveAt(temp_select_printer);
                        }
                        else
                        {
                            get_printer_ready = true;
                            select_printer = temp_select_printer;
                        }
                    }

                    if (stop_print || list_print.Count == 0)
                    {
                        this.CountError++;
                        SetText("Tất cả máy in bị lỗi. Vui lòng kiểm tra lại. ");
                        SetText("Print work stop.");

                        // thu them 2 lan, neu khong duoc thi dung han.
                        //if (this.CountError > 2)
                        //{
                        //    SetText("Đã thử 3 lần. Dừng tự động In. ");
                        //    this.buttonStart.Text = "Start";
                        //    StopPrint = true;
                        //}
                        break;
                    }

                    try
                    {
                        Console.WriteLine("Đã kiểm tra và lấy máy in " + list_print[select_printer]);
                        pdfPrint.Print(item, list_print[select_printer], file_path);
                        SetText("Đã in đơn hàng " + item.MaDH + ": " + item.MaHinhAnh);
                        SetCount();
                        Update_Print((string)item.id);
                    }
                    catch (Exception ex)
                    {
                        SetText(ex.Message);
                        SetText("[ERROR] Lỗi in đơn hàng " + item.MaDH + ": " + item.MaHinhAnh);
                        Console.WriteLine(ex.Message);
                    }
                }
                SetText("Kết thúc phiên In. Đợi phiên In tiếp theo.");
                SetText("==========================================");
            } catch (Exception)
            {
                Console.WriteLine("Co loi khi lay danh sach don hang.");
                SetText("Có lỗi trong quá trình lấy danh sách đơn hàng.");
            }
        }

        private void Update_Print(string itemID)
        {
            var values = new Dictionary<string, string>
                {
                   { "MaNhanVien", (string)Properties.Settings.Default.MaNhanVien },
                   { "TokenKey", (string)Properties.Settings.Default.tokenkey },
                   { "MaInDonHang", (string)itemID },
                };
            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync(Global.API_URL + "/print-update", content).Result;

            var result = response.Content.ReadAsStringAsync().Result;

            try
            {
                dynamic OrdersOjbect = JsonConvert.DeserializeObject(result);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

		
		private void PrintWork()
		{
			Printting = true;
			int count = 0;
			bool firsttime = true;
			while(!StopPrint)
			{				
				//Delay
				try{
					interval = int.Parse(this.textBoxInterval.Text);
				}
				catch{
					interval = 30;
                    this.textBoxInterval.Text = interval.ToString();
                }
				if((interval >1000) || (interval <10))
				{
					interval = 30;
                    this.textBoxInterval.Text = interval.ToString();
                }

                Thread.Sleep(1000);
				count++;
				if((count >= interval)||firsttime)
				{
					try{
                        PrintMain();
                    }
					catch(Exception ex){
						this.Invoke(ShowCB,new object[1]{ex.Message});
					}
					count = 0;
					firsttime = false;
				}				
			}
			Printting = false;
		}



		private void ShowInfo(object msg)
		{
			string info = (string)msg;
			SetText(info);
		}

        void SaveSettings()
        {
            //Luu gia tri settings
            Properties.Settings.Default.TimeDelay = (string)this.textBoxInterval.Text;
            Properties.Settings.Default.PaperSize = (string)this.selected_parper.SelectedIndex.ToString();
            Properties.Settings.Default.MinimizeStartup = (bool)this.checkBox_minimizeStartup.Checked;
            Properties.Settings.Default.PrintFolderPath = (string)this.labelFolder.Text;

            StringCollection saveListMayIn = new StringCollection();
            foreach (var item in this.checkedList_mayin.CheckedItems)
            {
                saveListMayIn.Add(item.ToString());
            }

            Properties.Settings.Default.ListMayIn = saveListMayIn;

            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        void ButtonStartClick(object sender, EventArgs e)
		{
            SaveSettings();
            Start_Job();
        }

        void Start_Job()
        {
            if (String.IsNullOrEmpty((string)Properties.Settings.Default.PrintFolderPath))
            {
                MessageBox.Show("Chưa chọn thư mục. Vui lòng chọn trước khi bắt đầu.");
                return;
            }

            if (this.checkedList_mayin.CheckedItems.Count == 0)
            {
                MessageBox.Show("Chưa chọn danh sách máy in.");
                return;
            }

            if (this.selected_parper.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn loại giấy.");
                return;
            }

            Console.WriteLine("[" + (string)Properties.Settings.Default.PrintFolderPath + "]");

            Thread tt = null;

            this.paper_type = this.selected_parper.Text;

            //if (this.selected_parper.SelectedIndex != -1)
            //{
            //    this.printSettings.DefaultPageSettings.PaperSize = printSettings.PaperSizes[this.selected_parper.SelectedIndex];
            //}

            if (!Printting)
            {
                StopPrint = false;
                tt = new Thread(new ThreadStart(PrintWork));
                tt.IsBackground = true;
                tt.Start();
                SetText("Print work start.");
                this.buttonStart.Text = "Stop";
            }
            else
            {
                StopPrint = true;
                Thread.Sleep(1000);
                if ((tt != null) && (tt.IsAlive))
                    tt.Abort();
                SetText("Print work stop.");
                this.buttonStart.Text = "Start";
            }
        }

        private void checkBox_StartAtWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_StartAtWindows.Checked)
            {
                AddApplicationToStartup();
                Console.WriteLine("AddApplicationToStartup");
            } else
            {
                RemoveApplicationFromStartup();
                Console.WriteLine("RemoveApplicationFromStartup");
            }
        }

        public static void RemoveApplicationFromStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                string process_name = typeof(Program).Assembly.GetName().Name.ToString();
                key.DeleteValue(process_name, false);
            }
        }

        public static void AddApplicationToStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                string process_name = typeof(Program).Assembly.GetName().Name.ToString();
                key.SetValue(process_name, "\"" + Application.ExecutablePath + "\"");
            }
        }

        public void CheckApplicationToStartup()
        {
            if (Properties.Settings.Default.MinimizeStartup != null)
            {
                if (Properties.Settings.Default.MinimizeStartup == true)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            }

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                string process_name = typeof(Program).Assembly.GetName().Name.ToString();
                if (key.GetValue(process_name) != null)
                {
                    checkBox_StartAtWindows.Checked = true;
                    Start_Job();
                } else {
                    checkBox_StartAtWindows.Checked = false;
                }
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(trayIcon, null);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.notifyIcon1.Visible = true;
                TrayIcon.ShowBalloonTip(1000, "AutoPrintOrders", "AlwaysOnTop is running in the background.", ToolTipIcon.Info);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                //this.notifyIcon1.Visible = false;
            }
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBoxInfo.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBoxInfo.AppendText(text + "\r\n");
            }
        }

        delegate void SetCountCallback();
        private void SetCount()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBoxInfo.InvokeRequired)
            {
                SetCountCallback d = new SetCountCallback(SetCount);
                this.Invoke(d, new object[] {  });
            }
            else
            {
                int count_temp = Int32.Parse(status_countPrinted.Text) + 1;
                this.status_countPrinted.Text = count_temp.ToString();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //load setting
            
        }

        private void btn_saveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void btn_loadSettings_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void btn_deleteSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Reload();
            LoadSettings();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            ShowWindow(this.Handle, SW_SHOWNA);
            this.TopMost = true;
        }
    }

   
}
