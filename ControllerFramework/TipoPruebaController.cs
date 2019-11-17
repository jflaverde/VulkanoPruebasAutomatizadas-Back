using Data.Messages;
using Data.CRUD;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class TipoPruebaController
    {
        public ReturnMessage ListPruebas(int estrategia_id, int tipoprueba_id)
        {
            try
            {
                //logica de negocio aquí
                TipoPruebaBehavior tipoPruebaBehavior = new TipoPruebaBehavior();
                return tipoPruebaBehavior.ListPruebas(estrategia_id, tipoprueba_id);
            }
            catch (Exception ex)
            {
                ReturnMessage message = new ReturnMessage();
                message.TipoMensaje = TipoMensaje.Error;
                message.Mensaje = ex.Message;
                return message;
            }
        }

        /// <summary>
        /// inserta el historial del resultadod de la prueba
        /// </summary>
        /// <param name="estrategia_id"></param>
        /// <param name="prueba_id"></param>
        /// <param name="ejecucion_id"></param>
        /// <param name="ruta_resultados"></param>
        /// <returns></returns>
        public int InsertEjecucionTipoPrueba(int estrategia_id, int prueba_id, int ejecucion_id, string ruta_resultados)
        {
            try
            {
                //logica de negocio aquí
                TipoPruebaBehavior tipoPruebaBehavior = new TipoPruebaBehavior();
                return tipoPruebaBehavior.InsertEjecucionTipoPrueba(estrategia_id, prueba_id,ejecucion_id,ruta_resultados);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
