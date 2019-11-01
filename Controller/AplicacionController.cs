using Data.Messages;
using Data.CRUD;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class AplicacionController
    {
        /// <summary>
        /// Crea una estrategia
        /// </summary>
        /// <param name="aplicacion"></param>
        /// <returns></returns>
        public ReturnMessage CreateAplicacion(AplicacionDTO aplicacion)
        {
            try
            {
                //logica de negocio aquí
                AplicacionBehavior aplicacionBehavior = new AplicacionBehavior();
                return aplicacionBehavior.CreateAplicacion(aplicacion);
            }
            catch (Exception ex)
            {
                ReturnMessage message = new ReturnMessage();
                message.TipoMensaje = TipoMensaje.Error;
                message.Mensaje = ex.Message;
                return message;
            }
        }

        public ReturnMessage UpdateAplicacion(AplicacionDTO aplicacion)
        {
            try
            {
                //logica de negocio aquí
                AplicacionBehavior aplicacionBehavior = new AplicacionBehavior();
                return aplicacionBehavior.UpdateAplicacion(aplicacion);
            }
            catch (Exception ex)
            {
                ReturnMessage message = new ReturnMessage();
                message.TipoMensaje = TipoMensaje.Error;
                message.Mensaje = ex.Message;
                return message;
            }
        }

        public ReturnMessage DeleteAplicacion(int aplicacion_id)
        {
            try
            {
                //logica de negocio aquí
                AplicacionBehavior aplicacionBehavior = new AplicacionBehavior();
                return aplicacionBehavior.DeleteAplicacion(aplicacion_id);
            }
            catch (Exception ex)
            {
                ReturnMessage message = new ReturnMessage();
                message.TipoMensaje = TipoMensaje.Error;
                message.Mensaje = ex.Message;
                return message;
            }
        }

      
        public ReturnMessage SelectAplicacion(int aplicacion_id)
        {
            ReturnMessage message = new ReturnMessage();
            try
            {
                //logica de negocio aquí
                AplicacionBehavior aplicacionBehavior = new AplicacionBehavior();

                message.obj= aplicacionBehavior.SelectAplicacion(aplicacion_id);
                message.TipoMensaje = TipoMensaje.Correcto;
                return message;
            }
            catch (Exception ex)
            {
                message.TipoMensaje = TipoMensaje.Error;
                message.Mensaje = ex.Message;
                return message;
            }
        }
    }
}
