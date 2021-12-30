using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevinFsspParser.BL.Models
{
    /// <summary>
    /// Модель описывающая физическое лицо, необходимое для единичного запроса.
    /// </summary>
    public class PhysicalPersonModel : ISearcheModel, IPerson
    {
        [Newtonsoft.Json.JsonProperty("firstname")]
        public string FirstName { get; set; }

        [Newtonsoft.Json.JsonProperty("lastname")]
        public string LastName { get; set; }

        [Newtonsoft.Json.JsonProperty("secondname")]
        public string SecondName { get; set; }

        [Newtonsoft.Json.JsonProperty("region")]
        public int Region { get; set; }

        [Newtonsoft.Json.JsonProperty("birthdate")]
        public string BirthDate { get; set; }


        [Newtonsoft.Json.JsonIgnore]
        int ISearcheModel.Type { get; } = 1;//Физическое лицо 

    }
}
