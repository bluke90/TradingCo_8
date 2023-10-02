using TradingCo.Data;
using TradingCo.Mechanics;

namespace TradingCo;

public partial class AccountPage : ContentPage
{
	private Account _account { get; set; }
    private MaterialContext _db { get; set; }
	private static string AccBalanceStr = "Account Balance: $";
    private static string LastAccBalanceStr = "Last Balance: $";


    public AccountPage(Account account, MaterialContext data)
	{
        _db = data;
		_account = account;
        _account.AccountPage = this;
		InitializeComponent();
		RefreashStats();
        UpdateAccountPage();
    }
	public void RefreashStats() {
        accBalance.Text = AccBalanceStr + Math.Round(_account.GetCurrentBalance(), 2);
        lastAccBalance.Text = LastAccBalanceStr + Math.Round(_account.GetLastBalance(), 2);
    }


    public Task PopulateTransaction(Transaction transaction) {
        var frame = GenerateTransactionFrame(transaction);

        transactionStack.Add(frame);
        return Task.CompletedTask;
    }

    public async void UpdateAccountPage() {
        transactionStack.Clear();

        var transactions = _db.Transactions.ToList();
        foreach (var tran in transactions) {
            await PopulateTransaction(tran);
        }
    }

	private Frame GenerateTransactionFrame(Transaction transaction) {

        var frame = new Frame()
        {
            BackgroundColor = Colors.Black,
            CornerRadius = 0,
            BorderColor = Colors.DarkGoldenrod,
            Padding = new Thickness(10, 20)
        };

        var stack = new StackLayout()
        {
            VerticalOptions = LayoutOptions.Center,
            Orientation = StackOrientation.Horizontal,
            Spacing = 10
        };
        var prebal_lbl = new Label()
        {
            Text = "Pre-Balance: $" + Math.Round(transaction.PreBalance, 2).ToString(),
            FontSize = 14,
            FontFamily = "RobotoCondensed-Semibold",
            TextColor = Colors.DarkGoldenrod
        };
        stack.Add(prebal_lbl);
        var amnt_lbl = new Label()
        {
            Text = "Amount: " + Math.Round(transaction.Amount, 2).ToString(),
            FontSize = 14,
            FontFamily = "RobotoCondensed-Semibold",
            TextColor = Colors.Red
        };
        stack.Add(amnt_lbl);
        var postbal_lbl = new Label()
        {
            Text = "Balance: $" + Math.Round(transaction.PostBalance, 2).ToString(),
            FontSize = 14,
            FontFamily = "RobotoCondensed-Semibold",
            TextColor = Colors.DarkGoldenrod
        };
        stack.Add(postbal_lbl);
        frame.Content = stack;
        return frame;
    }


}