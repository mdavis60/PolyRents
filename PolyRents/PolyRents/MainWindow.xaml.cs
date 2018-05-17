using PolyRents.helpers;
using PolyRents.views;
using System;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using PolyRents.ComputingResourcesDataSetTableAdapters;
using PolyRents.views.manage;
using System.Timers;
using System.Threading.Tasks;

namespace PolyRents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window    {

        private String myStatus;
        private Timer statusTimer;

        public String Status
        {
            get
            {
                return myStatus;
            }
            set
            {
                myStatus = value;
                status.Text = value;
                status.ToolTip = value == "" ? null : value;
            }
        }

        public void OnLoggerMessageChanged(object sender, MessageEventArgs e)
        {
            statusTimer.Stop();
            Status = e.Message;

            if (e.Level == "FATAL")
            {
                return;
            }

            statusTimer.Start();
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Status = "";
            });
        }

        private Logger logger = Logger.getInstance();

        public delegate void OnMessageChanged(string message);
        
        public String InformationStatus
        {
            get { return myStatus; }
            set {
                myStatus = value;
                status.Text = value;
            }
        }

        public MainWindow()
        {

            statusTimer = new Timer(15000);
            statusTimer.Elapsed += OnTimedEvent;

            logger.MessageChanged += new MessageChangedHandler(OnLoggerMessageChanged);

            InitializeComponent();

            InformationStatus = "ready";
        }
    }
}
