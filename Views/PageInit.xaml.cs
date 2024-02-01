namespace PM02PO12024;

public partial class PageInit : ContentPage
{
    Controllers.PersonasControllers PersonDB; 
	public PageInit()
	{
		InitializeComponent();
	}
    public PageInit(Controllers.PersonasControllers dbpath)
    {
        InitializeComponent();
        PersonDB = dbpath;
    }

    private async void btnprocesar_Clicked(object sender, EventArgs e)
    {
        var person = new Models.Personas
        {
            Nombres = nombres.Text,
            Apellidos = apellidos.Text,
            FechaNac = FechaNac.Date,
            telefono = telefono.Text 

        };

        if (await PersonDB.StorePerson(person) > 0)
        {
            await DisplayAlert("Aviso", "Registro ingresado con exito!!", "OK"); 
        }
    }
}