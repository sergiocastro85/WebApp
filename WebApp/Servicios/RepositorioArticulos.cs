﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Servicios
{
    public interface IRepositorioArticulos
    {
        Task Actualizar(ArticuloCreacionViewModel articulo);
        Task Borrar(int IdArticulo);
        Task<IEnumerable<Articulo>> Buscar();
        Task Crear(Articulo articulo);
        Task<Articulo> ObtenerPorId(int Id);
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

        public async Task<IEnumerable<Articulo>> Buscar()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Articulo>(@"SELECT ar.IdArticulo,
                                                                   cg.Nombre AS Categoria,
                                                                   ar.Nombre,
                                                                   ar.PrecioVenta
                                                            FROM dbo.Categrias cg
                                                                INNER JOIN dbo.Articulos ar
                                                                    ON ar.IdCategoria = cg.IdCategoria
                                                            ORDER BY cg.IdCategoria;");
        }

        //metodo que nos va dar la imfarcación de la cuenta a editar
        public async Task<Articulo> ObtenerPorId(int IdArticulo)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Articulo>(@"SELECT ar.IdArticulo,
                                                                        ar.Nombre,ar.Marca,ar.PrecioVenta
                                                                        FROM dbo.Categrias cg
                                                                            INNER JOIN dbo.Articulos ar
                                                                                ON ar.IdCategoria = cg.IdCategoria
                                                                         WHERE ar.IdArticulo=@IdArticulo;", new { IdArticulo });
            //cg.IdCategoria,
        }

        public async Task Actualizar(ArticuloCreacionViewModel articulo)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE dbo.Articulos
                                            SET Nombre = @Nombre,
                                                Marca = @Marca,
                                                PrecioVenta = @PrecioVenta,
                                                dtmVigencia = @Dtmvigencia
                                            WHERE IdArticulo = @IdArticulo;", articulo);

        }

        //metodo para eliminar cuenta
        public async Task Borrar(int IdArticulo)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM Articulos
                                            WHERE IdArticulo=@IdArticulo", new { IdArticulo });

        }
    }
}
