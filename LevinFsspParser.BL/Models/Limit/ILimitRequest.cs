using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevinFsspParser.BL.Models
{
    public interface ILimitRequest
    {
        bool CanGroupRequest
        {
            get
            {
                if (CountGroupRequestDay > 0 )
                {
                    return true;
                }
                return false;
            }
        }

        bool CanSingleRequest 
        { get 
            {
                if (CountPhysicalRequestDay>0 && CountPhysicalRequestHour>0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Колличество доступных групповых заросов в сутки
        /// </summary>
        int CountGroupRequestDay { get;  }

        /// <summary>
        /// Колличество доступных одиночных заросов в сутки
        /// </summary>
        int CountPhysicalRequestDay { get;  }




        /// <summary>
        /// Колличество доступных одиночных заросов в час 
        /// </summary>
        int CountPhysicalRequestHour { get; }

        /// <summary>
        /// Выполнен новый групповой запрос
        /// </summary>
         void AddGroupRequest();

        /// <summary>
        /// Выполнен новый одиночный запрос
        /// </summary>
         void AddPhysicalRequest();
    }
}
