namespace DominoOnlineOrdering.Models
{
    public class ItemBaseModel
    {
        public string Name { get; set; }
        public string SmallImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Energy { get; set; }
        public string ItemType { get; set; }
    }
}
