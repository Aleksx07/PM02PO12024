using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using PM02PO12024.Models;

namespace PM02PO12024.Controllers
{
    public class PersonasControllers
    {
        readonly SQLiteAsyncConnection _connection;

        // Constructor Vacion
        public PersonasControllers()
        {
            SQLite.SQLiteOpenFlags extensiones = SQLite.SQLiteOpenFlags.ReadWrite |
                                                SQLite.SQLiteOpenFlags.Create |
                                                SQLite.SQLiteOpenFlags.SharedCache;

            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBPersonsas.db3"), extensiones);

            _connection.CreateTableAsync<Personas>();
        }



        // Crear los metodos Crud para la clase Personas
        // Create  // Update
        public async Task<int> StorePerson(Personas personas)
        {

            if (personas.Id == 0)
            {
                return await _connection.InsertAsync(personas);
            }
            else
            {
                return await _connection.UpdateAsync(personas);
            }
        }

        // Read
        public async Task<List<Models.Personas>> GetListPersons()
        {

            return await _connection.Table<Personas>().ToListAsync();
        }

        // Read Element
        public async Task<Models.Personas> GePerson(int pid)
        {

            return await _connection.Table<Personas>().Where(i => i.Id == pid).FirstOrDefaultAsync();
        }

        // Delete Element
        public async Task<int> DeletePerson(Personas personas)
        {

            return await _connection.DeleteAsync(personas);
        }

    }
}