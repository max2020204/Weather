using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Weather.UserControls;

namespace Weather
{
    public partial class Form1 : Form
    {
        public Current current = new Current();
        public JsnReceive jsn;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();

        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        private void ControlPanelToForm(UserControl current)
        {
            current.Dock = DockStyle.Fill;
            panel3.Controls.Clear();
            panel3.Controls.Add(current);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Request();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            label2.Text = dt.ToString("HH:mm:ss");
        }
        private void button2_ClickAsync(object sender, EventArgs e)
        {
            Request();
        }
        public void Request()
        {
            try
            {
                int id = 0;
                string name = "";

                if (File.Exists("weather.json"))
                {
                    StreamReader sr = new StreamReader("weather.json");

                    string[] NewFile = File.ReadAllLines("weather.json");
                    for (int i = 0; i < NewFile.Length; i++)
                    {

                        string str = sr.ReadLine();
                        var jsonFile = JsonConvert.DeserializeObject<JsonFile>(str);
                        id = jsonFile.id;
                        name = jsonFile.name;
                        if (name == textBox1.Text)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Exception exs = new Exception();
                    MessageBox.Show(exs.Message);
                }
                string url = string.Format($"http://api.openweathermap.org/data/2.5/weather?id={id}&units=metric&APPID=5672206b5ff40a5b87886276db8fe0f0");
                WebRequest request = WebRequest.CreateHttp(url);
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            jsn = JsonConvert.DeserializeObject<JsnReceive>(line);
                            currentfill();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ControlPanelToForm(current);
        }
        public void currentfill()
        {
            current.lon1.Text = jsn.Coord.Lon.ToString();
            current.lat1.Text = jsn.Coord.Lat.ToString();
            current.main1.Text = jsn.Weather[0].Main;
            current.desc.Text = jsn.Weather[0].Description;
            current.temp.Text = jsn.Main.Temp.ToString();
            current.pres.Text = jsn.Main.Pressure.ToString() + " hPa";
            current.hum.Text = jsn.Main.Humidity.ToString() + " %";
            current.tmin.Text = jsn.Main.Temp_min.ToString();
            current.tmax.Text = jsn.Main.Temp_max.ToString();
            current.vis.Text = (jsn.Visibility/1000).ToString() + " km";
            current.wspeed.Text = jsn.Wind.Speed.ToString();
            current.wdeg.Text = jsn.Wind.Deg.ToString();
            current.dt.Text = UnixTimeStampToDateTime(jsn.Dt).ToString();
            current.sunrise.Text = UnixTimeStampToDateTime(jsn.Sys.Sunrise).ToString();
            current.sunset.Text = UnixTimeStampToDateTime(jsn.Sys.Sunset).ToString();
            current.country.Text = jsn.Sys.Country;
            current.city.Text = jsn.Name;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}