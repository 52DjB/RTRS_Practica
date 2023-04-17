using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для ComputerBase.xaml
    /// </summary>
    public partial class ComputerBase : Window
    {
        public ComputerBase()
        {
            InitializeComponent();

            CmList.ItemsSource = RTRS_BaseEntities.GetContext().Computer.ToList();

            CmbOs.ItemsSource = RTRS_BaseEntities.GetContext().PC_OS.ToList();
        }
        private void Basic_Click(object sender, RoutedEventArgs e)
        {
            BasicElement basicEquip = new BasicElement((sender as Button).DataContext as Computer);
            basicEquip.ShowDialog();
        }
        private void Dob_Click(object sender, RoutedEventArgs e)
        {
            ComputerAdd addOrUp = new ComputerAdd(null);
            Close();
            addOrUp.ShowDialog();
        }
        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            ComputerAdd UpOrAdd = new ComputerAdd((sender as Button).DataContext as Computer);
            UpOrAdd.ShowDialog();
        }
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            RTRS_BaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            CmList.ItemsSource = RTRS_BaseEntities.GetContext().Computer.ToList();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var PaymentForDel = CmList.SelectedItems.Cast<Computer>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить данные? ({PaymentForDel.Count()})", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    RTRS_BaseEntities.GetContext().Computer.RemoveRange(PaymentForDel);
                    RTRS_BaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    CmList.ItemsSource = RTRS_BaseEntities.GetContext().Computer.ToList();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message.ToString());
                }
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.ShowDialog();
        }
        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cur = RTRS_BaseEntities.GetContext().Computer.ToList();
            //if(TbSearch.Text != "")
            //{
            //    CmList.ItemsSource = RTRS_BaseEntities.GetContext().Computer.Where(z => z.OPerson.ToString().Contains(TbSearch.Text.ToLower())).ToList();
            //}
            if (TbSearch.Text != "")
            {
                CmList.ItemsSource = RTRS_BaseEntities.GetContext().Computer.Where(z => z.PC_Name.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
                //CmList.ItemsSource = RTRS_BaseEntities.GetContext().Computer.Where(z => z.Inventory_number.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
            }
            else
            {
                CmList.ItemsSource = RTRS_BaseEntities.GetContext().Computer.ToList();
            }
        }
        private void TbSearchP_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cur2 = RTRS_BaseEntities.GetContext().Computer.ToList();
            if (TbSearchPerson.Text != "")
            {
                CmList.ItemsSource = RTRS_BaseEntities.GetContext().Computer.Where(z => z.OPerson.ToLower().Contains(TbSearchPerson.Text.ToLower())).ToList();
            }
            else
            {
                CmList.ItemsSource = RTRS_BaseEntities.GetContext().Computer.ToList();
            }
        }

        private void CmbOs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var n = RTRS_BaseEntities.GetContext().Computer.ToList().Distinct();
            switch(CmbOs.SelectedIndex)
            {
                case 0: 
                    n = n.Where(x => x.idOS == 1); 
                    break;
                case 1:
                    n = n.Where(x => x.idOS == 2);
                    break;
                case 2:
                    n = n.Where(x => x.idOS == 3);
                    break;
                case 3:
                    n = n.Where(x => x.idOS == 4);
                    break;
                case 4:
                    n = n.Where(x => x.idOS == 5);
                    break;
                case 5:
                    n = n.Where(x => x.idOS == 6);
                    break;
                case 6:
                    n = n.Where(x => x.idOS == 7);
                    break;
                case 7:
                    n = n.Where(x => x.idOS == 8);
                    break;
                case 8:
                    n = n.Where(x => x.idOS == 9);
                    break;
                case 9:
                    n = n.Where(x => x.idOS == 10);
                    break;
                default:
                    break;
            }
            CmList.ItemsSource = n.ToList();
        }
    }
}
