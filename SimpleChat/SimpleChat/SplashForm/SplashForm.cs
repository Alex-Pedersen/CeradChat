using System;
using System.Windows.Forms;
using SimpleChat;
using Timer = System.Windows.Forms.Timer;

namespace SimpleClient
{
    public partial class SplashForm : Form
    {
        public Timer _tmr;
        public bool checker = false;

        public SplashForm()
        {
            InitializeComponent();
            _tmr = new Timer {Interval = 3000}; //Sets timer interval
            _tmr.Start(); //Starts the timer
            _tmr.Tick += tmr_Tick; //Ticks the timer

            Show();
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            _tmr.Stop(); //after 3 sec stop the timer
            Hide(); //Hides displayform
            Lobby lobby = new Lobby();
            lobby.Show();
        }


        private void SplashForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}