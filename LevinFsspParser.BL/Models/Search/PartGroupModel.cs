namespace LevinFsspParser.BL.Models
{
    /// <summary>
    /// Модель одного физического лица в групповом запросе.
    /// </summary>
    public class PartGroupModel
    {
        /// <summary>
        /// Тип лица для поиска. Физическое - 1, юридическое - 2, по номеру протокола -3. 
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        public int Type { get { return Model.Type; } }

        
        /// <summary>
        /// Модель физического лица для поиска.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("params")]
        public ISearcheModel Model { get; set; }
    }
}
