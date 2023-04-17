using RTRS_S1s5;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RTRS_S1s5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComputerBase computerBase = new ComputerBase();
            Close();
            computerBase.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InventoryBase inventoryBase = new InventoryBase();
            Close();
            inventoryBase.ShowDialog();
        }
    }
}

//Context.cs

//public static RTRS_BaseEntities _context;

//public RTRS_BaseEntities()
//            : base("name=RTRS_BaseEntities")
//        {
//}
//public static RTRS_BaseEntities GetContext()
//{
//    if (_context == null)
//    {
//        _context = new RTRS_BaseEntities();
//    }
//    return _context;
//}