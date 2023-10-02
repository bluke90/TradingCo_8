using TradingCo.Mechanics;

namespace TradingCo;

public partial class MainPage : ContentPage
{
    private MaterialStorage MaterialStorage { get; set; }
	private Market Market { get; set; }
	private Thread RefreashThread { get; set; }
    public MainPage(Market market, MaterialStorage materialStorage)
	{
		InitializeComponent();

		MaterialStorage = materialStorage;
		Market = market;

		RefreashThread = new Thread(RefreashLoop);
		RefreashThread.Start();
	}

	public void PopulateMaterials() {

		MaterialsDisplay.Clear();

		foreach (var mat in MaterialStorage.MaterialStorageList) {

			var frame = new Frame()
			{
				HorizontalOptions = LayoutOptions.Fill,
				BackgroundColor = Color.FromRgba("#9E7A1C"),
			};
			var stackLayout = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				Spacing = 20
			};
			var image = new Image() { Source = ImageSource.FromFile(mat.IconName), BackgroundColor = Color.FromRgba("#9E7A1C"), WidthRequest = 50, HeightRequest = 50 };
			var label_0 = new Label() { Text = mat.Name, VerticalOptions = LayoutOptions.Center, FontSize = 25, FontFamily = "RobotoCondensed-Bold" };
			var label_1 = new Label() { Text = $"Price: ${Math.Round(mat.Price, 2)}", VerticalOptions = LayoutOptions.Center, FontSize = 25, FontFamily = "RobotoCondensed-Bold" };

			stackLayout.Add(image);
            stackLayout.Add(label_0);
            stackLayout.Add(label_1);
			frame.Content = stackLayout;
			MaterialsDisplay.Add(frame);

        }
    }

	private void RefreashLoop() {
		while (true) {
			Thread.Sleep(100);
			this.Dispatcher.Dispatch(PopulateMaterials);
            Thread.Sleep(2400);
        }
	}
}

