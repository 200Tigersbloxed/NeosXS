using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using NeosXS;

namespace NeosXS_Headless
{
    public partial class MainForm : Form
    {
        WebsocketHelper wsh = new WebsocketHelper();
        System.Timers.Timer aTimer;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            HostTextBox.Text = wsh.host;
            PortTextBox.Text = wsh.port.ToString();
            SetTimer();
        }

        private void StartSocketButton_Click(object sender, EventArgs e)
        {
            if (!wsh.IsSocketServerListening)
                wsh.StartSocket();
        }

        private void StopSocketButton_Click(object sender, EventArgs e)
        {
            if (wsh.IsSocketServerListening)
                wsh.StopSocket();
        }

        private void RestartSocketButton_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "RESTARTING...";
            wsh.RestartSocket();
        }

        // Timer
        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(10000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (wsh.IsSocketServerListening)
                StatusLabel.Text = "CONNECTED";
            else
                StatusLabel.Text = "DISCONNECTED";

            wsh.host = HostTextBox.Text;
            wsh.port = Int32.Parse(PortTextBox.Text);
        }

        private void TestXSButton_Click(object sender, EventArgs e)
        {
            NotificationSender.OnUserJoined("Test", "Test");
        }
    }
}
