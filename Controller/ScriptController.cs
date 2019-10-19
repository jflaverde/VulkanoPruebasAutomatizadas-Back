using Data.CRUD;
using Data.DTO;
using Data.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
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
        /// Actualiza script
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public ReturnMessage UpdateScript(ScriptDTO script)
        {
            try
            {
                //logica de negocio aquí
                ScriptBehavior scriptBehavior = new ScriptBehavior();
                return scriptBehavior.UpdateScript(script);
            }
            catch (Exception ex)
            {
                ReturnMessage mensaje = new ReturnMessage();

                mensaje.TipoMensaje = TipoMensaje.Error;
                mensaje.Mensaje = ex.Message;
                return mensaje;
            }
        }
    }
}
