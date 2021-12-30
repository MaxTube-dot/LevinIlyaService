using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevinFsspParser.BL.Models
{
    /// <summary>
    /// Модель содержащая данные о уникальном id, выполняемой на сервере задачи.
    /// </summary>
    public class TaskModel : ITask
    {
        /// <summary>
        /// Уникальный id, выполняемой задачи.
        /// </summary>
        public string Id { get; set; }
    }
}
