﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using loader;
using Newtonsoft.Json;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;

namespace loader
{
    public partial class Main : Form
    {
        // https://www.codeproject.com/Articles/11114/Move-window-form-without-Titlebar-in-C
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Main()
        {
            InitializeComponent();
        }

        public void loadcontent(JObject result)
        {
            welcome.Text = $"Welcome back {result["username"].ToString()} ({result["uid"].ToString()})";
            avatar.ImageLocation = result["avatarurl"].ToString();


            if (result["cheatstatus"].ToString() == "0")
            {
                statustext.Text = "Undetected";
                statustext.ForeColor = System.Drawing.Color.Green;
            }
            else if (result["cheatstatus"].ToString() == "1")
            {
                statustext.Text = "Detected";
                statustext.ForeColor = System.Drawing.Color.Red;
            }
            else if (result["cheatmaintenance"].ToString() == "1")
            {
                statustext.Text = "Maintenance";
                statustext.ForeColor = System.Drawing.Color.Yellow;
            }

            else if (result["frozen"].ToString() == "1")
            {
                statustext.Text = "Subs are frozen.";
                statustext.ForeColor = System.Drawing.Color.Yellow;
            }

            string sub = result["sub"].ToString();
            subtext.Text = $"You have {checksub(sub).ToString()} day/s left.";

            Inviter.Text = $"Inviter: {result["invitedBy"].ToString()}";
        }

        public int checksub(string sub)
        {
            if (string.IsNullOrEmpty(sub))
            {
                return 0;
            }
            else
            {
                int value = (DateTime.Parse(sub, CultureInfo.InvariantCulture) - DateTime.Now).Days;
                return value;
            }


        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
