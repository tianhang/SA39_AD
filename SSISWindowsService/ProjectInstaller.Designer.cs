namespace SSISWindowsService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SSISServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SSISServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SSISServiceProcessInstaller
            // 
            this.SSISServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SSISServiceProcessInstaller.Password = null;
            this.SSISServiceProcessInstaller.Username = null;
            // 
            // SSISServiceInstaller
            // 
            this.SSISServiceInstaller.Description = "This service automatically notifies reorders and updates deputy status";
            this.SSISServiceInstaller.DisplayName = "SSIS Automatic Service";
            this.SSISServiceInstaller.ServiceName = "SSISService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SSISServiceProcessInstaller,
            this.SSISServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SSISServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SSISServiceInstaller;
    }
}