﻿using DataFramework.Messages;
using DataFramework.CRUD;
using DataFramework.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerVulkano
{
    public class EstrategiaController
    {
        /// <summary>
        /// Crea una estrategia
        /// </summary>
        /// <param name="estrategia"></param>
        /// <returns></returns>
        public ReturnMessage CreateEstrategia(EstrategiaDTO estrategia)
        {
            try
            {
                //logica de negocio aquí
                EstrategiaBehavior estrategiaBehavior = new EstrategiaBehavior();
                return estrategiaBehavior.CreateEstrategia(estrategia);
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
        /// Status estrategia
        /// </summary>
        /// <param name="estrategiaID"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetEstrategiaStatus(int estrategiaID)
        {
            try
            {
                //logica de negocio aquí
                EstrategiaBehavior estrategiaBehavior = new EstrategiaBehavior();
                return estrategiaBehavior.GetEstrategiaStatus(estrategiaID);
            }
            catch (Exception ex)
            {
                return new Dictionary<int, string>();
            }
            

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="estrategia"></param>
        /// <returns></returns>
        public ReturnMessage UpdateEstrategia(EstrategiaDTO estrategia)
        {
            try
            {
                //logica de negocio aquí
                EstrategiaBehavior estrategiaBehavior = new EstrategiaBehavior();
                return estrategiaBehavior.UpdateEstrategia(estrategia);
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
        /// 
        /// </summary>
        /// <param name="estrategiaID"></param>
        /// <returns></returns>
        public List<EstrategiaDTO> SelectEstrategia(int estrategiaID)
        {
            try
            {
                //logica de negocio aquí
                EstrategiaBehavior estrategiaBehavior = new EstrategiaBehavior();
                return estrategiaBehavior.SelectEstrategia(estrategiaID);
            }
            catch (Exception ex)
            {
                return new List<EstrategiaDTO>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="estrategiaID"></param>
        /// <returns></returns>
        public ReturnMessage DeleteEstrategia(int estrategiaID)
        {
            try
            {
                //logica de negocio aquí
                EstrategiaBehavior estrategiaBehavior = new EstrategiaBehavior();
                return estrategiaBehavior.DeleteEstrategia(estrategiaID);
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
        /// 
        /// </summary>
        /// <param name="estrategia"></param>
        /// <returns></returns>
        public ReturnMessage AddTipoPrueba(EstrategiaDTO estrategia)
        {
            try
            {
                //logica de negocio aquí
                EstrategiaBehavior estrategiaBehavior = new EstrategiaBehavior();
                return estrategiaBehavior.AddTipoPrueba(estrategia);
            }
            catch (Exception ex)
            {
                ReturnMessage message = new ReturnMessage();
                message.TipoMensaje = TipoMensaje.Error;
                message.Mensaje = ex.Message;
                return message;
            }
        }
    }
}
