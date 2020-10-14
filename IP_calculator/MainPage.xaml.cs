using System;
using Xamarin.Forms;

namespace IP_calculator
{
    /// <summary>
    /// MainPage
    /// </summary>
    public partial class MainPage : ContentPage
    {
        //private readonly Label textLabel = new Label();
        /// <summary>
        /// MainPage()
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            IP_entry.TextChanged += IP_TextChanged;
            Mask_entry.TextChanged += Mask_TextChanged;
        }

        private void Mask_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void IP_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
            //textLabel.Text = IP_entry.Text;
        }
    }
}