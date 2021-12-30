using System;
using System.Collections.Generic;

namespace LevinFsspParser.BL.Models
{
    /// <summary>
    /// Объект содержащий конечный результат запроса, по одному физическому лицу. 
    /// </summary>
    public class ResultModel : IResult
    {

        public enum ResultTypes
        {
            Physical = 1,
            Legal = 2,
            Ip = 3
        }

        public IPerson Person { get; set; }


        public DateTime TaskStart { get; set; }


        public DateTime TaskEnd { get; set; }

        public ResultTypes ResultType { get; set; }

        public IEnforcementProceeding[] EnforcementProceedings { get; set; }

        public TimeSpan GetLeadTime()
        {
            var diff = TaskEnd.Subtract(TaskStart);

            return diff;
        }

    }
}
