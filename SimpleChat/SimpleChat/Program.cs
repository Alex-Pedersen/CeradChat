using System;
using System.Windows.Forms;

namespace SimpleClient
{
    internal static class Program
    {
        /// <summary>
        ///     Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashForm.SplashForm());
        }

        public static void Invoke(this Control control, Action action)
        {
            control.Invoke(action);
        }
    }
}