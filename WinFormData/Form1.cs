using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileMonitor;

namespace WinFormData
{
    public partial class Form1 : Form
    {
        private readonly FileWatcher fw = new FileWatcher();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!fw.SiteGoogleConnection())
            {
                OrderGridView.Visible = false;
                PostionGridView.Visible = false;
                startButton.Visible = false;
                statusTextBox.AppendText("Cannot connect to Internet");
            }

            eSignalOutputPath.Text = fw.GetEsignalPath();
            OrderGridView.DataSource = GetLv1SampleTable();
            PostionGridView.DataSource = GetPositionSampleTable();
        }

        private static DataTable GetLv1SampleTable()
        {
            var x = DateTime.Now;
            var m = x.Minute;
            var s = x.Second;
            var h = x.Hour;
            var sampleData = new List<Lv1Quote>
                                 {
                                     new Lv1Quote {Close = 2 +s, High = 3, Low = 1+s, Open = 2, Symbol = "9501.JP",MarketTime = DateTime.Now},
                                     new Lv1Quote {Close = 2+s+m, High = 33-h, Low = 1, Open = 22+s*2, Symbol = "9502.JP",MarketTime = DateTime.Now},
                                     new Lv1Quote {Close = 23, High = 3+m-s, Low = 1+s, Open = 22+s, Symbol = "9503.JP",MarketTime = DateTime.Now},
                                     new Lv1Quote {Close = 23-h, High = 33, Low = 1+m-s, Open = 22+m, Symbol = "9504.JP",MarketTime = DateTime.Now}
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
        


    }
}
