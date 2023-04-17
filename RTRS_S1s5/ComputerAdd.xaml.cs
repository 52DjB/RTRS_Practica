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
    /// Логика взаимодействия для ComputerAdd.xaml
    /// </summary>
    public partial class ComputerAdd : Window
    {
        private Computer _currentComputer = new Computer();
        public ComputerAdd(Computer selectComputer)
        {
            InitializeComponent();

            CbOs.ItemsSource = RTRS_BaseEntities.GetContext().PC_OS.ToList();
            CbOrg.ItemsSource = RTRS_BaseEntities.GetContext().Organization.ToList();
            CbDomen.ItemsSource = RTRS_BaseEntities.GetContext().Domen.ToList();
            CbType.ItemsSource = RTRS_BaseEntities.GetContext().PC_Type.ToList();

            DataContext = _currentComputer;

            if (selectComputer != null)
            {
                _currentComputer = selectComputer;
            }
            DataContext = _currentComputer;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_currentComputer.id == 0)
                RTRS_BaseEntities.GetContext().Computer.Add(_currentComputer);
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
            ComputerBase computer = new ComputerBase();
                Close();
                computer.ShowDialog();
        }
    }
}
