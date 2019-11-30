using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFramework.CRUD;
using DataFramework.DTO;
using DataFramework.Messages;

namespace ControllerVulkano
{
    public class WorkerController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workerID"></param>
        /// <returns></returns>
        public List<WorkerStatus> GetWorkerStatus(int workerID)
        {
            try
            {
                WorkerBehavior worker = new WorkerBehavior();
                return worker.GetWorkerStatus(workerID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        /// <returns></returns>
        public ReturnMessage UpdateWorkerStatus(WorkerStatus worker)
        {
            try
            {
                WorkerBehavior worker1 = new WorkerBehavior();
                return worker1.UpdateWorkerStatus(worker);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
