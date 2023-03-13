using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Servicios
{


    public interface IRepositorioStock
    {
        Task<IEnumerable<StockProductos>> ObtenerStock();
    }

    public class RepositorioStockProducto : IRepositorioStock
    {

        private readonly string connectionString;

        public RepositorioStockProducto (IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<StockProductos>> ObtenerStock()
        {
            using var connection=new SqlConnection(connectionString);

            return await connection.QueryAsync<StockProductos>(@"SELECT art.IdArticulo, art.Nombre,ast.Cantidad FROM dbo.ArticuloStock ast
                                                                INNER JOIN dbo.Articulos art
                                                                ON art.IdArticulo = ast.IdArticulo;");

        }

    }

     








}
