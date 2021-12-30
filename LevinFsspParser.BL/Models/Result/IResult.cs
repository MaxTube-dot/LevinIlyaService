using System;

namespace LevinFsspParser.BL.Models
{
    /// <summary>
    /// Содержит информацию о исполнителном прозводстве, и лицо на которое производился поиск.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Сведения о исполнителных производствах.
        /// </summary>
        IEnforcementProceeding[] EnforcementProceedings { get; set; }

        /// <summary>
        /// На кого производился поиск.
        /// </summary>
        IPerson Person { get; set; }

        /// <summary>
        /// Тип лица на которое производился запрос, т.е. ResultModel содержит информацию о физическом, юридическом лице или о постановлении.
        /// </summary>
        ResultModel.ResultTypes ResultType { get; set; }

        /// <summary>
        /// Время выполнения запроса.
        /// </summary>
        DateTime TaskEnd { get; set; }

        /// <summary>
        /// Время начала выполнения запроса
        /// </summary>
        DateTime TaskStart { get; set; }

        /// <summary>
        /// Время выполнения запроса
        /// </summary>
        /// <returns>Время выполнения запроса</returns>
        TimeSpan GetLeadTime();
    }
}