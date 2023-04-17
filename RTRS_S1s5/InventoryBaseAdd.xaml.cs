using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RTRS_S1s5
{
    /// <summary>
    /// Логика взаимодействия для InventoryBaseAdd.xaml
    /// </summary>
    public partial class InventoryBaseAdd : Window
    {
        private Stock _currentStock = new Stock();
        public InventoryBaseAdd(Stock selectStock)
        {
            InitializeComponent();

            DataContext = _currentStock;

            if (selectStock != null)
            {
                _currentStock = selectStock;
            }
            DataContext = _currentStock;
            CbStock.ItemsSource = RTRS_BaseEntities.GetContext().StockType.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_currentStock.idStock == 0)
                RTRS_BaseEntities.GetContext().Stock.Add(_currentStock);

            // получаем выбранный элемент в списке CbStock
            var selectedStockType = CbStock.SelectedItem as StockType;

            // присваиваем выбранный элемент текущему экземпляру класса Stock
            _currentStock.StockType = selectedStockType;

            try
            {
                RTRS_BaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString());
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            InventoryBase inventoryBase = new InventoryBase();
            Close();
            inventoryBase.ShowDialog();
        }
    }
}
