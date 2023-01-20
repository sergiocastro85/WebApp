using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Servicios
{

    //interfaz
    public interface IRepositoriosCategorias
    {
        Task Actualizar(Categoria categoria);
        Task Borrar(int id);
        Task Crear(Categoria categoria);
        Task<bool> Existe(string nombre);
        Task<IEnumerable<Categoria>> Obtener();
        Task<Categoria> ObtenerId(int id);
    }

    //la clase la ponemos a heredar de la interface
    public class RepositoriosCategorias:IRepositoriosCategorias
    {
        private readonly string connectionString;

        //Códgio para insertar la categoría en la BD
        //Creamos el constructor de la clase
        //Creamos el IConfiguration, para acceder al conection string
        public RepositoriosCategorias(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Metodo para poder crear la categoría en la BD
        public async Task  Crear(Categoria categoria)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO dbo.Categrias(Nombre,logActivo) 
                                                VALUES (@Nombre,0); SELECT SCOPE_IDENTITY();", categoria);
            categoria.Idcategoria = id;
        }

        public async Task<bool>Existe(string nombre)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1
                                                                            FROM dbo.Categrias
                                                                            WHERE Nombre = @Nombre;", new { nombre });
            return existe == 1;
        }

        public async Task<IEnumerable<Categoria>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Categoria>(@"SELECT IdCategoria,
                                                           Nombre
                                                            FROM dbo.Categrias;
                                                            ");

        }

        //Metodo Actualizar
        public async Task Actualizar (Categoria categorias)
        {
            using var connection = new SqlConnection(connectionString);

            //me permite ejecutar un query que no va retornar nada
            await connection.ExecuteAsync(@"UPDATE dbo.Categrias
                                            SET Nombre=@Nombre
                                            WHERE IdCategoria=@IdCategoria", categorias);
          
            
        }
        public async Task<Categoria> ObtenerId(int id)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<Categoria>(@"SELECT IdCategoria,Nombre
                                                                            FROM dbo.Categrias
                                                                            WHERE IdCategoria=@id", new { id });
            
        }

        public async Task Borrar (int id)
        {
            using var connertion=new SqlConnection(connectionString);

            await connertion.ExecuteAsync("DELETE dbo.Categrias WHERE IdCategoria=@id", new { id });
        }

    }
}
