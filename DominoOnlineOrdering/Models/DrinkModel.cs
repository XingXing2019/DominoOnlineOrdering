using System.Collections.Generic;

namespace DominoOnlineOrdering.Models
{
    public class DrinkModel : ItemBaseModel
    {
        public List<DrinkModel> Options { get; set; }
    }
}
