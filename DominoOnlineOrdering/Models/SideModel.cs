using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoOnlineOrdering.Models
{
    public class SideModel : ItemBaseModel
    {
        public List<SideModel> Options { get; set; }
    }
}
