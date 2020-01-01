using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoOnlineOrdering.Models
{
    public class OrderedItemModel : INotifyPropertyChanged
    {
        #region Fields
        private double _totalPrice;
        #endregion

        #region Properties
        public ItemBaseModel Item { get; set; }
        public int QTY { get; set; }              
        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                this.RaisePropertyChanged("TotalPrice");
            }
        }
        #endregion

        #region Constructor
        public OrderedItemModel()
        {
            Item = new ItemBaseModel();
        }
        #endregion

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
