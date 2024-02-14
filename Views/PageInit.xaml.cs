namespace PM02PO12024;

public partial class PageInit : ContentPage
{
    Controllers.PersonasControllers PersonDB;
    FileResult photo;

    public PageInit()
	{
		InitializeComponent();
        
	}

    public String GetImage64() {

        if (photo != null) {
            using(MemoryStream ms = new MemoryStream()) {
                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();

                String Base64 = Convert.ToBase64String(data);

                return Base64;


            }
        }

        return null; 
    }

    public byte[] GetImageArray()
    {

        if (photo != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();

                return data;
            }
        }

        return null;
    }


    private async void Btnprocesar_Clicked(object sender, EventArgs e)
    {
        var person = new Models.Personas
        {
            Nombres = nombres.Text,
            Apellidos = apellidos.Text,
            FechaNac = FechaNac.Date,
            telefono = telefono.Text,
            foto = GetImage64()

        };

            if (await App.Database.StorePerson(person) > 0)
        {
            await DisplayAlert("Aviso", "Registro ingresado con exito!!", "OK"); 
        }
    }

    private async void Btnfoto_Clicked(object sender, EventArgs e)
    {
        photo = await MediaPicker.CapturePhotoAsync();
        
        if (photo != null) {
            String path = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream sourcephoto = await photo.OpenReadAsync();
            using FileStream Streamlocal = File.OpenWrite(path);

            foto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);

            await sourcephoto.CopyToAsync(Streamlocal);
        }
    }
}