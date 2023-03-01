using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Servicios
{

    public interface IRepositorioDetalleVentas
    {
        Task Crear(DetalleVenta detalleVenta);
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
                                                    new { detalleVenta.IdArticulo, detalleVenta.Cantidad, detalleVenta.Precio, detalleVenta.DtmFecha },
                                                    commandType: System.Data.CommandType.StoredProcedure);


            detalleVenta.IdArticulo = id;

        }
    }
}
