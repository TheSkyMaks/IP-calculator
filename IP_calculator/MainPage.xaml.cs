using Xamarin.Forms;

namespace IP_calculator
{
    /// <summary>
    /// MainPage
    /// </summary>
    public partial class MainPage : ContentPage
    {
        #region XamarinView
        internal bool IsBinary;
        internal IPcalc Calculator;
        internal string[] IP_start;
        internal string[] Mask_start;
        /// <summary>
        /// MainPage()
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            EntryBinaryOrDecimal(false);
        }

        private void IP_TextChanged(object sender, Syncfusion.XForms.MaskedEdit.ValueChangedEventArgs e)
        {
            IP_start = e.Value.ToString().Split('.');
            CalculatePLS();
        }

        private void Mask_TextChanged(object sender, Syncfusion.XForms.MaskedEdit.ValueChangedEventArgs e)
        {
            Mask_start = e.Value.ToString().Split('.');
            CalculatePLS();
        }

        private void BinarOrDecimal_CellChanged(object sender, ToggledEventArgs e)
        {
            EntryBinaryOrDecimal(!IsBinary);
        }

        private void EntryBinaryOrDecimal(bool isBinary)
        {
            IP_entr.Mask = "";
            IP_entr.Watermark = "";
            Mask_entr.Mask = "";
            Mask_entr.Watermark = "";

            string[] seed;
            if (isBinary)
            {
                IsBinary = true;
                seed = Seed_Binary();                
                IP_entr.Mask += "00000000.00000000.00000000.00000000";
                IP_entr.Watermark += "Your IP. Example: 11111111.11111111.01000000.00000001";
                Mask_entr.Mask += "00000000.00000000.00000000.00000000";
                Mask_entr.Watermark += "Your Mask. Example: 11111111.11111111.10000000.00000000";                
            }
            else
            {
                IsBinary = false;
                seed = Seed_Decimal();                
                IP_entr.Mask += "000.000.000.000";
                IP_entr.Watermark += "Your IP. Example: 192.168.0.0";
                Mask_entr.Mask += "000.000.000.000";
                Mask_entr.Watermark += "Your Mask. Example: 255.255.0.0";               
            }
            IP_start = seed;
            Mask_start = seed;
            CalculatePLS();
        }

        private string[] Seed_Decimal()
        {
            string[] seed = new string[4];
            for (int i = 0; i < seed.Length; i++)
            {
                seed[i] = "000";
            }
            return seed;
        }

        private string[] Seed_Binary()
        {
            string[] seed = new string[4];
            for (int i = 0; i < seed.Length; i++)
            {
                seed[i] = "00000000";
            }
            return seed;
        }

        private void CalculatePLS()
        {
            Calculator = new IPcalc
            {
                ErrorMessage = "",
                Results = ""
            };
            if (IsBinary)
            {
                Calculator.StartWithBinary(IP_start, Mask_start);
            }
            else
            {
                Calculator.StartWithDecimal(IP_start, Mask_start);
            }
            Error_label.Text = Calculator.ErrorMessage;
            Result_label.Text = Calculator.Results;
        }
        #endregion
    }
}