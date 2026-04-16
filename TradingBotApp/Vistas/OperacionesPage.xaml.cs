using TradingBotApp.ViewModels;

namespace TradingBotApp.Vistas;

public partial class OperacionesPage : ContentPage
{
	private readonly OperacionesViewModel _viewModel;

	public OperacionesPage(OperacionesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.CargarAsync();
    }
}