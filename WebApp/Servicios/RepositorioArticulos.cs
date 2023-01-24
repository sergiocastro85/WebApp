using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Servicios
{
    public interface IRepositorioArticulos
    {
        Task Crear(Articulo articulo);
    }

    public class RepositorioArticulos:IRepositorioArticulos
    {

        private readonly string connectionString;
        

        public RepositorioArticulos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        //Metodo de Crear
        public async Task Crear (Articulo articulo)
        {

            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO dbo.Articulos (IdCategoria , Nombre,  Marca, PrecioVenta, dtmVigencia)
                                                    VALUES (@IdCategoria, @Nombre, @Marca, @PrecioVenta, @dtmVigencia )

                                                    SELECT SCOPE_IDENTITY();", articulo);
            articulo.Idarticulo= id;    

        }
    }
}
