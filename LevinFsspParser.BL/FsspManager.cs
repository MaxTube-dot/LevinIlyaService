using LevinFsspParser.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LevinFsspParser.BL
{
    public class FsspManager : IFsspManager
    {
        /// <summary>
        /// Ограничение кол-во запросов с стороны API
        /// </summary>
        public LimitRequest _limitRequest = new LimitRequest();

        /// <summary>
        /// Время ожидания ответа на запрос.
        /// </summary>
        public int WaitingTimeMiliiSecond { get; set; } = 300000;

        /// <summary>
        /// Время между запросами на выполненность задачи.
        /// </summary>

        public int WaitMilliSecoundBeetwinRequest { get; set; } = 5000;


        private IWorkerWithAPI fsspManager;

        /// <summary>
        /// ТОкен доступа к API
        /// </summary>
        /// <param name="token">Токен</param>
        public FsspManager(string token)
        {
            fsspManager = new WorkerWithAPI(token);
        }


        /// <summary>
        /// Выполнение поиска в БД ФССП физического лица.
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="birthDate">Дата рождения в формате dd.mm.YYYY</param>
        /// <param name="region">Номер региона. https://api-ip.fssp.gov.ru/KOD_region.csv?v=2</param>
        /// <param name="secoundName">Отчество</param>
        /// <returns></returns>
        public IEnumerable<IEnforcementProceeding> SearchPerson(string firstName, string lastName, string birthDate, int region, string secoundName)
        {
            _limitRequest.AddPhysicalRequest();

            IPerson person = new PhysicalPersonModel() { FirstName = firstName, SecondName = secoundName, LastName = lastName, BirthDate = birthDate, Region = region };

            ITask task = fsspManager.SearchPhysical(person);


            List<IEnforcementProceeding> enforcementProceedings = new List<IEnforcementProceeding>();

            var results = GetResults(task).ToArray();


            enforcementProceedings.AddRange(results[0].EnforcementProceedings);


            return enforcementProceedings.ToArray();

        }

        /// <summary>
        /// Выполнение группового запроса 
        /// </summary>
        /// <param name="group">Лица на которых производится групповой запрос.</param>
        /// <returns> Содержит информацию о исполнительном производстве на запрашиваемых лиц.</returns>
        public IEnumerable<IResult> SearchGroup(IGroup group)
        {

            _limitRequest.AddGroupRequest();

            ITask task = fsspManager.SearchGroup(group);

            var results = GetResults(task);

            return results;

        }

        /// <summary>
        /// Получает содержащий фические лица и информацию о их исполнительных производствах
        /// </summary>
        /// <param name="task">Идентификатор задачи выполняемой на сервере.</param>
        /// <returns>Сведения об исполнительных производствах в отношении физических лиц.</returns>
        private IEnumerable<IResult> GetResults(ITask task)
        {
            int CountRequest = WaitingTimeMiliiSecond / WaitMilliSecoundBeetwinRequest;

            Thread.Sleep(1000);

            for (int i = 0; i < CountRequest; i++)
            {
                var statusCode = fsspManager.GetStatus(task);

                if (statusCode == 0 || statusCode == 3)
                {
                    Thread.Sleep(100);

                    var results = fsspManager.GetResult(task);
                    return results;

                }

                Thread.Sleep(WaitMilliSecoundBeetwinRequest);
            }

            throw new FsspExeption("Задача не выполнена за предоставленный период времени.", 0);
        }
    }

    public class FsspExeption:Exception
    {
        public int Code { get; set; }

        public FsspExeption(string message,int code ):base(message)
        {
            Code = code;
        }

        public FsspExeption(string message) : base(message)
        {
           
        }

    }
}
