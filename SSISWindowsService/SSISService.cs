using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ClassLibrary.Controllers;
using ClassLibrary.Helper;

namespace SSISWindowsService
{
    partial class SSISService : ServiceBase
    {
        private Timer timer;
        private static ErrorLog errorobj;

        public SSISService()
        {
            InitializeComponent();
            timer = new System.Timers.Timer(60000);
            timer.Elapsed += OnTimedEvent;
            errorobj = new ErrorLog();
        }

        protected override void OnStart(string[] args)
        {
            timer.Enabled = true;
            //timer.Start();
            try
            {
                GetBackAuthorityController getBackAuthorityController = new GetBackAuthorityController();
                NotifyReordersController notifyReordersController = new NotifyReordersController();
                getBackAuthorityController.run();
                notifyReordersController.run();
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("SSISService-OnTimedEvent():::" + exception.ToString());
            }
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                GetBackAuthorityController getBackAuthorityController = new GetBackAuthorityController();
                NotifyReordersController notifyReordersController = new NotifyReordersController();
                getBackAuthorityController.run();
                notifyReordersController.run();
            }
            catch (Exception exception)
            {
                errorobj.WriteErrorLog("SSISService-OnTimedEvent():::" + exception.ToString());
            }
            

        }
    }
}
