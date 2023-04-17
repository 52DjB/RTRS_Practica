using System;
using System.Collections;
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
    /// Логика взаимодействия для InventoryBase.xaml
    /// </summary>
    public partial class InventoryBase : Window
    {
        public InventoryBase()
        {
            InitializeComponent();

            RTRS_BaseEntities.GetContext();
            StGrid.ItemsSource = RTRS_BaseEntities.GetContext().Stock.ToList();

            //CmbType.ItemsSource = RTRS_BaseEntities.GetContext().StockType.ToList();

        }
        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            InventoryBaseAdd UpOrAdd = new InventoryBaseAdd((sender as Button).DataContext as Stock);
            Close();
            UpOrAdd.ShowDialog();
        }
        //private void CmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var n = RTRS_BaseEntities.GetContext().Stock.ToList().Distinct();
        //    switch (CmbType.SelectedIndex)
        //    {
        //        case 0:
        //            n = n.Where(x => x.TypeStock == 1);
        //            break;
        //        case 1:
        //            n = n.Where(x => x.TypeStock == 2);
        //            break;
        //        case 2:
        //            n = n.Where(x => x.TypeStock == 3);
        //            break;
        //        case 3:
        //            n = n.Where(x => x.TypeStock == 4);
        //            break;
        //        default:
        //            break;
        //    }
        //    CmbType.ItemsSource = n.ToList();
        //}
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var PaymentForDel = StGrid.SelectedItems.Cast<Stock>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить данные? ({PaymentForDel.Count()})", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    RTRS_BaseEntities.GetContext().Stock.RemoveRange(PaymentForDel);
                    RTRS_BaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    StGrid.ItemsSource = RTRS_BaseEntities.GetContext().Stock.ToList();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message.ToString());
                }
            }
        }
        private void Dob_Click(object sender, RoutedEventArgs e)
        {
            InventoryBaseAdd addOrUp = new InventoryBaseAdd(null);
            Close();
            addOrUp.ShowDialog();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.ShowDialog();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cur = RTRS_BaseEntities.GetContext().Stock.ToList();

            if (TbSearch.Text != "")
            {
                StGrid.ItemsSource = RTRS_BaseEntities.GetContext().Stock.Where(z => z.Inventory_number.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
            }
            else
            {
                StGrid.ItemsSource = RTRS_BaseEntities.GetContext().Stock.ToList();
            }
        }
    }
}

