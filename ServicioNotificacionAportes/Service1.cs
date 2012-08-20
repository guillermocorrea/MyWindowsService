using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.IO;

namespace ServicioNotificacionAportes
{
    public partial class Service1 : ServiceBase
    {
        private Timer t = null;

        public Service1()
        {
            InitializeComponent();
            // System.Configuration.ConfigurationManager.AppSettings["Intervalo"];
            t = new Timer(Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["Intervalo"]));
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
        }

        protected override void OnStart(string[] args)
        {
            t.Start(); //Iniciamos el timer
        }

        protected override void OnStop()
        {
            t.Stop();
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string path = @"C:\log.txt";
                TextWriter tw = new StreamWriter(path, true);
                tw.WriteLine("A fecha de : " + DateTime.Now.ToString() + ", Intervalo: " + t.Interval.ToString());
                tw.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Exception: " + ex.Message);
            }
        }
    }
}
