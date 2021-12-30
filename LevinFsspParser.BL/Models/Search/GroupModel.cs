using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevinFsspParser.BL.Models
{
    /// <summary>
    /// Класс содержащий группу физических лиц, для выполнения группового запроса.
    /// </summary>
    public class GroupModel : IGroup
    {

        private List<PartGroupModel> _groupsModel = new List<PartGroupModel>();

        public PartGroupModel[] GroupsModel
        {
            get { return _groupsModel.ToArray(); }
            set { _groupsModel = value.ToList(); }
        }

        public void AddPerson(ISearcheModel model)
        {
            _groupsModel.Add(new PartGroupModel() { Model = model });
        }
    }




}
