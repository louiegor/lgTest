using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace WinFormData
{
    public partial class Form1 : Form
    {
        private readonly FileWatcher fw = new FileWatcher();
        private readonly XmlHelper api = new XmlHelper();
        public static Form1 Form1Edit;

        public Form1()
        {
            InitializeComponent();
            Form1Edit = this;
        }

        delegate void SetTextCallback(string text);

        public void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (statusTextBox.InvokeRequired)
            {
                var d = new SetTextCallback(SetText);
                Invoke(d, new object[] { text });
            }
            else
            {
                statusTextBox.Text = String.Format(text + "\r\n" + statusTextBox.Text);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            eSignalOutputPath.Text = fw.GetEsignalPath();
            Global.EsignalPath = eSignalOutputPath.Text;
            Global.PproPath = pproPath.Text;
            Global.TraderId = traderId.Text;
            Global.ShareSize = int.Parse(univShareSize.Text);
            GetConnection();
            OrderGridView.DataSource = GetLv1SampleTable();
            PostionGridView.DataSource = GetPositionSampleTable();
        }

        private void GetConnection()
        {
            if (fw.SiteGoogleConnection()) return;
            OrderGridView.Visible = false;
            PostionGridView.Visible = false;
            startButton.Visible = false;
            statusTextBox.AppendText("Cannot connect to Internet");
        }

        private static DataTable GetLv1SampleTable()
        {
            var x = DateTime.Now;
            var m = x.Minute;
            var s = x.Second;
            var h = x.Hour;
            var sampleData = new List<Lv1Quote>
                                 {
                                     new Lv1Quote {BidPrice = 2 +s, AskPrice = 3, BidSize = 1+s, AskSize = 2, Symbol = "9501.JP",MarketTime = DateTime.Now},
                                     new Lv1Quote {BidPrice = 2+s+m, AskPrice = 33-h, BidSize = 1, AskSize = 22+s*2, Symbol = "9502.JP",MarketTime = DateTime.Now},
                                     new Lv1Quote {BidPrice = 23, AskPrice = 3+m-s, BidSize = 1+s, AskSize = 22+s, Symbol = "9503.JP",MarketTime = DateTime.Now},
                                     new Lv1Quote {BidPrice = 23-h, AskPrice = 33, BidSize = 1+m-s, AskSize = 22+m, Symbol = "9504.JP",MarketTime = DateTime.Now}
                                 };

            var table =  ConvertToDataTable(sampleData);

            return table;
        }

        private static DataTable GetPositionSampleTable()
        {
            var sampleData = new List<OpenPosition>
                                 {
                                     new OpenPosition {Symbol = "9501.JP", Market = "Japan", Side = "Buy", Volume = 100},
                                     new OpenPosition {Symbol = "9502.JP", Market = "Japan", Side = "Sell", Volume = 200},
                                     new OpenPosition {Symbol = "9503.JP", Market = "Japan", Side = "Buy", Volume = 300},
                                     new OpenPosition {Symbol = "9504.JP", Market = "Japan", Side = "Sell", Volume = 1000},
                                 };

            var table = ConvertToDataTable(sampleData);

            return table;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OrderGridView.DataSource = GetLv1SampleTable();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            api.RegisterAll();
            fw.Watch();
            
        }
        


    }
}
