using System.Collections.Generic;

namespace DominoOnlineOrdering.Models
{
    public class PizzaModel : ItemBaseModel
    {
        public List<ToppingModel> Toppings { get; set; }
    }
}
