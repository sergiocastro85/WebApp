﻿using System.Reflection.Metadata.Ecma335;

namespace WebApp.Servicios
{

    public interface IServicioUsuarios
    {
        int ObtenerUsuarioId();
    }

    public class ServicioUsuarios:IServicioUsuarios
    {
        public int ObtenerUsuarioId()
        {
            return 1;
        }

        
    }
}
