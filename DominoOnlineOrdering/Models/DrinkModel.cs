using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoOnlineOrdering.Models
{
    public class DrinkModel : ItemBaseModel
    {
        public List<DrinkModel> Options { get; set; }
    }
}
