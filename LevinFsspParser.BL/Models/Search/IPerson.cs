namespace LevinFsspParser.BL.Models
{
    public interface IPerson
    {
        string BirthDate { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int Region { get; set; }
        string SecondName { get; set; }
    }
}