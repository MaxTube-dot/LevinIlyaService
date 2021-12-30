namespace LevinFsspParser.BL.Models
{
    public interface IGroup
    {
        /// <summary>
        /// Массив физических лиц на котрорых производится запрос.
        /// </summary>
        PartGroupModel[] GroupsModel { get; set; }

        /// <summary>
        /// Добавить физичесское лицо в поиск
        /// </summary>
        /// <param name="model">Физическое лицо</param>
        void AddPerson (ISearcheModel model);
    }
}