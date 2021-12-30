using LevinFsspParser.BL.Models;
using System.Collections.Generic;

namespace LevinFsspParser.BL
{
    internal interface IWorkerWithAPI
    {
        string Token { get; set; }

        IEnumerable<IResult> GetResult(ITask task);
        int GetStatus(ITask task);
        ITask SearchGroup(IGroup group);
        ITask SearchPhysical(IPerson person);
    }
}