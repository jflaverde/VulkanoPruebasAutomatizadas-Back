using System;
using System.Collections.Generic;
using System.Text;
using Data.Messages;
using Data.CRUD;
using Data.DTO;

namespace Controller
{
    public class AplicacionController
    {
        public List<AplicacionDTO> SelectAplicacion(int aplicacion_id)
        {
            try
            {
                //logica de negocio aquí
                AplicacionBehavior aplicacionBehavior = new AplicacionBehavior();
                return aplicacionBehavior.SelectAplicacion(aplicacion_id);
            }
            catch (Exception ex)
            {
                return new List<AplicacionDTO>();
            }
        }
    }
}
