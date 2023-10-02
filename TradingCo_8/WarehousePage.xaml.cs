using TradingCo.Mechanics;

namespace TradingCo;

public partial class WarehousePage : ContentPage
{

	private AccountPage accountPage { get; set; }
	public MaterialStorage MaterialStorage { get; set; }
	private Account _account { get; set; }
	private Market Market { get; set; }

	public WarehousePage(MaterialStorage materialStorage, Account account, AccountPage accountPage, Market market) {
        InitializeComponent();
        MaterialStorage = materialStorage;
        PopulateStorage();
        _account = account;
        this.accountPage = accountPage;
		this.Market = market;
    }



    private void PopulateStorage() {
		Materials.Clear();
		
		var count = 0;

		var horizStack = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 25, HorizontalOptions = LayoutOptions.Center };

		foreach (var mat in MaterialStorage.MaterialStorageList) {
			count++;

			var frame = new Frame()
			{
				HeightRequest = 150,
				WidthRequest = 150,
				BackgroundColor = Colors.Black,
				BorderColor = Colors.DarkGoldenrod,
				Padding = new Thickness(10)
			};

			var grid = new Grid()
			{
				RowDefinitions = new RowDefinitionCollection()
			{
				new RowDefinition() { Height = 50 },
				new RowDefinition() { Height = 100 }
			},
				ColumnDefinitions = new ColumnDefinitionCollection() {
				new ColumnDefinition() {Width = 70},
				new ColumnDefinition() {Width = 70}
			}
			};

			var image = new Image()
			{
				Source = ImageSource.FromFile(mat.IconName),
				HeightRequest = 50,
				WidthRequest = 50,
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.Start
			};
			grid.Add(image, 0, 0);
			var lbl_0 = new Label()
			{
				Text = mat.Name,
				FontFamily = "RobotoCondensed-Bold",
				FontSize = 21,
				TextColor = Colors.DarkGoldenrod,
				HorizontalOptions = LayoutOptions.Start
			};
			grid.Add(lbl_0, 1, 0);
            var lbl_1 = new Label()
            {
                Text = $"Qty: {mat.Quantity}",
                FontFamily = "RobotoCondensed-Bold",
                FontSize = 18,
                TextColor = Colors.DarkGoldenrod,
                HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.End
            };
			grid.Add(lbl_1, 1, 0);

			var buyBtn = new Button()
			{
				Text = "Buy",
				TextColor = Colors.Black,
                FontFamily = "RobotoCondensed-Bold",
				BorderColor = Colors.Black,
				BorderWidth = 1,
                BackgroundColor = Colors.LawnGreen,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
                WidthRequest = 62,
                HeightRequest = 52,
				BindingContext = mat
            };
            buyBtn.Clicked += (sender, e) => BuyBtn_Clicked(sender, e, mat);
			grid.Add(buyBtn, 0, 1);
            var sellBtn = new Button()
            {
                Text = "Sell",
                TextColor = Colors.Black,
                FontFamily = "RobotoCondensed-Bold",
                BorderColor = Colors.Black,
                BorderWidth = 1,
                BackgroundColor = Colors.Red,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 62,
                HeightRequest = 52,
                BindingContext = mat
            };
            sellBtn.Clicked += (sender, e) => SellBtn_Clicked(sender, e, mat);
			grid.Add(sellBtn, 1, 1);

            frame.Content = grid;
			horizStack.Children.Add(frame);
            if (count == 2) {
                count = 0;
                Materials.Add(horizStack);
                horizStack = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 25, HorizontalOptions = LayoutOptions.Center };
            }
        }
    }

    private async void SellBtn_Clicked(object sender, EventArgs e, Material mat) {
		int amount;

		try {
			Market.pauseMarket = true;
			amount = Int32.Parse(await DisplayPromptAsync("Sell Material", "Enter amount to sell"));
			Market.pauseMarket = false;
		} catch {
            Market.pauseMarket = false;
            return;
        }

        if (mat.Quantity == 0 || mat.Quantity < amount) {
			
			Dispatcher.Dispatch(() => 
				DisplayAlert($"Out of {mat.Name}!", "It appears as though we're out this material... We'll need to buy more before we can sell!", "Okay")
			);
            return;
        }

        mat.Quantity = mat.Quantity - amount;
		_account.SellItem(mat.Price, amount);
		MaterialStorage.SaveMaterialStorageList();
		PopulateStorage();
		accountPage.RefreashStats();
    }

    private async void BuyBtn_Clicked(object sender, EventArgs e, Material mat) {
		int amount;
		
		try {
			Market.pauseMarket = true;
			amount = Int32.Parse(await DisplayPromptAsync("Buy Material", "Enter amount to buy"));
            Market.pauseMarket = false;
        } catch {
            Market.pauseMarket = false;
            return;
        }

        if (_account.GetCurrentBalance() < (mat.Price * amount)) {
			Dispatcher.Dispatch(() =>
				DisplayAlert("Not enough Funds!", "It appears as though we don't have enough funds to make this purchase... Sell some materials to get more!", "Okay")
			);
			return;
        }

        mat.Quantity = mat.Quantity + amount;
        _account.BuyItem(mat.Price, amount);
		MaterialStorage.SaveMaterialStorageList();
        PopulateStorage();
        accountPage.RefreashStats();
    }
}