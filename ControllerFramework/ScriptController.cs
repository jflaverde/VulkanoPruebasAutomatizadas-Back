using DataFramework.CRUD;
using DataFramework.DTO;
using DataFramework.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerVulkano
{
    public class ScriptController
    {
        public ReturnMessage AddScript(ScriptDTO script)
        {
            try
            {
                //logica de negocio aquí
                ScriptBehavior scriptBehavior = new ScriptBehavior();
                return scriptBehavior.AddScript(script);
            }
            catch (Exception ex)
            {
                ReturnMessage mensaje = new ReturnMessage();
                mensaje.TipoMensaje = TipoMensaje.Error;
                mensaje.Mensaje = ex.Message;
                return mensaje;
            }
        }

        /// <summary>
        /// Selecciona un script dado un tipo de prueba
        /// </summary>
        /// <param name="tipoPrueba_id"></param>
        /// <returns></returns>
        public ScriptDTO SelectScript(int tipoPrueba_id)
        {
            try
            {
                //logica de negocio aquí
                ScriptBehavior scriptBehavior = new ScriptBehavior();
                return scriptBehavior.SelectScript(tipoPrueba_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReturnMessage UpdateScript(ScriptDTO script)
        {
            ReturnMessage returnMessage = new ReturnMessage();
            try
            {
                ScriptBehavior scriptBehavior = new ScriptBehavior();
                return scriptBehavior.UpdateScript(script);
            }
            catch (Exception ex)
            {
                returnMessage.Mensaje = ex.Message;
                returnMessage.TipoMensaje = TipoMensaje.Error;
                throw;
            }
        }
    }
}
