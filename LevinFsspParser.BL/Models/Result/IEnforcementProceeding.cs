namespace LevinFsspParser.BL.Models
{
    /// <summary>
    /// Сведения об исполнительном производстве 
    /// </summary>
    public interface IEnforcementProceeding
    {
        string Bailiff { get; set; }
        string Department { get; set; }
        string Details { get; set; }
        string ExeProduction { get; set; }
        string IpEnd { get; set; }
        string Name { get; set; }
        string Subject { get; set; }
    }
}