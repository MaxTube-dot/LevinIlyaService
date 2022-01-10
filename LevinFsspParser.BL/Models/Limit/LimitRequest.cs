using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevinFsspParser.BL.Models
{
    public class LimitRequest : ILimitRequest
    {
        private List<Request> Requests = new List<Request>();


        private int MaxCountGroupRequestDay = 5000;
        public int CountGroupRequestDay
        {
            get
            {
                return MaxCountGroupRequestDay - Requests
                    .Where(x => x.Type == Request.TypeRequest.Group)
                    .Where(x => x.Date > DateTime.Now.AddHours(-24))
                    .Count();
            }
        }



        private int MaxCountPhysicalRequestDay = 1000;
        public int CountPhysicalRequestDay
        {
            get
            {
                return MaxCountPhysicalRequestDay - Requests
                    .Where(x => x.Type == Request.TypeRequest.Single)
                    .Where(x => x.Date > DateTime.Now.AddHours(-24))
                    .Count();
            }
        }


        private int MaxCountPhysicalRequestHour = 100;
        public int CountPhysicalRequestHour
        {
            get
            {
                return MaxCountPhysicalRequestHour - Requests
                    .Where(x => x.Type == Request.TypeRequest.Single)
                    .Where(x => x.Date > DateTime.Now.AddHours(-1))
                    .Count();
            }
        }



        public void AddGroupRequest()
        {
            if (CountGroupRequestDay < 1)
                throw new LimitExeption("Слишком много групповых запросов в сутки!");

            Requests.Add(new Request { Type = Request.TypeRequest.Group });

            ClearRequests();
        }



         public void AddPhysicalRequest()
        {
            if (CountPhysicalRequestDay < 1)
                throw new LimitExeption("Слишком много одиночных запросов в сутки!");

            if (CountPhysicalRequestHour < 1)
                throw new LimitExeption("Слишком много одиночных запросов в час!");

            Requests.Add(new Request { Type = Request.TypeRequest.Single });

            ClearRequests();
        }

        private  void ClearRequests()
        {
            DateTime yesterday = DateTime.Now.AddHours(-24);
             Requests.RemoveAll(x => x.Date < yesterday);
        }


    }
}
