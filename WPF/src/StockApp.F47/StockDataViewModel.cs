using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Threading;

namespace StockApp
{
    public class StockDataViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource cvs = new CollectionViewSource();
        private ObservableCollection<StockData> col = new ObservableCollection<StockData>();
        private StockData _detail;
        private Random _random;
        private int _minValue = 0;
        private int _maxValue = 0;
        private decimal _currentValue = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        private void GetData()
        {
            var stockData = new StockData()
            {
                TimeStamp = DateTime.Now,
                Price = Convert.ToDecimal(_random.Next(_minValue * 100, _maxValue * 100)) / 100
            };
            if (_currentValue == 0)
            {
                _currentValue = stockData.Price;
            }
            stockData.Color = (stockData.Price - _currentValue > 0) ? "2" : (stockData.Price - _currentValue < 0) ? "1" : "0";
            _currentValue = stockData.Price;
            col.Add(stockData);
            cvs.Source = col;
            cvs.View.CurrentChanged += (sender, e) => Detail = stockData;
        }

        public StockDataViewModel(int min, int max)
        {
            _minValue = min;
            _maxValue = max;
            _random = new System.Random();
            _currentValue = 0;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (sender, e) => GetData();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StockData Detail 
        { 
            get => this._detail; 
            set 
            { 
                this._detail = value; OnPropertyChanged("stockData"); 
            } 
        }

        public ICollectionView View
        {
            get
            {
                if (cvs.Source == null) GetData();
                return cvs.View;
            }
        }
    }

    public class Stock
    {
        public string Name { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }

    public class StockData
    {
        public DateTime TimeStamp { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
    }
}
