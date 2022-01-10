using LevinFsspParser.BL.Models;
using System.Collections.Generic;

namespace LevinFsspParser.BL
{
    public interface IFsspManager
    {
        int WaitingTimeMiliiSecond { get; set; }
        int WaitMilliSecoundBeetwinRequest { get; set; }

        IEnumerable<IResult> SearchGroup(IGroup group);
        IEnumerable<IEnforcementProceeding> SearchPerson(string firstName, string lastName, string birthDate, int region, string secoundName);
    }
}