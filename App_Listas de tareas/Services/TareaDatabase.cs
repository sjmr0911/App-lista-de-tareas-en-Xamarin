using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Models;
using System.IO;
using System;


namespace TodoApp.Data
{
    public class TareaDatabase
    {
        private static SQLiteAsyncConnection database;

        public static async Task Init()
        {
            if (database != null)
                return;

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tareas.db3");
            database = new SQLiteAsyncConnection(dbPath);
            await database.CreateTableAsync<Tarea>();
        }

        public static Task<List<Tarea>> ObtenerTareasAsync() =>
            database.Table<Tarea>().ToListAsync();

        public static Task<int> GuardarTareaAsync(Tarea tarea) =>
            tarea.Id != 0 ? database.UpdateAsync(tarea) : database.InsertAsync(tarea);

        public static Task<int> EliminarTareaAsync(Tarea tarea) =>
            database.DeleteAsync(tarea);
    }
}