using System.Diagnostics;
using System.Windows.Input;
using System.ServiceProcess;

namespace KeyboardHooker
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            HookHandler.SetupHook();
            GlobalHotKey setupApplication = new GlobalHotKey(ModifierKeys.Control | ModifierKeys.Shift, Key.L, SetupApplication);
            HookHandler.AddHotKey(setupApplication);
        }

        protected override void OnStop()
        {
            HookHandler.ShutdownHook();
        }

        /// <summary>
        /// Функция запуска процесса
        /// </summary>
        private void SetupApplication()
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = @"E:\VisualStudio Projects\Projects\BC\BC\bin\Debug\BC.exe",
                Arguments = "https://vk.com/eeegggooorrr_a"
            });
        }
    }
}