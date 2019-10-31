﻿using Data.CRUD;
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
                ReturnMessage mensaje = new ReturnMessage();

                mensaje.TipoMensaje = TipoMensaje.Error;
                mensaje.Mensaje = ex.Message;
                return mensaje;
            }
        }
    }
}
