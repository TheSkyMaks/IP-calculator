using Xamarin.Forms;

namespace IP_calculator
{
    /// <summary>
    /// MainPage
    /// </summary>
    public partial class MainPage : ContentPage
    {
        internal bool IsBinary;
        internal string[] IP;
        internal string[] Mask;
        /// <summary>
        /// MainPage()
        /// </summary>
        public MainPage()
        {
            IP = new string[4];
            Mask = new string[4];
            InitializeComponent();
            IsBinary = false;
            EntryBinaryOrDecimal(false);
        }
        private void IP_TextChanged(object sender, Syncfusion.XForms.MaskedEdit.ValueChangedEventArgs e)
        {
            IP = e.Value.ToString().Split('.');
        }

        private void Mask_TextChanged(object sender, Syncfusion.XForms.MaskedEdit.ValueChangedEventArgs e)
        {
            Mask = e.Value.ToString().Split('.');
        }       

        private void BinarOrDecimal_CellChanged(object sender, ToggledEventArgs e)
        {
            EntryBinaryOrDecimal(e.Value);
        }

        private void EntryBinaryOrDecimal(bool isBinary)
        {
            var Calculator = new IPcalcul();
            if (isBinary)
            {
                IsBinary = true;
                Mask_entr.Mask = "0000 0000.0000 0000.0000 0000.0000 0000";
                Mask_entr.Watermark = "Your Mask. Example: 1111 1111.1111 1111.0000 0000.0000 0000";
                IP_entr.Mask = "0000 0000.0000 0000.0000 0000.0000 0000";
                IP_entr.Watermark = "Your IP. Example: 1111 1111.1111 1111.0100 0000.0000 0001";
                Calculator.StartWithBinary(IP, Mask);
            }
            else
            {
                IsBinary = false;
                IP_entr.Mask = "000.000.000.000";
                IP_entr.Watermark = "Your IP. Example: 192.168.0.0";
                Mask_entr.Mask = "000.000.000.000";
                Mask_entr.Watermark = "Your Mask. Example: 255.255.0.0";
                Calculator.StartWithDecimal(IP, Mask);
            }
            Result_label.Text = Calculator.Results;
        }
        /*
        private void Mask_ShortOrLong_CellChanged(object sender, ToggledEventArgs e)
        {
            var isShortMask = e.Value;
        }
        */
    }
}