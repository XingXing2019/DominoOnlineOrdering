using DominoOnlineOrdering.Models;
using DominoOnlineOrdering.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DominoOnlineOrdering.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private const string _pizzaMenuFilePath = @"..\..\Data\PizzaMenu.xml";
        private const string _toppingMenuFilePath = @"..\..\Data\ToppingMenu.xml";
        private const string _sidesMenuFilePath = @"..\..\Data\SideMenu.xml";
        private const string _desertMenuFilePath = @"..\..\Data\DesertMenu.xml";
        private const string _drinkMenuFilePath = @"..\..\Data\DrinkMenu.xml";

        private ItemBaseModel _selectedItem;
        private double _price;
        private double _totalPrice;
        private int _qty;
        private ObservableCollection<ToppingModel> _selectedToppings;
        private ObservableCollection<OrderedItemModel> _orderedItems;
        private string _voucherCode;
        private int _voucherApplyCount = 1;
        private int _orderedItemCount;
        private List<ItemBaseModel> _recommendMenu;
        private Dictionary<string, int> _orderedItemTypes;
        private string _recommendItemType;
        private string _creditCarName;
        private string _creditCarNo;
        private int? _creditCardExpiryMonth;
        private int? _creditCardExpiryYear;
        private int? _creditCardCVV;
        #endregion

        #region Properties
        public List<PizzaModel> PizzaMenu { get; set; }
        public List<ToppingModel> ToppingMenu { get; set; }
        public List<ToppingModel> OriginalToppings { get; set; }
        public List<SideModel> SidesMenu { get; set; }
        public List<DesertModel> DesertMenu { get; set; }
        public List<DrinkModel> DrinkMenu { get; set; }
        public ItemBaseModel HawaiianPizza { get; set; }
        public ItemBaseModel ChickenMeatBall { get; set; }
        public ItemBaseModel ThickShake { get; set; }
        public ItemBaseModel CheeseGarlicScroll { get; set; }

        public ItemBaseModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                this.RaisePropertyChanged("SelectedItem");
            }
        }
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                this.RaisePropertyChanged("Price");
            }
        }
        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                this.RaisePropertyChanged("TotalPrice");
            }
        }
        public int QTY
        {
            get { return _qty; }
            set
            {
                _qty = value;
                this.RaisePropertyChanged("QTY");
            }
        }
        public ObservableCollection<ToppingModel> SelectedToppings
        {
            get { return _selectedToppings; }
            set
            {
                _selectedToppings = value;
                this.RaisePropertyChanged("SelectedToppings");
            }
        }
        public ObservableCollection<OrderedItemModel> OrderedItems
        {
            get { return _orderedItems; }
            set
            {
                _orderedItems = value;
                this.RaisePropertyChanged("OrderedItems");
            }
        }
        public string VoucherCode
        {
            get { return _voucherCode; }
            set
            {
                _voucherCode = value;
                this.RaisePropertyChanged("VoucherCode");
            }
        }
        public int VoucherApplyCount
        {
            get { return _voucherApplyCount; }
            set
            {
                _voucherApplyCount = value;
                this.RaisePropertyChanged("VoucherApplyCount");
            }
        }
        public int OrderedItemCount
        {
            get { return _orderedItemCount; }
            set
            {
                _orderedItemCount = value;
                this.RaisePropertyChanged("OrderedItemCount");
            }
        }
        public List<ItemBaseModel> RecommendMenu
        {
            get { return _recommendMenu; }
            set
            {
                _recommendMenu = value;
                this.RaisePropertyChanged("RecommendMenu");
            }
        }
        public Dictionary<string, int> OrderedItemTypes
        {
            get { return _orderedItemTypes; }
            set
            {
                _orderedItemTypes = value;
                this.RaisePropertyChanged("OrderedItemTypes");
            }
        }
        public string RecommendItemType
        {
            get { return _recommendItemType; }
            set
            {
                _recommendItemType = value;
                this.RaisePropertyChanged("RecommendItemType");
            }
        }
        public string CreditCarName
        {
            get { return _creditCarName; }
            set
            {
                _creditCarName = value;
                this.RaisePropertyChanged("CreditCarName");
            }
        }
        public string CreditCarNo
        {
            get { return _creditCarNo; }
            set
            {
                _creditCarNo = value;
                this.RaisePropertyChanged("CreditCarNo");
            }
        }
        public int? CreditCardExpiryMonth
        {
            get { return _creditCardExpiryMonth; }
            set
            {
                _creditCardExpiryMonth = value;
                this.RaisePropertyChanged("CreditCardExpiryMonth");
            }
        }
        public int? CreditCardExpiryYear
        {
            get { return _creditCardExpiryYear; }
            set
            {
                _creditCardExpiryYear = value;
                this.RaisePropertyChanged("CreditCardExpiryYear");
            }
        }
        public int? CreditCardCVV
        {
            get { return _creditCardCVV; }
            set
            {
                _creditCardCVV = value;
                this.RaisePropertyChanged("CreditCardCVV");
            }
        }
        #endregion

        #region Commands
        private RelayCommand<ItemBaseModel> _selectItemCommand;
        public RelayCommand<ItemBaseModel> SelectItemCommand
        {
            get { if (_selectItemCommand == null) _selectItemCommand = new RelayCommand<ItemBaseModel>(i => SelectItemCommandExecutor(i)); return _selectItemCommand; }
            set { _selectItemCommand = value; }
        }


        private RelayCommand<MainWindow> _homePageCommand;
        public RelayCommand<MainWindow> HomePageCommand
        {
            get { if (_homePageCommand == null) _homePageCommand = new RelayCommand<MainWindow>(w => HomePageCommandExecutor(w)); return _homePageCommand; }
            set { _homePageCommand = value; }
        }


        private RelayCommand<MainWindow> _windowMinCommand;
        public RelayCommand<MainWindow> WindowMinCommand
        {
            get { if (_windowMinCommand == null) _windowMinCommand = new RelayCommand<MainWindow>(w => WindowMinCommandExecutor(w)); return _windowMinCommand; }
            set { _windowMinCommand = value; }
        }


        private RelayCommand<MainWindow> _windowMaxCommand;
        public RelayCommand<MainWindow> WindowMaxCommand
        {
            get { if (_windowMaxCommand == null) _windowMaxCommand = new RelayCommand<MainWindow>(w => WindowMaxCommandExecutor(w)); return _windowMaxCommand; }
            set { _windowMaxCommand = value; }
        }


        private RelayCommand _windowCloseCommand;
        public RelayCommand WindowCloseCommand
        {
            get { if (_windowCloseCommand == null) _windowCloseCommand = new RelayCommand(WindowCloseCommandExecutor); return _windowCloseCommand; }
            set { _windowCloseCommand = value; }
        }


        private RelayCommand<ToppingModel> _selectToppingCommand;
        public RelayCommand<ToppingModel> SelectToppingCommand
        {
            get { if (_selectToppingCommand == null) _selectToppingCommand = new RelayCommand<ToppingModel>(t => SelectToppingCommandExecutor(t)); return _selectToppingCommand; }
            set { _selectToppingCommand = value; }
        }


        private RelayCommand<ToppingModel> _removeToppingCommand;
        public RelayCommand<ToppingModel> RemoveToppingCommand
        {
            get { if (_removeToppingCommand == null) _removeToppingCommand = new RelayCommand<ToppingModel>(t => RemoveToppingCommandExecutor(t)); return _removeToppingCommand; }
            set { _removeToppingCommand = value; }
        }


        private RelayCommand<ItemBaseModel> _addOrderCommand;
        public RelayCommand<ItemBaseModel> AddOrderCommand
        {
            get { if (_addOrderCommand == null) _addOrderCommand = new RelayCommand<ItemBaseModel>(i => AddOrderCommandExecutor(i)); return _addOrderCommand; }
            set { _addOrderCommand = value; }
        }


        private RelayCommand<OrderedItemModel> _removeItemCommand;
        public RelayCommand<OrderedItemModel> RemoveItemCommand
        {
            get { if (_removeItemCommand == null) _removeItemCommand = new RelayCommand<OrderedItemModel>(o => RemoveItemCommandExecutor(o)); return _removeItemCommand; }
            set { _removeItemCommand = value; }
        }


        private RelayCommand<string> _applyVoucherCommand;
        public RelayCommand<string> ApplyVoucherCommand
        {
            get
            {
                if (_applyVoucherCommand == null)
                    _applyVoucherCommand = new RelayCommand<string>(v => ApplyVoucherCommandExecutor(v), v => CanApplyVoucherCommandExecute(v));
                this.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "VoucherCode")
                        _applyVoucherCommand.RaiseCanExecuteChanged();
                    if (e.PropertyName == "VoucherApplyCount")
                        _applyVoucherCommand.RaiseCanExecuteChanged();
                    if (e.PropertyName == "TotalPrice")
                        _applyVoucherCommand.RaiseCanExecuteChanged();
                };
                return _applyVoucherCommand;
            }
        }


        private RelayCommand<ItemBaseModel> _finishAndPayCommand;
        public RelayCommand<ItemBaseModel> FinishAndPayCommand
        {
            get
            {
                if (_finishAndPayCommand == null)
                    _finishAndPayCommand = new RelayCommand<ItemBaseModel>(i => FinishAndPayCommandExecutor(i), i => CanFinishAndPayCommandExecute(i));
                this.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "OrderedItemCount")
                        _finishAndPayCommand.RaiseCanExecuteChanged();
                };
                return _finishAndPayCommand;
            }
            set { _finishAndPayCommand = value; }
        }


        private RelayCommand<MainWindow> _placeOrderCommand;
        public RelayCommand<MainWindow> PlaceOrderCommand
        {
            get { if (_placeOrderCommand == null) _placeOrderCommand = new RelayCommand<MainWindow>(w => PlaceOrderCommandExecutor(w)); return _placeOrderCommand; }
            set { _placeOrderCommand = value; }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            PizzaMenu = XmlDataAccessService.LoadItems<PizzaModel>(_pizzaMenuFilePath);
            ToppingMenu = XmlDataAccessService.LoadItems<ToppingModel>(_toppingMenuFilePath);
            SidesMenu = XmlDataAccessService.LoadItems<SideModel>(_sidesMenuFilePath);
            DesertMenu = XmlDataAccessService.LoadItems<DesertModel>(_desertMenuFilePath);
            DrinkMenu = XmlDataAccessService.LoadItems<DrinkModel>(_drinkMenuFilePath);
            SelectedToppings = new ObservableCollection<ToppingModel>();
            OrderedItems = new ObservableCollection<OrderedItemModel>();
            OrderedItemTypes = new Dictionary<string, int>() { { "PizzaModel", 0 }, { "SideModel", 0 }, { "DrinkModel", 0 }, { "DesertModel", 0 } };
            HawaiianPizza = PizzaMenu[3];
            ChickenMeatBall = SidesMenu[6];
            ThickShake = DrinkMenu[0];
            CheeseGarlicScroll = SidesMenu[3];
        }
        #endregion

        #region CommandExecutors
        private void SelectItemCommandExecutor(ItemBaseModel item)
        {
            SelectedToppings.Clear();
            if (item != null)
            {
                SelectedItem = item;
                Price = item.Price;
                if (item.GetType().Name == "PizzaModel")
                {
                    OriginalToppings = PizzaMenu.First(p => p.Name == item.Name).Toppings;
                    foreach (var t in OriginalToppings)
                    {
                        SelectedToppings.Add(t);
                    }
                }
            }
            QTY = 1;
            Messenger.Default.Send(item, "Expand");
        }
        private void HomePageCommandExecutor(MainWindow window)
        {
            var cdf = window.grc.ColumnDefinitions;
            cdf[0].Width = new GridLength(2.5, GridUnitType.Star);
            cdf[1].Width = new GridLength(0);
            cdf[2].Width = new GridLength(0);
            cdf[3].Width = new GridLength(0);
            var rdf = window.payColumn.RowDefinitions;
            rdf[0].Height = new GridLength(110);
            rdf[1].Height = new GridLength(130);
            rdf[2].Height = new GridLength(0);
            rdf[3].Height = new GridLength(1, GridUnitType.Auto);

            CreditCarName = "";
            CreditCarNo = "";
            CreditCardExpiryMonth = null;
            CreditCardExpiryYear = null;
            CreditCardCVV = null;
        }
        private void WindowMinCommandExecutor(MainWindow window)
        {
            window.WindowState = WindowState.Minimized;
        }
        private void WindowMaxCommandExecutor(MainWindow window)
        {
            if (window.WindowState == WindowState.Normal)
                window.WindowState = WindowState.Maximized;
            else
                window.WindowState = WindowState.Normal;
        }
        private void WindowCloseCommandExecutor()
        {
            Environment.Exit(Environment.ExitCode);
        }
        private void SelectToppingCommandExecutor(ToppingModel topping)
        {
            if (SelectedToppings.Count >= 7)
                return;
            if (SelectedToppings.Any(t => t.Name == topping.Name))
                return;
            SelectedToppings.Add(topping);
            if (!OriginalToppings.Any(t => t.Name == topping.Name))
                Price += topping.Price;
        }
        private void RemoveToppingCommandExecutor(ToppingModel topping)
        {
            SelectedToppings.Remove(topping);
            if (!OriginalToppings.Any(t => t.Name == topping.Name))
                Price -= topping.Price;
        }
        private void AddOrderCommandExecutor(ItemBaseModel item)
        {
            double newToppingPrice = 0;
            if (item.GetType().Name == "PizzaModel")
            {
                var newToppings = SelectedToppings.Where(st => !OriginalToppings.Any(ot => ot.Name == st.Name)).ToList();
                newToppingPrice = newToppings.Sum(t => t.Price);
                item.Price += newToppingPrice;
            }
            SelectedToppings.Clear();
            var orderedItem = new OrderedItemModel();
            orderedItem.Item.ItemType = item.GetType().Name;
            orderedItem.Item.Name = item.Name;
            orderedItem.Item.Energy = item.Energy;
            orderedItem.TotalPrice = item.Price * QTY;
            orderedItem.QTY = QTY;
            if (VoucherApplyCount != 1)
                orderedItem.TotalPrice *= 0.9;
            TotalPrice += orderedItem.TotalPrice;
            OrderedItems.Add(orderedItem);
            OrderedItemCount = OrderedItems.Count();
            OrderedItemTypes[item.GetType().Name]++;
            item.Price -= newToppingPrice;
            Messenger.Default.Send(item, "Expand");
        }
        private void RemoveItemCommandExecutor(OrderedItemModel orderedItem)
        {
            OrderedItems.Remove(orderedItem);
            OrderedItemCount = OrderedItems.Count();
            OrderedItemTypes[orderedItem.Item.ItemType]--;
            TotalPrice -= orderedItem.TotalPrice;
            TotalPrice = Math.Round(TotalPrice, 2);
        }
        private bool CanApplyVoucherCommandExecute(string voucherCode)
        {
            return !string.IsNullOrEmpty(voucherCode) && VoucherApplyCount == 1 && TotalPrice != 0;
        }
        private void ApplyVoucherCommandExecutor(string voucherCode)
        {
            TotalPrice *= 0.9;
            foreach (var i in OrderedItems)
                i.TotalPrice *= 0.9;
            VoucherApplyCount++;
            VoucherCode = "";
        }
        private bool CanFinishAndPayCommandExecute(ItemBaseModel item)
        {
            return OrderedItemCount != 0;
        }
        private void FinishAndPayCommandExecutor(ItemBaseModel item)
        {
            RecommendMenu = GenerateRecommendMenu();
            Messenger.Default.Send(item, "Expand");
        }
        private List<ItemBaseModel> GenerateRecommendMenu()
        {
            List<ItemBaseModel> recommendMenu = new List<ItemBaseModel>();
            int min = OrderedItemTypes.Min(t => t.Value);
            var menu = OrderedItemTypes.First(t => t.Value == min).Key;
            RecommendItemType = (menu.Substring(0, menu.Length - 5) + "S").ToUpper();
            var r = new Random();
            List<int> index = new List<int>();
            while (index.Count() < 2)
            {
                int i = r.Next(0, 8);
                if (!index.Contains(i))
                    index.Add(i);
            }
            if (menu == "PizzaModel")
                foreach (var i in index)
                    recommendMenu.Add(PizzaMenu[i]);
            else if (menu == "SideModel")
                foreach (var i in index)
                    recommendMenu.Add(SidesMenu[i].Options[r.Next(0, SidesMenu[i].Options.Count)]);
            else if (menu == "DrinkModel")
                foreach (var i in index)
                    recommendMenu.Add(DrinkMenu[i].Options[r.Next(0, DrinkMenu[i].Options.Count)]);
            else
                foreach (var i in index)
                    recommendMenu.Add(DesertMenu[i].Options[r.Next(0, DesertMenu[i].Options.Count)]);
            return recommendMenu;
        }
        private void PlaceOrderCommandExecutor(MainWindow window)
        {
            var rdf = window.payColumn.RowDefinitions;
            rdf[0].Height = new GridLength(0);
            rdf[1].Height = new GridLength(0);
            rdf[3].Height = new GridLength(0);
            rdf[2].Height = new GridLength(1, GridUnitType.Star);
            TotalPrice = 0;
            OrderedItems.Clear();
            OrderedItemCount = OrderedItems.Count();
            OrderedItemTypes = new Dictionary<string, int>() { { "PizzaModel", 0 }, { "SideModel", 0 }, { "DrinkModel", 0 }, { "DesertModel", 0 } };
            VoucherApplyCount = 1;
        }
        #endregion
    }
}