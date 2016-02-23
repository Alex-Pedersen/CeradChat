using System;
using System.Windows.Forms;
using SimpleClient.ChatForms;
using SimpleServer;
using SimpleServer.ChatForms;
using Timer = System.Windows.Forms.Timer;

namespace SimpleClient.SplashForm
{
    public partial class SplashForm : Form
    {
        private readonly Timer _tmr;

        public SplashForm()
        {
            InitializeComponent();
            _tmr = new Timer {Interval = 3000}; //Sets timer interval
            _tmr.Start(); //Starts the timer
            _tmr.Tick += tmr_Tick; //Ticks the timer
            Show(); //Shows the form
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            _tmr.Stop(); //after 3 sec stop the timer
            Hide(); //Hides displayform
            var lobby = new LoginChat();
            var chatpublic = new PublicChat();
            var serverChat = new ServerChat();
            lobby.Show();
            chatpublic.Show();
            serverChat.Show();
        }


        private void SplashForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}