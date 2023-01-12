using System.Net;
using System.Text.RegularExpressions;
using Dispatcher;

namespace DispatcherDarkorbit
{
    public partial class Form1 : Form
    {
        public static HttpManager Client { get; set; }
        public Form1()
        {
            InitializeComponent();

            var cookie = new Cookie("dosid" , "");
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
            Console.WriteLine(result);

            string endsAtSlot1 = "";
            var time = Regex.Match(result, "dispatch_collect_elem_1\", \"(.*?)\"");
            if (time.Success)
            {
                endsAtSlot1 = time.Groups[1].Value;
                richTextBox1.Text += $"{DateTime.Now}" +  " Log: " + "Slot1 expected end: " + endsAtSlot1 + Environment.NewLine;
            }

            Console.WriteLine(endsAtSlot1);

            //var request = await Client.PostAsyncLimitRaw($"https://{ServerTxt.Text}.darkorbit.com/ajax/dispatch.php", "command=sendDispatch&dispatchId=dispatch_retriever_a01");
            //Console.WriteLine("Success: " + request);
            

            //commands command=collectDispatch&slot=2 // collect
            //send //command=sendDispatch&dispatchId=dispatch_retriever_a01

        }
    }
}