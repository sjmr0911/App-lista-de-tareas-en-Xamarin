using System;
using Xamarin.Forms;
using TodoApp.Models;
using TodoApp.Data;

namespace TodoApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await TareaDatabase.Init();
            ListaTareas.ItemsSource = await TareaDatabase.ObtenerTareasAsync();
        }

        private async void OnAgregarTareaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarEditarTareaPage());
        }

        private async void OnTareaSeleccionada(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Tarea tarea)
            {
                await Navigation.PushAsync(new AgregarEditarTareaPage(tarea));
            }

            ListaTareas.SelectedItem = null;
        }
    }
}