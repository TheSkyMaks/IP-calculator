using Xamarin.Forms;

namespace IP_calculator
{
    class StartPage : ContentPage
    {        
        public StartPage()
        {
            Label header = new Label() { Text = "Привет из Xamarin Forms" };
            Content = header;
            //Данный класс представляет страницу, поэтому наследуется от класса ContentPage. 
            //В конструкторе создается метка с текстом, которая задается в качестве содержимого страницы (Content = header).
        }
    }
}
