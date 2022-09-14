using HelloMAUI.Services;
using HelloMAUI.ViewModel;

namespace HelloMAUI;

public partial class MainPage : ContentPage
{	
    public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;		
    }	
}