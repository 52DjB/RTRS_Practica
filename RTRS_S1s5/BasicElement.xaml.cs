using RTRS_S1s5;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Xml.Linq;

namespace RTRS_S1s5
{
    /// <summary>
    /// Логика взаимодействия для BasicElement.xaml
    /// </summary>
    public partial class BasicElement : Window
    {
        private Computer _comp = new Computer();
        public BasicElement(Computer selectComputer)
        {
            InitializeComponent();
            RTRS_BaseEntities.GetContext();

            if (selectComputer != null)
            {
                _comp = selectComputer;
            }
            DataContext = _comp;

            RTRS_BaseEntities context = RTRS_BaseEntities.GetContext();
            CbVC.ItemsSource = RTRS_BaseEntities.GetContext().VC_Label.ToList();

            var motherProducers = RTRS_BaseEntities.GetContext().Motherboard_Table
            .Where(m => m.ComputerID == _comp.id)
            .Select(m => m.Mother_Producer)
            .Distinct()
            .ToList();

            if (motherProducers.Count == 1)
            {
                var motherProducerTextBox = new TextBox()
                {
                    Text = motherProducers[0],
                    Name = "TxMotherProducer",
                    MinWidth = 200,
                    Margin = new Thickness(0)
                };
                SP_Mather2.Children.Add(motherProducerTextBox);
            }
            else if (motherProducers.Count == 0)
            {
                var motherProducerTextBox = new TextBox()
                {
                    Text = " ",
                    Name = "TxMotherProducer",
                    MinWidth = 200,
                    Margin = new Thickness(0)
                };
                SP_Mather2.Children.Add(motherProducerTextBox);
            }

            var motherModels = RTRS_BaseEntities.GetContext().Motherboard_Table
                .Where(m => m.ComputerID == _comp.id)
                .OrderBy(m => m.ComputerID)
                .ThenBy(m => m.id)
                .Select(m => m.Mother_Model)
                .ToList();

            if (motherModels.Count == 1)
            {
                var motherModelTextBox = new TextBox()
                {
                    Text = motherModels[0],
                    Name = "TxMotherModel",
                    MinWidth = 200,
                    Margin = new Thickness(0)
                };
                SP_Mather.Children.Add(motherModelTextBox);
            }
            else if (motherModels.Count == 0)
            {
                var motherModelTextBox = new TextBox()
                {
                    Text = " ",
                    Name = "TxMotherModel",
                    MinWidth = 200,
                    Margin = new Thickness(0)
                };
                SP_Mather.Children.Add(motherModelTextBox);
            }

            //===========================================

            var nicModels = RTRS_BaseEntities.GetContext().NIC_Table
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.NIC_Model)
                .ToList();

            if (nicModels.Count > 1)
            {
                var groupedNicModels = nicModels.GroupBy(n => n);
                var orderedNicModels = new List<string>();

                foreach (var group in groupedNicModels)
                {
                    orderedNicModels.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedNicModels.Count; i++)
                {
                    var nicModelTextBox = new TextBox()
                    {
                        Text = orderedNicModels[i],
                        Name = "TxNic" + (SP_Nic.Children.Count + 1).ToString(),
                        MinWidth = 200,
                        Margin = new Thickness(0)
                    };
                    SP_Nic.Children.Add(nicModelTextBox);
                }
            }
            else if (nicModels.Count == 1)
            {
                var nicModelTextBox = new TextBox()
                {
                    Text = nicModels[0],
                    Name = "TxNic1",
                    MinWidth = 200,
                    Margin = new Thickness(0)
                };
                SP_Nic.Children.Add(nicModelTextBox);
            }
            //===========================================
            var BinModels = RTRS_BaseEntities.GetContext().NIC_Table
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.Built_in)
                .ToList();

            if (BinModels.Count > 1)
            {
                var groupedBinModels = BinModels.GroupBy(n => n);
                var orderedBinModels = new List<string>();

                foreach (var group in groupedBinModels)
                {
                    orderedBinModels.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedBinModels.Count; i++)
                {
                    var BinModelTextBox = new TextBox()
                    {
                        Text = orderedBinModels[i],
                        Name = "TxBI" + (SP_Bul.Children.Count + 1).ToString(),
                        MinWidth = 200,
                        Margin = new Thickness(0)
                    };
                    SP_Bul.Children.Add(BinModelTextBox);
                }
            }
            else if (BinModels.Count == 1)
            {
                var BinModelTextBox = new TextBox()
                {
                    Text = BinModels[0],
                    Name = "TxBI1",
                    MinWidth = 200,
                    Margin = new Thickness(0)
                };
                SP_Bul.Children.Add(BinModelTextBox);
            }
            //================================================

            var CPUModels = RTRS_BaseEntities.GetContext().CPU_Table
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.CPU_Type)
                .ToList();

            if (CPUModels.Count > 1)
            {
                var groupedCPUModels = CPUModels.GroupBy(n => n);
                var orderedCPUModels = new List<string>();

                foreach (var group in groupedCPUModels)
                {
                    orderedCPUModels.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedCPUModels.Count; i++)
                {
                    var CPUModelTextBox = new TextBox()
                    {
                        Text = orderedCPUModels[i],
                        Name = "TxCPU" + (SP_Type.Children.Count + 1).ToString(),
                        MinWidth = 200,
                        Margin = new Thickness(0)
                    };
                    SP_Type.Children.Add(CPUModelTextBox);
                }
            }
            else if (CPUModels.Count == 1)
            {
                var CPUModelTextBox = new TextBox()
                {
                    Text = CPUModels[0],
                    Name = "TxCPU",
                    MinWidth = 200,
                    Margin = new Thickness(0)
                };
                SP_Type.Children.Add(CPUModelTextBox);
            }
            //======================================================
            var CPUProc = RTRS_BaseEntities.GetContext().CPU_Table
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.Num_Proc)
                .ToList();

            if (CPUProc.Count > 1)
            {
                var groupedCPUProc = CPUProc.GroupBy(n => n);
                var orderedCPUProc = new List<string>();

                foreach (var group in groupedCPUProc)
                {
                    orderedCPUProc.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedCPUProc.Count; i++)
                {
                    var CPUProcTextBox = new TextBox()
                    {
                        Text = orderedCPUProc[i],
                        Name = "TxProc" + (SP_Cor.Children.Count + 1).ToString(),
                        MinWidth = 50,
                        Margin = new Thickness(0)
                    };
                    SP_Cor.Children.Add(CPUProcTextBox);
                }
            }
            else if (CPUProc.Count == 1)
            {
                var CPUPrcoTextBox = new TextBox()
                {
                    Text = CPUProc[0],
                    Name = "TxProc",
                    MinWidth = 50,
                    Margin = new Thickness(0)
                };
                SP_Cor.Children.Add(CPUPrcoTextBox);
            }
            //==================================================


            var CPUMHz = RTRS_BaseEntities.GetContext().CPU_Table
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.MHz)
                .ToList();

            if (CPUMHz.Count > 1)
            {
                var groupedCPUMHz = CPUMHz.GroupBy(n => n);
                var orderedCPUMHz = new List<string>();

                foreach (var group in groupedCPUMHz)
                {
                    orderedCPUMHz.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedCPUMHz.Count; i++)
                {
                    var CPUMHzTextBox = new TextBox()
                    {
                        Text = orderedCPUMHz[i] + "MHz",
                        Name = "TxMHz" + (SP_MHz.Children.Count + 1).ToString(),
                        MinWidth = 50,
                        Margin = new Thickness(0)
                    };
                    SP_MHz.Children.Add(CPUMHzTextBox);
                }
            }
            else if (CPUMHz.Count == 1)
            {
                var CPUMHzTextBox = new TextBox()
                {
                    Text = CPUMHz[0],
                    Name = "TxMHz",
                    MinWidth = 50,
                    Margin = new Thickness(0)
                };
                SP_MHz.Children.Add(CPUMHzTextBox);
            }
            //================================================

            var RAMMBytes = RTRS_BaseEntities.GetContext().RAM_Table
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.RAM_Memory_MBytes)
                .ToList();

            if (RAMMBytes.Count > 1)
            {
                var groupedRAMMBytes = RAMMBytes.GroupBy(n => n);
                var orderedRAMMBytes = new List<string>();

                foreach (var group in groupedRAMMBytes)
                {
                    orderedRAMMBytes.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedRAMMBytes.Count; i++)
                {
                    var RAMMBytesTextBox = new TextBox()
                    {
                        Text = orderedRAMMBytes[i],
                        Name = "TxMBytes" + (SP_Mb.Children.Count + 1).ToString(),
                        MinWidth = 200,
                        Margin = new Thickness(0)
                    };
                    SP_Mb.Children.Add(RAMMBytesTextBox);
                }
            }
            else if (RAMMBytes.Count == 1)
            {
                var RUMMBytesTextBox = new TextBox()
                {
                    Text = RAMMBytes[0],
                    Name = "TxMBytes",
                    MinWidth = 200,
                    Margin = new Thickness(0)
                };
                SP_Mb.Children.Add(RUMMBytesTextBox);
            }
            //=====================================
            var RAMFreq = RTRS_BaseEntities.GetContext().RAM_Table
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.RAM_Frequency)
                .ToList();

            if (RAMMBytes.Count > 1)
            {
                var groupedRAMFreq = RAMFreq.GroupBy(n => n);
                var orderedRAMFreq = new List<string>();

                foreach (var group in groupedRAMFreq)
                {
                    orderedRAMFreq.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedRAMFreq.Count; i++)
                {
                    var RAMFreqTextBox = new TextBox()
                    {
                        Text = orderedRAMFreq[i],
                        Name = "TxFreq" + (SP_Freq.Children.Count + 1).ToString(),
                        MinWidth = 50,
                        Margin = new Thickness(0)
                    };
                    SP_Freq.Children.Add(RAMFreqTextBox);
                }
            }
            else if (RAMFreq.Count == 1)
            {
                var RAMFreqTextBox = new TextBox()
                {
                    Text = RAMFreq[0],
                    Name = "TxFreq",
                    MinWidth = 50,
                    Margin = new Thickness(0)
                };
                SP_Freq.Children.Add(RAMFreqTextBox);
            }
            //===============================================
            var RAMAmount = RTRS_BaseEntities.GetContext().RAM_Table
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.RAM_Amount)
                .ToList();

            if (RAMAmount.Count > 1)
            {
                var groupedRAMAmount = RAMAmount.GroupBy(n => n);
                var orderedRAMAmount = new List<string>();

                foreach (var group in groupedRAMAmount)
                {
                    orderedRAMAmount.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedRAMAmount.Count; i++)
                {
                    var RAMAmountTextBox = new TextBox()
                    {
                        Text = orderedRAMAmount[i],
                        Name = "TxAmount" + (SP_Amount.Children.Count + 1).ToString(),
                        MinWidth = 50,
                        Margin = new Thickness(0)
                    };
                    SP_Amount.Children.Add(RAMAmountTextBox);
                }
            }
            else if (RAMAmount.Count == 1)
            {
                var RAMAmountTextBox = new TextBox()
                {
                    Text = RAMAmount[0],
                    Name = "TxAmount",
                    MinWidth = 50,
                    Margin = new Thickness(0)
                };
                SP_Amount.Children.Add(RAMAmountTextBox);
            }
            //======================================
            var DSName = RTRS_BaseEntities.GetContext().DataStoreTable
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.DSName)
                .ToList();

            if (DSName.Count > 1)
            {
                var groupedDSName = DSName.GroupBy(n => n);
                var orderedDSName = new List<string>();

                foreach (var group in groupedDSName)
                {
                    orderedDSName.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedDSName.Count; i++)
                {
                    var DSNameTextBox = new TextBox()
                    {
                        Text = orderedDSName[i],
                        Name = "TxDName" + (SP_DName.Children.Count + 1).ToString(),
                        MinWidth = 200,
                        Margin = new Thickness(0)
                    };
                    SP_DName.Children.Add(DSNameTextBox);
                }
            }
            else if (DSName.Count == 1)
            {
                var DSNameTextBox = new TextBox()
                {
                    Text = DSName[0],
                    Name = "TxDName",
                    MinWidth = 200,
                    Margin = new Thickness(0)
                };
                SP_DName.Children.Add(DSNameTextBox);
            }
            //===============================================
            var DSMemory = RTRS_BaseEntities.GetContext().DataStoreTable
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.Memory)
                .ToList();

            if (DSMemory.Count > 1)
            {
                var groupedDSMemory = DSMemory.GroupBy(n => n);
                var orderedDSMemory = new List<string>();

                foreach (var group in groupedDSMemory)
                {
                    orderedDSMemory.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedDSMemory.Count; i++)
                {
                    var DSMemoryTextBox = new TextBox()
                    {
                        Text = orderedDSMemory[i],
                        Name = "TxMemory" + (SP_DMemory.Children.Count + 1).ToString(),
                        MinWidth = 50,
                        Margin = new Thickness(0)
                    };
                    SP_DMemory.Children.Add(DSMemoryTextBox);
                }
            }
            else if (DSMemory.Count == 1)
            {
                var DSMemoryTextBox = new TextBox()
                {
                    Text = DSMemory[0],
                    Name = "TxMemory",
                    MinWidth = 50,
                    Margin = new Thickness(0)
                };
                SP_DMemory.Children.Add(DSMemoryTextBox);
            }
            //===============================================
            var DSType = RTRS_BaseEntities.GetContext().DataStoreTable
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.Type)
                .ToList();

            if (DSType.Count > 1)
            {
                var groupedDSType = DSType.GroupBy(n => n);
                var orderedDSType = new List<string>();

                foreach (var group in groupedDSType)
                {
                    orderedDSType.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedDSType.Count; i++)
                {
                    var DSTypeComboBox = new TextBox()
                    {
                        Text = orderedDSType[i],
                        Name = "TxDSType" + (SP_DType.Children.Count + 1).ToString(),
                        MinWidth = 50,
                        Margin = new Thickness(0)
                    };
                    SP_DType.Children.Add(DSTypeComboBox);
                }
            }
            else if (DSType.Count == 1)
            {
                var DSTypeComboBox = new TextBox()
                {
                    Text = DSType[0],
                    Name = "TxDSType",
                    MinWidth = 50,
                    Margin = new Thickness(0)
                };
                SP_DType.Children.Add(DSTypeComboBox);
            }
        }
        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            BasicElementAdd UpOrAdd = new BasicElementAdd((sender as Button).DataContext as Computer);
            Close();
            UpOrAdd.ShowDialog();
        }
    }
}

//InitializeComponent();
//RTRS_BaseEntities.GetContext();

//if (selectComputer != null)
//{
//    _comp = selectComputer;
//}
//DataContext = _comp;

//RTRS_BaseEntities context = RTRS_BaseEntities.GetContext();
//TxBI.ItemsSource = RTRS_BaseEntities.GetContext().BuiltIn_Table.ToList();