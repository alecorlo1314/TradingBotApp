using TradingBotApp.ViewModels;

namespace TradingBotApp.Vistas;

public partial class BotConfigPage : ContentPage
{
    private readonly BotConfigViewModel _botConfigViewModel;

    public BotConfigPage(BotConfigViewModel botConfigViewModel)
	{
		InitializeComponent();

		BindingContext = _botConfigViewModel = botConfigViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            await _botConfigViewModel.CargarEstatusAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"CRASH: {ex}");
        }
    }
}