using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App3
{
    public partial class MainPage : ContentPage
    {
        Image img;
        Button takePhotoBtn;
        Button getPhotoBtn;
        Image vid;
        Button takeVidBtn;
        Button getVidBtn;
        public MainPage()
        {
            //InitializeComponent();
            takePhotoBtn = new Button { Text = "Сделать фото" };
            getPhotoBtn = new Button { Text = "Выбрать фото" };
            takeVidBtn = new Button { Text = "снять видева" };
            getVidBtn = new Button { Text = "смотреть бесплатно" };
            img = new Image();
            vid = new Image();
            

            // выбор фото
            getPhotoBtn.Clicked += GetPhotoAsync;

            takeVidBtn.Clicked += TakeVideoAsync;

            // съемка фото
            takePhotoBtn.Clicked += TakePhotoAsync;
            getVidBtn.Clicked += GetVideoAsync;

            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    new StackLayout
                    {
                         Children = {takePhotoBtn, getPhotoBtn,takeVidBtn,getVidBtn},
                         Orientation =StackOrientation.Vertical,
                         HorizontalOptions = LayoutOptions.CenterAndExpand
                    },
                    img
                }
            };
        }

        async void GetPhotoAsync(object sender, EventArgs e)
        {
            try
            {
                // выбираем фото
                var photo = await MediaPicker.PickPhotoAsync();
                // загружаем в ImageView
                img.Source = ImageSource.FromFile(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        async void GetVideoAsync(object sender, EventArgs e)
        {
            try
            {
                // выбираем фото
                var video = await MediaPicker.PickVideoAsync();
                // загружаем в ImageView
                vid.Source = ImageSource.FromFile(video.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }


        async void TakePhotoAsync(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                // для примера сохраняем файл в локальном хранилище
                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                // загружаем в ImageView
                img.Source = ImageSource.FromFile(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        async void TakeVideoAsync(object sender, EventArgs e)
        {
            try
            {
                var video = await MediaPicker.CaptureVideoAsync();

                // для примера сохраняем файл в локальном хранилище
                var newFile = Path.Combine(FileSystem.AppDataDirectory, video.FileName);
                using (var stream = await video.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                // загружаем в ImageView
                vid.Source = ImageSource.FromFile(video.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }
}
