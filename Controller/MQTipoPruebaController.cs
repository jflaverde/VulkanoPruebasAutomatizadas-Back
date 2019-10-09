using Data.CRUD;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
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
