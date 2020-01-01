using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DominoOnlineOrdering.Models
{
    public class PizzaModel : ItemBaseModel
    {
        public List<ToppingModel> Toppings { get; set; }
    }
}
