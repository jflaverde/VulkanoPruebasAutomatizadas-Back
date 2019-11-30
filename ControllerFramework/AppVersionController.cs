using DataFramework.Messages;
using Data.CRUD;
using DataFramework.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerVulkano
{
    public class AppVersionController
    {
        /// <summary>
        /// Crea una version de aplicacion
        /// </summary>
        /// <param name="appVersion"></param>
        /// <returns></returns>
        public ReturnMessage CreateAppVersion(AppVersionDTO appVersion)
        {
            try
            {
                //logica de negocio aquí
                AppVersionBehavior appVersionBehavior = new AppVersionBehavior();
                return appVersionBehavior.CreateAppVersion(appVersion);
            }
            catch (Exception ex)
            {
                ReturnMessage message = new ReturnMessage();
                message.TipoMensaje = TipoMensaje.Error;
                message.Mensaje = ex.Message;
                return message;
            }
        }
 
        public ReturnMessage SelectAppVersion(int appversion_id)
        {
            ReturnMessage message = new ReturnMessage();
            try
            {
                //logica de negocio aquí
                AppVersionBehavior appVersionBehavior = new AppVersionBehavior();

                message.obj= appVersionBehavior.SelectAppVersion(appversion_id);
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
