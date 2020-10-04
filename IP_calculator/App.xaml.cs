using Xamarin.Forms;

namespace IP_calculator
{
    /// <summary>
    /// Start point of app
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Start point of app
        /// </summary>
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        /// <summary>
        /// Handle when your app starts
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Handle when your app sleeps
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        ///  Handle when your app resumes
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}