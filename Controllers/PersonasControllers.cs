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
        SQLiteAsyncConnection _connection;

        //Constructor vacio 
        public PersonasControllers() { }

        //Conexion a la base de datos
        async Task Init()
        {
            if (_connection is not null)
            {
                return; 
            }
            SQLite.SQLiteOpenFlags extensiones = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | 
                                                 SQLiteOpenFlags.SharedCache; 
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBPersonas.db3"), extensiones);

            var creacion = await _connection.CreateTableAsync<Personas>();
        }

        //Crear los metodos Crud para la clase Personas 
        //Create //Update 
        public async Task<int> StorePerson(Personas personas)
        {
            await Init();
            if(personas.Id == 0)
            {
                return await _connection.InsertAsync(personas);

            } else
            {
                return await _connection.UpdateAsync(personas);
            }
        }

        //Read 
        public async Task<List<Models.Personas>> GetListPersons(){
            await Init();
            return await _connection.Table<Models.Personas>().ToListAsync();

        }

        //Read Element
        public async Task<Models.Personas> GetListPersons(int pid)
        {
            await Init();
            return await _connection.Table<Personas>().Where(i => i.Id == pid).FirstOrDefaultAsync();

        }

        //Delete Element 
        public async Task<int> DeletePerson(Personas personas)
        {
            await Init();
            return await _connection.DeleteAsync(personas);

        }



    }
}
