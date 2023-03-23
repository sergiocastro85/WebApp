using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Servicios
{

    public interface IRepositorioUsuarios
    {
        Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado);
        Task<int> CrearUsuario(Usuario usuario);
    }


    public class RepositorioUsuarios:IRepositorioUsuarios
    {

        private readonly string connectionString;

        public RepositorioUsuarios(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Crear metos de Crear Usuario

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            //usuario.EmailNormalizado = usuario.Email.ToUpper();
            
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO dbo.Usuarios 
                                                        (Tipo,password,Email,EmailNormalizado)
                                                        VALUES (@Tipo,@Password,@Email,@EmailNormalizado);
                                                        SELECT SCOPE_IDENTITY();", usuario);

            return id;
        }

        public async Task<Usuario>BuscarUsuarioPorEmail(string EmailNormalizado)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QuerySingleOrDefaultAsync<Usuario>(@"SELECT *
                                                                FROM dbo.Usuarios
                                                                WHERE EmailNormalizado = @EmailNormalizado;", new {EmailNormalizado});
        }

    }
}
