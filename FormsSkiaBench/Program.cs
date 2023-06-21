namespace FormsSkiaBench
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var main = new Thread(() =>
            {
                using var form = new Form1();
                form.Show();
                while (!form.IsDisposed)
                {
                    form.DoRender();
                    Application.DoEvents();
                }
            });
            main.IsBackground = true;
            main.SetApartmentState(ApartmentState.STA);
            main.Start();
            main.Join();
        }
    }
}