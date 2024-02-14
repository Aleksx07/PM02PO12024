namespace PM02PO12024.Views;

public partial class PageListPersonas : ContentPage
{
	public PageListPersonas()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        var page = new PageInit();
        await Navigation.PushAsync(page);
    }

    private void listperson_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listperson.ItemsSource = await App.Database.GetListPersons();
    }

    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        var page = new Views.PageMaps();
        await Navigation.PushAsync(page);
    }
}