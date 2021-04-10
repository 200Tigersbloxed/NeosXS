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
        NeosXSAPI nsxapi = new NeosXSAPI();
        System.Timers.Timer aTimer;
        public int XSOPortGlobal = 42069;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            HostTextBox.Text = nsxapi.GetSocketHost();
            PortTextBox.Text = nsxapi.GetSocketPort().ToString();
            SetTimer();
        }

        private void StartSocketButton_Click(object sender, EventArgs e)
        {
            if (!nsxapi.IsSocketListening())
                nsxapi.StartSocket();
        }

        private void StopSocketButton_Click(object sender, EventArgs e)
        {
            if (nsxapi.IsSocketListening())
                nsxapi.StopSocket();
        }

        private void RestartSocketButton_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "RESTARTING...";
            nsxapi.RestartSocket();
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
            if (nsxapi.IsSocketListening())
                StatusLabel.Text = "CONNECTED";
            else
                StatusLabel.Text = "DISCONNECTED";

            nsxapi.SetSocketHost(HostTextBox.Text);
            nsxapi.SetSocketPort(Int32.Parse(PortTextBox.Text));
        }

        private void TestXSButton_Click(object sender, EventArgs e)
        {
            try
            {
                XSHelper.SendNotification("Test", "Did it work?", XSOPortGlobal);
            }
            catch (Exception) { }
        }

        private void XSOPortText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // make sure it's a number
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // Update the XSOverlay Port
            try
            {
                XSOPortGlobal = Int32.Parse(XSOPortText.Text);
                nsxapi.SetSocketXSOPort(XSOPortGlobal);
                nsxapi.UpdateXSOPort();
            }
            catch (Exception) { /*it's either null or one character*/ }
        }
    }
}
