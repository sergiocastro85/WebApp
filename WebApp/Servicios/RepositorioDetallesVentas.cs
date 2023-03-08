using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Servicios
{

    public interface IRepositorioDetalleVentas
    {
        Task Actualizar(DetalleVenta detalleVenta);
        Task<IEnumerable<DetalleVenta>> Buscar();
        Task Crear(DetalleVenta detalleVenta);
        Task<DetalleVenta> ObtenerPorId(int IdDetalleVenta);
    }
    public class RepositorioDetallesVentas:IRepositorioDetalleVentas
    {
        private readonly string connectionString;

        public RepositorioDetallesVentas (IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(DetalleVenta detalleVenta)
        {
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>("spDetallesVenta",
                                                    new { detalleVenta.IdArticulo, detalleVenta.Cantidad, detalleVenta.Precio, detalleVenta.DtmFecha,detalleVenta.rTotal },
                                                    commandType: System.Data.CommandType.StoredProcedure);


            detalleVenta.IdArticulo = id;

        }

        public async Task Actualizar(DetalleVenta detalleVenta)
        {
            using var connection=new SqlConnection(connectionString);

             await connection.ExecuteAsync("spDetallesVentaUpdate", new
            {
                detalleVenta.IdDetalleVenta,
                detalleVenta.IdArticulo,
                detalleVenta.Cantidad,
                detalleVenta.Precio,
                detalleVenta.DtmFecha,
                detalleVenta.rTotal
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<DetalleVenta> ObtenerPorId(int IdDetalleVenta)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<DetalleVenta>(@"SELECT dv.*,ar.Nombre FROM dbo.DetalledeVentas dv
                                                                            INNER JOIN dbo.Articulos ar
                                                                            ON ar.IdArticulo = dv.IdArticulo
                                                                            WHERE dv.IdDetalleVenta=@IdDetalleVenta", new { IdDetalleVenta });


        }

        public async Task<IEnumerable<DetalleVenta>> Buscar()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<DetalleVenta>(@"SELECT dv.IdDetalleVenta,
                                                                               ar.Nombre,
                                                                               dv.Cantidad,
                                                                               dv.Precio,
                                                                               dv.rTotal,
                                                                               dv.DtmFecha
                                                                        FROM dbo.DetalledeVentas dv
                                                                            INNER JOIN dbo.Articulos ar
                                                                                ON ar.IdArticulo = dv.IdArticulo;");
        }

    }
}
