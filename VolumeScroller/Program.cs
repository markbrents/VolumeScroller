using System;
using System.Threading;
using System.Windows.Forms;

namespace VolumeScroller
{
    static class Program
    {
        private static Mutex mutx;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;

            mutx = new Mutex(true, "VolumeScrollerMutex", out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Volume Scroller is already running");
                return; 
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
