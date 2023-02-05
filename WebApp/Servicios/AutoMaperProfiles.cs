using AutoMapper;
using WebApp.Models;

namespace WebApp.Servicios
{
    public class AutoMaperProfiles:Profile
    {
        public AutoMaperProfiles()
        {
            CreateMap<Articulo, ArticuloCreacionViewModel>();
        }   
    }
}
