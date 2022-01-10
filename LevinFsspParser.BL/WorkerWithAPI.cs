using System;
using System.Net;
using LevinFsspParser.BL.Models;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Generic;

namespace LevinFsspParser.BL
{

    /// <summary>
    /// Класс реализующий бизнес логику взаимодействия с API.
    /// </summary>
   internal  class WorkerWithAPI : IWorkerWithAPI
    {

        public string Token { get; set; } 

        public string _host = "https://api-ip.fssp.gov.ru/api/v1.0";

        private WebAccessRealization _webAccess = new WebAccessRealization();

        public WorkerWithAPI(string token)
        {
            Token = token;
        }

        /// <summary>
        /// Инициализация поиска данных в БД ФССП для  физического лица.
        /// </summary>
        /// <param name="person">Физическое лицо, по данным которого мы ищем записи в БД ФССП.</param>
        /// <returns>Объект TaskModel, содержащий уникальный номер задачи находящейся в обработке. </returns>
        public ITask SearchPhysical(IPerson person)
        {
            if (string.IsNullOrEmpty(person.FirstName))
                throw new ArgumentException("firstName пуст.");


            if (string.IsNullOrEmpty(person.LastName))
                throw new ArgumentException("lastName пуст.");

            if (person.Region>99 && person.Region < 1)
                throw new ArgumentException("region имеет неправильный код.");

            if (!DateTime.TryParse(person.BirthDate, out DateTime birthDate))
                throw new ArgumentException("birthDate имеет неправильный формат.");


            string basePath = "/search/physical";
            string fullPath = MakeApiUrl(basePath);


            #region Parameters
            var queryParameters = new NameValueCollection();

            queryParameters.Add("token", Token);
            queryParameters.Add("region", person.Region.ToString());
            queryParameters.Add("firstname", person.FirstName);

            if (person.SecondName != null)
            {
                queryParameters.Add("secondname", person.SecondName);
            }

            queryParameters.Add("lastname", person.LastName);
            queryParameters.Add("birthdate", person.BirthDate);
            #endregion


            string response = _webAccess.Get(fullPath, queryParameters);

            JObject respJson = JObject.Parse(response);

            string taskId = respJson["response"]["task"].ToString();


            return new TaskModel() { Id = taskId };
        }

        /// <summary>
        /// Проверка статуса задачи находящейся в обработке
        /// </summary>
        /// <param name="task">Объект TaskModel, содержащий уникальный номер задачи находящейся в обработке.</param>
        /// <returns>Кодовое обозначение статуса задачи. 0 - выполнена,1 - выполняется, 2 - в очереди, 3 - выполнена,но частично с ошибками </returns>
        public int GetStatus(ITask task)
        {
            string path = "/status";
            string fullPath = MakeApiUrl(path);



            var queryParameters = new NameValueCollection();

            queryParameters.Add("token", Token);
            queryParameters.Add("task", task.Id);



            string response = _webAccess.Get(fullPath, queryParameters);

            JObject respJson = JObject.Parse(response);

            int statusCode = int.Parse(respJson["response"]["status"].ToString());



            return statusCode;
        }

        /// <summary>
        /// Получение результата запроса.
        /// </summary>
        /// <param name="task">Объект TaskModel, содержащий уникальный номер задачи находящейся в обработке.</param>
        /// <returns>Массиы ResultModel[] содержащий записи из БД ФССП</returns>
        public IEnumerable<IResult> GetResult(ITask task)
        {
            string path = "/result";
            string fullPath = MakeApiUrl(path);

            var queryParameters = new NameValueCollection();

            queryParameters.Add("token", Token);
            queryParameters.Add("task", task.Id);

            string response = _webAccess.Get(fullPath, queryParameters);
            //string response = File.ReadAllText("ResultResponseMock.json", Encoding.UTF8);

            JObject respJson = JObject.Parse(response);

            DateTime taskStart = DateTime.Parse(respJson["response"]["task_start"].ToString());
            DateTime taskEnd = DateTime.Parse(respJson["response"]["task_end"].ToString());

            JArray resultArrayNode = (JArray)respJson["response"]["result"];

            var result = resultArrayNode.Select(node => new ResultModel()
            {
                TaskStart = taskStart,
                TaskEnd = taskEnd,
                ResultType = (ResultModel.ResultTypes)((int)node["query"]["type"]),
                Person = JsonConvert.DeserializeObject<PhysicalPersonModel>(node["query"]["params"].ToString()),
                EnforcementProceedings = node["result"].Select(node2 =>
                JsonConvert.DeserializeObject<EnforcementProceedingModel>(node2.ToString())).ToArray()
            }).ToArray();



            return result;
        }

        /// <summary>
        /// Групповой запрос на получение данных из БД ФССП.
        /// </summary>
        /// <param name="group">Объект GroupModel содержащий физические лица,по которым ищется информация в БД ФССП</param>
        /// <returns></returns>
        public ITask SearchGroup(IGroup group)
        {
            string basePath = "/search/group";

            string fullPath = MakeApiUrl(basePath);


            var jsonGroup = JsonConvert.SerializeObject(group.GroupsModel);

            string data = @"{""token"": """ + Token + @""",""request"":" + jsonGroup + "}";



            string response = _webAccess.Post(fullPath, data);

            JObject respJson = JObject.Parse(response);


            string taskId = respJson["response"]["task"].ToString();



            return new TaskModel() { Id = taskId };

        }


        private string MakeApiUrl(string path)
        {
            return $"{_host}{path}";
        }


    }
}
