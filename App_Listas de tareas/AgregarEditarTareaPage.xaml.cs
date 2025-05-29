using System;
using Xamarin.Forms;
using TodoApp.Models;
using TodoApp.Data;

namespace TodoApp
{
    public partial class AgregarEditarTareaPage : ContentPage
    {
        private Tarea tarea;

        public AgregarEditarTareaPage(Tarea tarea = null)
        {
            InitializeComponent();
            this.tarea = tarea;

            if (tarea != null)
            {
                NombreEntry.Text = tarea.Nombre;
                DescripcionEntry.Text = tarea.Descripcion;
                EliminarBtn.IsVisible = true;
            }
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            await TareaDatabase.Init();

            if (string.IsNullOrWhiteSpace(NombreEntry.Text))
            {
                await DisplayAlert("Error", "El nombre es obligatorio.", "OK");
                return;
            }

            if (tarea == null)
                tarea = new Tarea();

            tarea.Nombre = NombreEntry.Text;
            tarea.Descripcion = DescripcionEntry.Text;

            await TareaDatabase.GuardarTareaAsync(tarea);
            await Navigation.PopAsync();
        }

        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirmar", "¿Eliminar esta tarea?", "Sí", "No");
            if (confirm)
            {
                await TareaDatabase.EliminarTareaAsync(tarea);
                await Navigation.PopAsync();
            }
        }
    }
}