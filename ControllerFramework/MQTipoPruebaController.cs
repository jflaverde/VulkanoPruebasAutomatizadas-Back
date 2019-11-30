using DataFramework.CRUD;
using DataFramework.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerVulkano
{
    public class MQTipoPruebaController
    {
        public List<MQTipoPruebaDTO> SelectMQTipoPrueba(int tipoPruebaID)
        {
            try
            {
                //logica de negocio aquí
                MQTipoPruebaBehavior mqTipoPruebaBehavior = new MQTipoPruebaBehavior();
                return mqTipoPruebaBehavior.SelectMQTipoPrueba(tipoPruebaID);
            }
            catch (Exception ex)
            {
                return new List<MQTipoPruebaDTO>();
            }
        }
    }
}
