using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Timers;
using Dispatcher;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace DispatcherDarkorbit
{
    public partial class Form1 : Form
    {
        public static HttpManager Client { get; set; } // Client
        string[] dispatchS = new string[5];
        bool isRunning = false;
        public Form1()
        {
            InitializeComponent();

            var cookie = new Cookie("dosid", "");

        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            if (ServerTxt.Text == "" || SidTxt.Text == "")
            {
                MessageBox.Show("Please check your data");
                return;
            }

            var cookie = new Cookie("dosid", SidTxt.Text, "/", $"{ServerTxt.Text}.darkorbit.com");

            Client = new();
            Client._cookies.Add(cookie);
            var result = await Client.GetAsyncLimit($"https://{ServerTxt.Text}.darkorbit.com/indexInternal.es?action=internalDispatch");
            richTextBox1.Text = $"{DateTime.Now}" + " Log: " + "Loged in" + Environment.NewLine + richTextBox1.Text;

        }

        private void button_click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Console.WriteLine("Button: " + (string)(sender as Button).Text);
                switch ((sender as Button).Text)
                {
                    case "Send All":
                        SendAllDispatchers();
                        break;
                    case "Collect All":
                        CollectAll();
                        break;
                    case "Refresh Stats":
                        RefreshStats();
                        break;
                    case "Start AutoDispatch":
                        isRunning = !isRunning;
                        AutoDispatch();
                        break;
                }
            }
        }
        public async Task SendAllDispatchers()
        {
            Console.WriteLine("Send Started");
            for (int n = 1; n < 6; n++)
            {
                if (dispatchS[n - 1] == "Not Hired")
                {
                    var request = await Client.PostAsyncLimitRaw($"https://{ServerTxt.Text}.darkorbit.com/ajax/dispatch.php", "command=sendDispatch&dispatchId=dispatch_retriever_r01");
                }
            }
            richTextBox1.Text = $"{DateTime.Now}" + " Log: Sent" + Environment.NewLine + richTextBox1.Text;
            Console.WriteLine("Send Ended");
        }
        public async Task CollectAll()
        {
            Console.WriteLine("Collect Started");
            for (int n = 1; n < 6; n++)
            {
                if (dispatchS[n - 1] == "Finished")
                {
                    var request = await Client.PostAsyncLimitRaw($"https://{ServerTxt.Text}.darkorbit.com/ajax/dispatch.php", $"command=collectDispatch&slot={n}");
                }
            }
            richTextBox1.Text = $"{DateTime.Now}" + " Log: Collected" + Environment.NewLine + richTextBox1.Text;
            Console.WriteLine("Collect Ended");

        }
        public async Task RefreshStats()
        {

            Console.WriteLine("Refresh Started");
            var result = await Client.GetAsyncLimit($"https://{ServerTxt.Text}.darkorbit.com/indexInternal.es?action=internalDispatch");
            double time = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")).TotalSeconds;
            double[] dispatchT = new double[5];
            string txt;
            for (int n = 1; n < 6; n++)
            {
                var getTime = Regex.Match(result, $"dispatch_collect_elem_{n}\", \"(.*?)\"");
                if (getTime.Success)
                {

                    dispatchT[n - 1] = TimeSpan.Parse(getTime.Groups[1].Value.Substring(11)).TotalSeconds - time;

                    if (dispatchT[n - 1] <= 0)
                    {
                        dispatchT[n - 1] = 0;
                        dispatchS[n - 1] = "Finished";
                        txt = "-//-";
                    }
                    else
                    {
                        dispatchS[n - 1] = "Running";
                        txt = secToDate(dispatchT[n - 1]);
                    }

                }
                else
                {
                    dispatchT[n - 1] = 0;
                    dispatchS[n - 1] = "Not Hired";
                    txt = "-//-";

                }
                switch (n)
                {
                    case 1:
                        label10.Text = txt;
                        label20.Text = dispatchS[0];
                        break;
                    case 2:
                        label1.Text = txt;
                        label19.Text = dispatchS[1];
                        break;
                    case 3:
                        label7.Text = txt;
                        label18.Text = dispatchS[2];
                        break;
                    case 4:
                        label8.Text = txt;
                        label17.Text = dispatchS[3];
                        break;
                    case 5:
                        label9.Text = txt;
                        label16.Text = dispatchS[4];
                        break;
                }

            }

            richTextBox1.Text = $"{DateTime.Now}" + " Log: Status refreshed" + Environment.NewLine + richTextBox1.Text;
            Console.WriteLine("Refresh Ended");

        }
        public string secToDate(double s)
        {
            TimeSpan t = TimeSpan.FromSeconds(s);
            return string.Format("{0:D2}:{1:D2}:{2:D2}",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);
        }
        public async void AutoDispatch()
        {
            
            while (isRunning == true)
            {
                await RefreshStats();
                if (!dispatchS.Contains("Running"))
                {
                    await CollectAll();
                    await RefreshStats();
                    await SendAllDispatchers();
                    await RefreshStats();
                    richTextBox1.Text = $"{DateTime.Now}" + " Log: Sleeping for 5 min" + Environment.NewLine + richTextBox1.Text;
                    Console.WriteLine("Sended refreshed");
                }
                else
                    Console.WriteLine("Nothing to do");

                Console.WriteLine("Sleeping for 5 min");
                await Task.Delay(5*60*1000);
            }

        }
    }
}