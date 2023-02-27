using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Servicios
{

    public interface IRepositorioProveedor
    {
        Task Actualizar(Proveedor proveedor);
        Task Borrar(int IdProveedor);
        Task<IEnumerable<Proveedor>> Buscar();
        Task Crear(Proveedor proveedor);
        //Task<Proveedor> ObtenerPorId(int IdProveedor);
        Task<IEnumerable<Proveedor>> ObtenerProveedor();
        Task<Proveedor> ObternerPorId(int IdProveedor);
    }
    public class RepositorioProveedor:IRepositorioProveedor
    {
        private readonly string connectionString;

        public RepositorioProveedor(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        //Metodo de Crear
        public async Task Crear(Proveedor proveedor)
        {

            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO dbo.Proveedor(Identificacion,Nombre,Direccion,Telefono)
                                                            VALUES(@Identificacion,@Nombre,@Direccion,@Telefono)

                                                    SELECT SCOPE_IDENTITY();", proveedor);
            proveedor.IdProveedor = id;

        }

        public async Task<IEnumerable<Proveedor>> Buscar()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Proveedor>(@"SELECT IdProveedor,Identificacion,Nombre,Direccion,Telefono
                                                            FROM SistemaWeb.dbo.Proveedor
                                                            WHERE IdProveedor=@IdProveedor;");
        }

        //metodo que nos va dar la imfarcación del proveedor a editar
        public async Task<IEnumerable<Proveedor>> ObtenerProveedor()
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Proveedor>(@"SELECT IdProveedor,
                                                                   Identificacion,
                                                                   Nombre,
                                                                   Direccion,
                                                                   Telefono
                                                            FROM dbo.Proveedor;");

        }

        public async Task<Proveedor> ObternerPorId(int IdProveedor)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Proveedor>(@"SELECT IdProveedor,
                                                                           Identificacion,
                                                                           Nombre,
                                                                           Direccion,
                                                                           Telefono
                                                                    FROM SistemaWeb.dbo.Proveedor
                                                                    WHERE IdProveedor = @IdProveedor;", new {IdProveedor});

        }

        public async Task Actualizar(Proveedor proveedor)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE dbo.Proveedor
                                            SET Identificacion = @Identificacion,
                                                Nombre = @Nombre,
                                                Direccion = @Direccion,
                                                Telefono = @Telefono
                                            WHERE IdProveedor = @IdProveedor;", proveedor);

        }


        public async Task Borrar(int IdProveedor)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE dbo.Proveedor
                                            WHERE IdProveedor=@IdProveedor;", new { IdProveedor });

        }
    }
}
