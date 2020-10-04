using System;
using Xamarin.Forms;

namespace IP_calculator
{
    /// <summary>
    /// MainPage
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private readonly Label textLabel = new Label();
        /// <summary>
        /// MainPage()
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            IP_entry.TextChanged += IP_TextChanged;

        }

        private void IP_TextChanged(object sender, TextChangedEventArgs e)
        {
            textLabel.Text = IP_entry.Text;
        }
    }
}