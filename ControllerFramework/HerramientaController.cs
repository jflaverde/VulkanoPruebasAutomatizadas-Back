using DataFramework.Messages;
using DataFramework.CRUD;
using DataFramework.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerVulkano
{
    public class HerramientaController
    {
        /// <summary>
        /// Crea una estrategia
        /// </summary>
        /// <param name="herramienta"></param>
        /// <returns></returns>
        public ReturnMessage CreateHerramienta(HerramientaDTO herramienta)
        {
            try
            {
                //logica de negocio aquí
                HerramientaBehavior herramientaBehavior = new HerramientaBehavior();
                return herramientaBehavior.CreateHerramienta(herramienta);
            }
            catch (Exception ex)
            {
                ReturnMessage message = new ReturnMessage();
                message.TipoMensaje = TipoMensaje.Error;
                message.Mensaje = ex.Message;
                return message;
            }
        }

        public ReturnMessage UpdateHerramienta(HerramientaDTO herramienta)
        {
            try
            {
                //logica de negocio aquí
                HerramientaBehavior herramientaBehavior = new HerramientaBehavior();
                return herramientaBehavior.UpdateHerramienta(herramienta);
            }
            catch (Exception ex)
            {
                ReturnMessage message = new ReturnMessage();
                message.TipoMensaje = TipoMensaje.Error;
                message.Mensaje = ex.Message;
                return message;
            }
        }

        public ReturnMessage DeleteHerramienta(int herramienta_id)
        {
            try
            {
                //logica de negocio aquí
                HerramientaBehavior herramientaBehavior = new HerramientaBehavior();
                return herramientaBehavior.DeleteHerramienta(herramienta_id);
            }
            catch (Exception ex)
            {
                ReturnMessage message = new ReturnMessage();
                message.TipoMensaje = TipoMensaje.Error;
                message.Mensaje = ex.Message;
                return message;
            }
        }

        public List<HerramientaDTO> SelectHerramienta(int herramienta_id)
        {
            try
            {
                //logica de negocio aquí
                HerramientaBehavior herramientaBehavior = new HerramientaBehavior();
                return herramientaBehavior.SelectHerramienta(herramienta_id);
            }
            catch (Exception ex)
            {
                return new List<HerramientaDTO>();
            }
        }
    }
}
