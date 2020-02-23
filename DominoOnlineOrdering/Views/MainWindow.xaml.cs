using DominoOnlineOrdering.Models;
using DominoOnlineOrdering.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using System.Windows.Input;

namespace DominoOnlineOrdering
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            Messenger.Default.Register<ItemBaseModel>(this, "Expand", ExpandColumn);
        }

        private void ExpandColumn(ItemBaseModel item)
        {
            var cdf = grc.ColumnDefinitions;
            Type t = null;
            if(item != null)
                t = item.GetType();
            if(cdf[0].Width != new GridLength(0))
            {
                cdf[0].Width = new GridLength(0);
                if(item == null)
                    cdf[3].Width = new GridLength(2.5, GridUnitType.Star);
                else if (t.Name == "PizzaModel")
                    cdf[1].Width = new GridLength(2.5, GridUnitType.Star);
                else if (t.Name == "SideModel" || t.Name == "DesertModel" || t.Name == "DrinkModel")
                    cdf[2].Width = new GridLength(2.5, GridUnitType.Star);
            }
            else
            {
                if(item == null)
                {
                    cdf[1].Width = new GridLength(0);
                    cdf[2].Width = new GridLength(0);
                    cdf[3].Width = new GridLength(2.5, GridUnitType.Star);
                }
                else if (t.Name == "PizzaModel" || t.Name == "SideModel" || t.Name == "DrinkModel" || t.Name == "DesertModel")
                {
                    if (cdf[3].Width == new GridLength(2.5, GridUnitType.Star))
                        return;
                    cdf[0].Width = new GridLength(2.5, GridUnitType.Star);
                    cdf[1].Width = new GridLength(0);
                    cdf[2].Width = new GridLength(0);
                }
            }
        }
    }
}
