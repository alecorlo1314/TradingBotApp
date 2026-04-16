using TradingBotApp.ViewModels;

namespace TradingBotApp.Vistas;

public partial class DashboardPage : ContentPage
{
	private readonly DashboardViewModel _dashboardViewModel;
	public DashboardPage(DashboardViewModel dashboardViewModel)
	{
		InitializeComponent();

		//Inyeccion de dependencias
        BindingContext = _dashboardViewModel = dashboardViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        //Cargar datos al abrir la vista Dashboard
        await _dashboardViewModel.CargarAsync();
    }
}
