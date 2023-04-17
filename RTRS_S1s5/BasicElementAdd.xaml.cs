using RTRS_S1s5;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
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
    public partial class BasicElementAdd : Window
    {
        private Computer _comp = new Computer();
        private CPU_Table _cpu = new CPU_Table();
        private RAM_Table _ram = new RAM_Table();
        private NIC_Table _nic = new NIC_Table();
        private DataStoreTable _datastore = new DataStoreTable();

        RTRS_BaseEntities context = RTRS_BaseEntities.GetContext();

        public BasicElementAdd(Computer selectComputer)
        {
            InitializeComponent();
            RTRS_BaseEntities.GetContext();

            if (selectComputer != null)
            {
                _comp = selectComputer;
            }
            DataContext = _comp;

            RTRS_BaseEntities context = RTRS_BaseEntities.GetContext();
            SP_VCLabel.ItemsSource = RTRS_BaseEntities.GetContext().VC_Label.ToList();
            //CbType.ItemsSource = RTRS_BaseEntities.GetContext().DataStoreType.ToList();

            //===========================================
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
                    Margin = new Thickness(10)
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
                    Margin = new Thickness(10)
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
                        Name = "TxType" + (SP_Type.Children.Count + 1).ToString(),
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
                    Name = "TxBI1",
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
                        Width = 20,
                        Margin = new Thickness(0)
                    };
                    SP_Cor.Children.Add(CPUProcTextBox);
                    var CPUProcText = new TextBlock()
                    {
                        Text = "Шт.",
                        Name = "Шт" + (SP_Ht.Children.Count + 1).ToString(),
                        Width = 30,
                        Margin = new Thickness(0)
                    };
                    SP_Ht.Children.Add(CPUProcText);
                }
            }
            else if (CPUProc.Count == 1)
            {
                var CPUPrcoTextBox = new TextBox()
                {
                    Text = CPUProc[0],
                    Name = "TxProc",
                    Width = 20,
                    Margin = new Thickness(0)
                };
                SP_Cor.Children.Add(CPUPrcoTextBox);
                var CPUProcText = new TextBlock()
                {
                    Text = "Шт.",
                    Name = "Шт" + (SP_Ht.Children.Count + 1).ToString(),
                    Width = 30,
                    Margin = new Thickness(0)
                };
                SP_Ht.Children.Add(CPUProcText);
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
                        Text = orderedCPUMHz[i],
                        Name = "TxMHz" + (SP_MHz.Children.Count + 1).ToString(),
                        MinWidth = 20,
                        Margin = new Thickness(0)
                    };
                    SP_MHz.Children.Add(CPUMHzTextBox);
                    var CPUMhzText = new TextBlock()
                    {
                        Text = "MHz.",
                        Name = "Mhz" + (SP_Mhz.Children.Count + 1).ToString(),
                        Width = 30,
                        Margin = new Thickness(0)
                    };
                    SP_Mhz.Children.Add(CPUMhzText);
                }
            }
            else if (CPUMHz.Count == 1)
            {
                var CPUMHzTextBox = new TextBox()
                {
                    Text = CPUMHz[0],
                    Name = "TxMHz",
                    MinWidth = 20,
                    Margin = new Thickness(0)
                };
                SP_MHz.Children.Add(CPUMHzTextBox);
                var CPUMhzText = new TextBlock()
                {
                    Text = "MHz",
                    Name = "Mhz" + (SP_Mhz.Children.Count + 1).ToString(),
                    Width = 30,
                    Margin = new Thickness(0)
                };
                SP_Mhz.Children.Add(CPUMhzText);
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


            //=======================================================
            var VCModel = RTRS_BaseEntities.GetContext().VideoCardTable
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.VCModel)
                .ToList();

            if (VCModel.Count == 1)
            {
                var groupedVCModel = VCModel.GroupBy(n => n);
                var orderedVCModel = new List<string>();

                foreach (var group in groupedVCModel)
                {
                    orderedVCModel.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedVCModel.Count; i++)
                {
                    var VCModelTextBox = new TextBox()
                    {
                        Text = orderedVCModel[i],
                        Name = "VCModel" + (SP_VC.Children.Count + 1).ToString(),
                        MinWidth = 200,
                        Margin = new Thickness(0)
                    };
                    SP_VC.Children.Add(VCModelTextBox);
                }
            }
            else if (VCModel.Count == 0)
            {
                var VCModelTextBox = new TextBox()
                {
                    Text = " ",
                    Name = "VCModel",
                    MinWidth = 200,
                    Margin = new Thickness(10)
                };
                SP_VC.Children.Add(VCModelTextBox);
            }
            //===============================================
            var VCMemory = RTRS_BaseEntities.GetContext().VideoCardTable
                .Where(n => n.ComputerID == _comp.id)
                .OrderBy(n => n.ComputerID)
                .ThenBy(n => n.id)
                .Select(n => n.VCMemory)
                .ToList();

            if (VCMemory.Count == 1)
            {
                var groupedVCMemory = VCMemory.GroupBy(n => n);
                var orderedVCMemory = new List<string>();

                foreach (var group in groupedVCMemory)
                {
                    orderedVCMemory.AddRange(group);
                }

                // Add a new TextBox for each NIC_Model to the StPan stack panel
                for (int i = 0; i < orderedVCMemory.Count; i++)
                {
                    var VCMemoryTextBox = new TextBox()
                    {
                        Text = orderedVCMemory[i],
                        Name = "VCMemory" + (SP_Mem.Children.Count + 1).ToString(),
                        MinWidth = 50,
                        Margin = new Thickness(0)
                    };
                    SP_Mem.Children.Add(VCMemoryTextBox);
                }
            }
            else if (VCModel.Count == 0)
            {
                var VCModelTextBox = new TextBox()
                {
                    Text = " ",
                    Name = "VCMemory",
                    MinWidth = 200,
                    Margin = new Thickness(10)
                };
                SP_Mem.Children.Add(VCModelTextBox);
            }
            var VCLabelList = RTRS_BaseEntities.GetContext().VideoCardTable
                    .Where(n => n.ComputerID == _comp.id)
                    .OrderBy(n => n.ComputerID)
                    .ThenBy(n => n.id)
                    .Select(n => n.VC_Label.VCLabe)
                    .ToList();
            if (VCLabelList.Count == 1)
            {
                var groupedVCLabel = VCLabelList.GroupBy(n => n);
                var orderedVCLabel = new List<string>();

                foreach (var group in groupedVCLabel)
                {
                    orderedVCLabel.AddRange(group);
                }

                // Add a new ComboBox for each VCLabel to the StPan stack panel
                for (int i = 0; i < orderedVCLabel.Count; i++)
                {
                    var VCLabelComboBox = new ComboBox()
                    {
                        Name = "VCLabel" + (SP_Labe.Children.Count + 1).ToString(),
                        MinWidth = 50,
                        Margin = new Thickness(0),
                        ItemsSource = orderedVCLabel,
                        SelectedItem = orderedVCLabel[i]
                    };
                    SP_Labe.Children.Add(VCLabelComboBox);
                }
            }
            else if (VCLabelList.Count == 0)
            {
                var VCLabelComboBox = new ComboBox()
                {
                    Text = " ",
                    Name = "VCLabel" + (SP_Labe.Children.Count + 1).ToString(),
                    MinWidth = 200,
                    Margin = new Thickness(10)
                };
                SP_Labe.Children.Add(VCLabelComboBox);
            }
        }
        //===============================================

        private void DobNIC_Click(object sender, RoutedEventArgs e)
        {
            var nicModelTextBox = new TextBox()
            {
                Text = "",
                Name = "TxNic" + (SP_Nic.Children.Count + 1).ToString(),
                MinWidth = 200,
                Margin = new Thickness(0),
            };
            //===========================================
            var binModelTextBox = new TextBox()
            {
                Text = "",
                Name = "TxBI" + (SP_Bul.Children.Count + 1).ToString(),
                MinWidth = 200,
                Margin = new Thickness(0)
            };
            SP_Nic.Children.Add(nicModelTextBox);
            SP_Bul.Children.Add(binModelTextBox);
        }

        private void DelNIC_Click(object sender, RoutedEventArgs e)
        {
            if (SP_Nic.Children.Count > 1)
            {
                if (SP_Nic != null)
                {
                    var lastNicTextBox = SP_Nic.Children.OfType<TextBox>().LastOrDefault();
                    var lastBinTextBox = SP_Bul.Children.OfType<TextBox>().LastOrDefault();

                    if (lastNicTextBox != null)
                    {
                        SP_Nic.Children.Remove(lastNicTextBox);
                    }
                    if (lastBinTextBox != null)
                    {
                        SP_Bul.Children.Remove(lastBinTextBox);
                    }
                }
            }
        }
        private void DobCPU_Click(object sender, RoutedEventArgs e)
        {
            var CPUModelTextBox = new TextBox()
            {
                Text = "",
                Name = "TxCPU" + (SP_Type.Children.Count + 1).ToString(),
                MinWidth = 200,
                Margin = new Thickness(0)
            };
            //===========================================
            var CPUProcTextBox = new TextBox()
            {
                Text = "",
                Name = "TxProc" + (SP_Cor.Children.Count + 1).ToString(),
                MinWidth = 50,
                Margin = new Thickness(0)
            };
            var CPUMHzTextBox = new TextBox()
            {
                Text = "",
                Name = "TxMHz" + (SP_MHz.Children.Count + 1).ToString(),
                MinWidth = 50,
                Margin = new Thickness(0)
            };
            SP_Type.Children.Add(CPUModelTextBox);
            SP_Cor.Children.Add(CPUProcTextBox);
            SP_MHz.Children.Add(CPUMHzTextBox);
        }

        private void DelCPU_Click(object sender, RoutedEventArgs e)
        {
            if (SP_Type.Children.Count > 1)
            {
                var lastNicTextBox = SP_Type.Children.OfType<TextBox>().LastOrDefault();
                var lastBinTextBox = SP_Cor.Children.OfType<TextBox>().LastOrDefault();
                var lastMHzTextBox = SP_MHz.Children.OfType<TextBox>().LastOrDefault();

                if (lastNicTextBox != null)
                {
                    SP_Type.Children.Remove(lastNicTextBox);
                }
                if (lastBinTextBox != null)
                {
                    SP_Cor.Children.Remove(lastBinTextBox);
                }
                if (lastMHzTextBox != null)
                {
                    SP_MHz.Children.Remove(lastMHzTextBox);
                }
            }
        }

        private void DobRAM_Click(object sender, RoutedEventArgs e)
        {
            var RAMMBytesTextBox = new TextBox()
            {
                Text = "",
                Name = "TxNic" + (SP_Mb.Children.Count + 1).ToString(),
                MinWidth = 200,
                Margin = new Thickness(0)
            };
            //===========================================
            var RAMFreqTextBox = new TextBox()
            {
                Text = "",
                Name = "TxBI" + (SP_Freq.Children.Count + 1).ToString(),
                MinWidth = 50,
                Margin = new Thickness(0)
            };
            var RAMAmountTextBox = new TextBox()
            {
                Text = "",
                Name = "TxBI" + (SP_Amount.Children.Count + 1).ToString(),
                MinWidth = 50,
                Margin = new Thickness(0)
            };
            SP_Mb.Children.Add(RAMMBytesTextBox);
            SP_Freq.Children.Add(RAMFreqTextBox);
            SP_Amount.Children.Add(RAMAmountTextBox);
        }

        private void DelRAM_Click(object sender, RoutedEventArgs e)
        {
            if (SP_Mb.Children.Count > 1)
            {
                var lastRAMMBytesTextBox = SP_Mb.Children.OfType<TextBox>().LastOrDefault();
                var lastFreqTextBox = SP_Freq.Children.OfType<TextBox>().LastOrDefault();
                var lastAmountTextBox = SP_Amount.Children.OfType<TextBox>().LastOrDefault();


                if (lastRAMMBytesTextBox != null)
                {
                    SP_Mb.Children.Remove(lastRAMMBytesTextBox);
                }
                if (lastFreqTextBox != null)
                {
                    SP_Freq.Children.Remove(lastFreqTextBox);
                }
                if (lastAmountTextBox != null)
                {
                    SP_Amount.Children.Remove(lastAmountTextBox);
                }
            }
        }

        private void DobDS_Click(object sender, RoutedEventArgs e)
        {
            var DSNameTextBox = new TextBox()
            {
                Text = "",
                Name = "TxNic" + (SP_DName.Children.Count + 1).ToString(),
                MinWidth = 200,
                Margin = new Thickness(0)
            };
            //===========================================
            var DSMemoryTextBox = new TextBox()
            {
                Text = "",
                Name = "TxBI" + (SP_DMemory.Children.Count + 1).ToString(),
                MinWidth = 50,
                Margin = new Thickness(0)
            };
            var DSTypeComboBox = new TextBox()
            {
                Text = "",
                Name = "TxBI" + (SP_DType.Children.Count + 1).ToString(),
                MinWidth = 50,
                Margin = new Thickness(0)
            };
            SP_DName.Children.Add(DSNameTextBox);
            SP_DMemory.Children.Add(DSMemoryTextBox);
            SP_DType.Children.Add(DSTypeComboBox);
        }

        private void DelDS_Click(object sender, RoutedEventArgs e)
        {
            if (SP_DName.Children.Count > 1)
            {
                var lastDSNameTextBox = SP_DName.Children.OfType<TextBox>().LastOrDefault();
                var lastMemoryTextBox = SP_DMemory.Children.OfType<TextBox>().LastOrDefault();
                var lastTypeTextBox = SP_DType.Children.OfType<TextBox>().LastOrDefault();

                if (lastDSNameTextBox != null)
                {
                    SP_DName.Children.Remove(lastDSNameTextBox);
                }
                if (lastMemoryTextBox != null)
                {
                    SP_DMemory.Children.Remove(lastMemoryTextBox);
                }
                if (lastTypeTextBox != null)
                {
                    SP_DType.Children.Remove(lastTypeTextBox);
                }
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Save changes made to the current computer
            if (_comp.id == 0)
                RTRS_BaseEntities.GetContext().Computer.Add(_comp);
            try
            {

                // Save changes made to NIC_Table
                var nicBinModels = new List<Tuple<string, string>>();

                var nicModels = SP_Nic.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
                var existingNicModels = RTRS_BaseEntities.GetContext().NIC_Table
                    .Where(n => n.ComputerID == _comp.id && n.NIC_Model != null)
                    .ToList();

                var BinModels = SP_Bul.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
                var existingBinModels = RTRS_BaseEntities.GetContext().NIC_Table
                    .Where(n => n.ComputerID == _comp.id && n.Built_in != null)
                    .ToList();

                // Update or add NIC_Table entries based on existing data and new data
                foreach (var nicModel in nicModels)
                {
                    var binModel = BinModels[nicModels.IndexOf(nicModel)];
                    var existingNicModel = existingNicModels.FirstOrDefault(n => n.NIC_Model == nicModel);

                    if (existingNicModel != null)
                    {
                        var existingBinModel = existingBinModels.FirstOrDefault(n => n.id == existingNicModel.id);
                        if (existingBinModel?.Built_in != binModel)
                        {
                            existingBinModel.Built_in = binModel;
                        }
                    }
                    else
                    {
                        RTRS_BaseEntities.GetContext().NIC_Table.Add(new NIC_Table
                        {
                            ComputerID = _comp.id,
                            NIC_Model = nicModel,
                            Built_in = binModel
                        });
                    }

                    nicBinModels.Add(new Tuple<string, string>(nicModel, binModel));
                }

                // Delete any NIC_Table entries that are not in the updated nicBinModels list
                foreach (var existingNicModel in existingNicModels)
                {
                    var existingBinModel = existingBinModels.FirstOrDefault(n => n.id == existingNicModel.id);
                    var binModel = existingBinModel?.Built_in;

                    if (!nicBinModels.Contains(new Tuple<string, string>(existingNicModel.NIC_Model, binModel)))
                    {
                        RTRS_BaseEntities.GetContext().NIC_Table.Remove(existingNicModel);
                    }
                }

                // Save changes made to CPU_Table
                var cpuModels = SP_Type.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
                var cpuProc = SP_Cor.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
                var cpuMHz = SP_MHz.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();

                var existingCPUModels = RTRS_BaseEntities.GetContext().CPU_Table
                    .Where(n => n.ComputerID == _comp.id && n.CPU_Type != null)
                    .ToList();

                var TyProMhzModels = new List<Tuple<string, string, string>>();

                // Update or add CPU_Table entries based on existing data and new data
                for (int i = 0; i < cpuModels.Count; i++)
                {
                    var existingCPUModel = existingCPUModels.FirstOrDefault(n => n.CPU_Type == cpuModels[i]);

                    if (existingCPUModel != null)
                    {
                        // Update existing CPU_Table entry
                        existingCPUModel.Num_Proc = cpuProc[i];
                        existingCPUModel.MHz = cpuMHz[i];
                    }
                    else
                    {
                        // Add new CPU_Table entry
                        RTRS_BaseEntities.GetContext().CPU_Table.Add(new CPU_Table
                        {
                            ComputerID = _comp.id,
                            CPU_Type = cpuModels[i],
                            Num_Proc = cpuProc[i],
                            MHz = cpuMHz[i]
                        });
                    }

                    TyProMhzModels.Add(new Tuple<string, string, string>(cpuModels[i], cpuProc[i], cpuMHz[i]));
                }

                // Delete any CPU_Table entries that are not in the updated TyProMhzModels list
                foreach (var existingCPUModel in existingCPUModels)
                {
                    if (!TyProMhzModels.Contains(new Tuple<string, string, string>(existingCPUModel.CPU_Type, existingCPUModel.Num_Proc, existingCPUModel.MHz)))
                    {
                        RTRS_BaseEntities.GetContext().CPU_Table.Remove(existingCPUModel);
                    }
                }

                // Save changes made to RAM_Table
                var ramMBytes = SP_Mb.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
                var ramFreq = SP_Freq.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
                var ramAmount = SP_Amount.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();

                var existingRamModels = RTRS_BaseEntities.GetContext().RAM_Table
                    .Where(n => n.ComputerID == _comp.id && n.RAM_Memory_MBytes != null)
                    .ToList();

                var MbFreAmModels = new List<Tuple<string, string, string>>();

                // Update or add RAM_Table entries based on existing data and new data
                for (int i = 0; i < ramMBytes.Count; i++)
                {
                    var existingRamModel = existingRamModels.FirstOrDefault(n => n.RAM_Memory_MBytes == ramMBytes[i]);

                    if (existingRamModel != null)
                    {
                        // Update existing RAM_Table entry
                        existingRamModel.RAM_Frequency = ramFreq[i];
                        existingRamModel.RAM_Amount = ramAmount[i];
                    }
                    else
                    {
                        // Add new RAM_Table entry
                        RTRS_BaseEntities.GetContext().RAM_Table.Add(new RAM_Table
                        {
                            ComputerID = _comp.id,
                            RAM_Memory_MBytes = ramMBytes[i],
                            RAM_Frequency = ramFreq[i],
                            RAM_Amount = ramAmount[i]
                        });
                    }

                    MbFreAmModels.Add(new Tuple<string, string, string>(ramMBytes[i], ramFreq[i], ramAmount[i]));
                }

                // Delete any RAM_Table entries that are not in the updated MbFreAmModels list
                foreach (var existingRamModel in existingRamModels)
                {
                    if (!MbFreAmModels.Contains(new Tuple<string, string, string>(existingRamModel.RAM_Memory_MBytes, existingRamModel.RAM_Frequency, existingRamModel.RAM_Amount)))
                    {
                        RTRS_BaseEntities.GetContext().RAM_Table.Remove(existingRamModel);
                    }
                }

                // Save changes made to DataStoreTable
                var dsNames = SP_DName.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
                var dsMemory = SP_DMemory.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
                var dsType = SP_DType.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();

                var existingDSNames = RTRS_BaseEntities.GetContext().DataStoreTable
                    .Where(n => n.ComputerID == _comp.id && n.DSName != null)
                    .ToList();

                var NameMeTyModels = new List<Tuple<string, string, string>>();

                // Update or add DataStoreTable entries based on existing data and new data
                for (int i = 0; i < dsNames.Count; i++)
                {
                    var existingDSName = existingDSNames.FirstOrDefault(n => n.DSName == dsNames[i]);

                    if (existingDSName != null)
                    {
                        // Update existing DataStoreTable entry
                        existingDSName.Memory = dsMemory[i];
                        existingDSName.Type = dsType[i];
                    }
                    else
                    {
                        // Add new DataStoreTable entry
                        RTRS_BaseEntities.GetContext().DataStoreTable.Add(new DataStoreTable
                        {
                            ComputerID = _comp.id,
                            DSName = dsNames[i],
                            Memory = dsMemory[i],
                            Type = dsType[i]
                        });
                    }

                    NameMeTyModels.Add(new Tuple<string, string, string>(dsNames[i], dsMemory[i], dsType[i]));
                }

                // Delete any DataStoreTable entries that are not in the updated NameMeTyModels list
                foreach (var existingDSName in existingDSNames)
                {
                    if (!NameMeTyModels.Contains(new Tuple<string, string, string>(existingDSName.DSName, existingDSName.Memory, existingDSName.Type)))
                    {
                        RTRS_BaseEntities.GetContext().DataStoreTable.Remove(existingDSName);
                    }
                }

                var motherModels = SP_Mather.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
                var existingMotherModels = RTRS_BaseEntities.GetContext().Motherboard_Table
                .Where(m => m.ComputerID == _comp.id && m.Mother_Model != null)
                .ToList();
                var motherProducers = SP_Mather2.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
                var existingMotherProducers = RTRS_BaseEntities.GetContext().Motherboard_Table
                    .Where(m => m.ComputerID == _comp.id && m.Mother_Producer != null)
                    .ToList();

                // Update or add Motherboard_Table entries based on existing data and new data
                foreach (var motherModel in motherModels)
                {
                    var motherProducer = motherProducers[motherModels.IndexOf(motherModel)];
                    var existingMotherModel = existingMotherModels.FirstOrDefault(m => m.Mother_Model == motherModel);

                    if (existingMotherModel != null)
                    {
                        var existingMotherProducer = existingMotherProducers.FirstOrDefault(m => m.id == existingMotherModel.id);
                        if (existingMotherProducer?.Mother_Producer != motherProducer)
                        {
                            existingMotherProducer.Mother_Producer = motherProducer;
                        }
                    }
                    else
                    {
                        RTRS_BaseEntities.GetContext().Motherboard_Table.Add(new Motherboard_Table
                        {
                            ComputerID = _comp.id,
                            Mother_Model = motherModel,
                            Mother_Producer = motherProducer
                        });
                    }
                }

                // Delete any Motherboard_Table entries that are not in the updated list
                foreach (var existingMotherModel in existingMotherModels)
                {
                    var existingMotherProducer = existingMotherProducers.FirstOrDefault(m => m.id == existingMotherModel.id);
                    var motherProducer = existingMotherProducer?.Mother_Producer;

                    if (!motherModels.Contains(existingMotherModel.Mother_Model) || !motherProducers.Contains(motherProducer))
                    {
                        RTRS_BaseEntities.GetContext().Motherboard_Table.Remove(existingMotherModel);
                    }
                }

                RTRS_BaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!");
            }
            catch (DbEntityValidationException ex)
            {
                string errors = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                MessageBox.Show($"Ошибка сохранения: {errors}");
            }
        }
    }
}

//private void Save_Click(object sender, RoutedEventArgs e)
//{
//    try
//    {
//        // Save changes made to the current computer
//        if (_comp.id == 0)
//            RTRS_BaseEntities.GetContext().Computer.Add(_comp);

//        // Save changes made to NIC_Table
//        var nicBinModels = new List<Tuple<string, string>>();

//        var nicModels = SP_Nic.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
//        var existingNicModels = RTRS_BaseEntities.GetContext().NIC_Table
//            .Where(n => n.ComputerID == _comp.id && n.NIC_Model != null)
//            .ToList();

//        var BinModels = SP_Bul.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
//        var existingBinModels = RTRS_BaseEntities.GetContext().NIC_Table
//            .Where(n => n.ComputerID == _comp.id && n.Built_in != null)
//            .ToList();

//        // Update or add NIC_Table entries based on existing data and new data
//        foreach (var nicModel in nicModels)
//        {
//            var binModel = BinModels[nicModels.IndexOf(nicModel)];
//            var existingNicModel = existingNicModels.FirstOrDefault(n => n.NIC_Model == nicModel);

//            if (existingNicModel != null)
//            {
//                var existingBinModel = existingBinModels.FirstOrDefault(n => n.id == existingNicModel.id);
//                if (existingBinModel?.Built_in != binModel)
//                {
//                    existingBinModel.Built_in = binModel;
//                }
//            }
//            else
//            {
//                RTRS_BaseEntities.GetContext().NIC_Table.Add(new NIC_Table
//                {
//                    ComputerID = _comp.id,
//                    NIC_Model = nicModel,
//                    Built_in = binModel
//                });
//            }

//            nicBinModels.Add(new Tuple<string, string>(nicModel, binModel));
//        }

//        // Delete any NIC_Table entries that are not in the updated nicBinModels list
//        foreach (var existingNicModel in existingNicModels)
//        {
//            var existingBinModel = existingBinModels.FirstOrDefault(n => n.id == existingNicModel.id);
//            var binModel = existingBinModel?.Built_in;

//            if (!nicBinModels.Contains(new Tuple<string, string>(existingNicModel.NIC_Model, binModel)))
//            {
//                RTRS_BaseEntities.GetContext().NIC_Table.Remove(existingNicModel);
//            }
//        }

//        // Save changes to database
//        RTRS_BaseEntities.GetContext().SaveChanges();
//        MessageBox.Show("Данные сохранены!");
//    }
//    catch (DbEntityValidationException ex)
//    {
//        string errors = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
//        MessageBox.Show($"Ошибка сохранения: {errors}");
//    }
//}


//try
//{
//    // Сохранение изменений в таблице Computer
//    if (_comp.id == 0)
//        RTRS_BaseEntities.GetContext().Computer.Add(_comp);
//    RTRS_BaseEntities.GetContext().SaveChanges();
//    // Сохранение изменений в таблице CPU_Table
//    if (_cpu.id == 0)
//        RTRS_BaseEntities.GetContext().CPU_Table.Add(_cpu);
//    else
//        RTRS_BaseEntities.GetContext().Entry(_cpu).State = EntityState.Modified;
//    RTRS_BaseEntities.GetContext().SaveChanges();

//    // Сохранение изменений в таблице RAM_Table
//    if (_ram.id == 0)
//        RTRS_BaseEntities.GetContext().RAM_Table.Add(_ram);
//    else
//        RTRS_BaseEntities.GetContext().Entry(_ram).State = EntityState.Modified;
//    RTRS_BaseEntities.GetContext().SaveChanges();

//    // Сохранение изменений в таблице NIC_Table
//    if (_nic.id == 0)
//        RTRS_BaseEntities.GetContext().NIC_Table.Add(_nic);
//    else
//        RTRS_BaseEntities.GetContext().Entry(_nic).State = EntityState.Modified;
//    RTRS_BaseEntities.GetContext().SaveChanges();

//    // Сохранение изменений в таблице DataStoreTable
//    if (_datastore.id == 0)
//        RTRS_BaseEntities.GetContext().DataStoreTable.Add(_datastore);
//    else
//        RTRS_BaseEntities.GetContext().Entry(_datastore).State = EntityState.Modified;
//    RTRS_BaseEntities.GetContext().SaveChanges();

//    MessageBox.Show("Данные сохранены!");
//}
//catch (DbEntityValidationException ex)
//{
//    string errors = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
//    MessageBox.Show($"Ошибка сохранения: {errors}");
//}


//public partial class BasicElementAdd : Window
//{
//    private Computer _comp = new Computer();
//    RTRS_BaseEntities context = RTRS_BaseEntities.GetContext();

//    public BasicElementAdd(Computer selectComputer)
//    {
//        InitializeComponent();
//        RTRS_BaseEntities.GetContext();

//        if (selectComputer != null)
//        {
//            _comp = selectComputer;
//        }
//        DataContext = _comp;

//        RTRS_BaseEntities context = RTRS_BaseEntities.GetContext();
//        CbVC.ItemsSource = RTRS_BaseEntities.GetContext().VC_Label.ToList();
//        CbType.ItemsSource = RTRS_BaseEntities.GetContext().DataStoreType.ToList();
//        //===========================================
//        var nicModels = RTRS_BaseEntities.GetContext().NIC_Table
//            .Where(n => n.ComputerID == _comp.id)
//            .OrderBy(n => n.ComputerID)
//            .ThenBy(n => n.id)
//            .Select(n => n.NIC_Model)
//            .ToList();

//        if (nicModels.Count > 1)
//        {
//            var groupedNicModels = nicModels.GroupBy(n => n);
//            var orderedNicModels = new List<string>();

//            foreach (var group in groupedNicModels)
//            {
//                orderedNicModels.AddRange(group);
//            }

//            // Add a new TextBox for each NIC_Model to the StPan stack panel
//            for (int i = 0; i < orderedNicModels.Count; i++)
//            {
//                var nicModelTextBox = new TextBox()
//                {
//                    Text = orderedNicModels[i],
//                    Name = "TxNic" + (StPan.Children.Count + 1).ToString(),
//                    MinWidth = 200,
//                    Margin = new Thickness(0)
//                };
//                StPan.Children.Add(nicModelTextBox);
//            }
//        }
//        else if (nicModels.Count == 1)
//        {
//            var nicModelTextBox = new TextBox()
//            {
//                Text = nicModels[0],
//                Name = "TxNic1",
//                MinWidth = 200,
//                Margin = new Thickness(0)
//            };
//            StPan.Children.Add(nicModelTextBox);
//        }
//        //===========================================
//        var BinModels = RTRS_BaseEntities.GetContext().NIC_Table
//            .Where(n => n.ComputerID == _comp.id)
//            .OrderBy(n => n.ComputerID)
//            .ThenBy(n => n.id)
//            .Select(n => n.Built_in)
//            .ToList();

//        if (BinModels.Count > 1)
//        {
//            var groupedBinModels = BinModels.GroupBy(n => n);
//            var orderedBinModels = new List<string>();

//            foreach (var group in groupedBinModels)
//            {
//                orderedBinModels.AddRange(group);
//            }

//            // Add a new TextBox for each NIC_Model to the StPan stack panel
//            for (int i = 0; i < orderedBinModels.Count; i++)
//            {
//                var BinModelTextBox = new TextBox()
//                {
//                    Text = orderedBinModels[i],
//                    Name = "TxBI" + (StPan2.Children.Count + 1).ToString(),
//                    MinWidth = 200,
//                    Margin = new Thickness(0)
//                };
//                StPan2.Children.Add(BinModelTextBox);
//            }
//        }
//        else if (BinModels.Count == 1)
//        {
//            var BinModelTextBox = new TextBox()
//            {
//                Text = BinModels[0],
//                Name = "TxBI1",
//                MinWidth = 200,
//                Margin = new Thickness(0)
//            };
//            StPan2.Children.Add(BinModelTextBox);
//        }
//    }
//    private void DobNIC_Click(object sender, RoutedEventArgs e)
//    {
//        var nicModelTextBox = new TextBox()
//        {
//            Text = "",
//            Name = "TxNic" + (StPan.Children.Count + 1).ToString(),
//            MinWidth = 200,
//            Margin = new Thickness(0)
//        };
//        //===========================================
//        var binModelTextBox = new TextBox()
//        {
//            Text = "",
//            Name = "TxBI" + (StPan2.Children.Count + 1).ToString(),
//            MinWidth = 200,
//            Margin = new Thickness(0)
//        };
//        StPan.Children.Add(nicModelTextBox);
//        StPan2.Children.Add(binModelTextBox);
//    }

//    private void DelNIC_Click(object sender, RoutedEventArgs e)
//    {
//        if (StPan.Children.Count > 1)
//        {
//            var lastNicTextBox = StPan.Children.OfType<TextBox>().LastOrDefault();
//            var lastBinTextBox = StPan2.Children.OfType<TextBox>().LastOrDefault();

//            if (lastNicTextBox != null)
//            {
//                StPan.Children.Remove(lastNicTextBox);
//            }
//            if (lastBinTextBox != null)
//            {
//                StPan2.Children.Remove(lastBinTextBox);
//            }
//        }
//    }
//    private void Save_Click(object sender, RoutedEventArgs e)
//    {
//        if (_comp.id == 0)
//            RTRS_BaseEntities.GetContext().Computer.Add(_comp);
//        try
//        {
//            RTRS_BaseEntities.GetContext().SaveChanges();
//            MessageBox.Show("Данные сохранены!");
//        }
//        catch (Exception Ex)
//        {
//            MessageBox.Show(Ex.Message.ToString());
//        }
//    }
//}
//}


//List<NIC_Table> nics = context.NIC_Table.Where(n => n.ComputerID == _currentComputer.id).ToList();

//if (nics.Any())
//{
//    TxNic.Text = nics.First().NIC_Model;
//    TxBI.Text = nics.First().BuiltIn_Table.BIName;

//    for (int i = 1; i < nics.Count; i++)
//    {
//        System.Windows.Controls.TextBox textBox = new System.Windows.Controls.TextBox();
//        textBox.Text = nics[i].NIC_Model;
//        StPan.Children.Add(textBox);

//        System.Windows.Controls.TextBox textBoxBuiltIn = new System.Windows.Controls.TextBox();
//        textBoxBuiltIn.Text = nics[i].BuiltIn_Table.BIName;
//        StPan2.Children.Add(textBoxBuiltIn);
//    }
//}
//else
//{
//    TxNic.Text = "Нет записей для данного компьютера";
//    TxBI.Text = "";
//}

//private void Save_Click(object sender, RoutedEventArgs e)
//{
//    if (_currentComputer.id == 0)
//        RTRS_BaseEntities.GetContext().Computer.Add(_currentComputer);
//    try
//    {
//        RTRS_BaseEntities.GetContext().SaveChanges();
//        MessageBox.Show("Данные сохранены!");
//    }
//    catch (Exception Ex)
//    {
//        MessageBox.Show(Ex.Message.ToString());
//    }
//}

//List<NIC_Table> nics = context.NIC_Table.Where(n => n.ComputerID == _comp.id).ToList();

//if (nics.Any())
//{
//    TxNic1.Text = nics.First().NIC_Model;

//    for (int i = 1; i < nics.Count; i++)
//    {
//        TextBox textBox = new TextBox();
//        textBox.Text = nics[i].NIC_Model;
//        StPan.Children.Add(textBox);

//        ComboBox comboBoxBuiltIn = new ComboBox();
//        comboBoxBuiltIn.ItemsSource = context.BuiltIn_Table.ToList();
//        comboBoxBuiltIn.SelectedValue = nics[i].BuiltIn_Table.id;
//        comboBoxBuiltIn.SelectedValuePath = "id";
//        comboBoxBuiltIn.DisplayMemberPath = "BIName";
//        StPan2.Children.Add(comboBoxBuiltIn);
//    }
//}
//else
//{
//    TxNic1.Text = "Нет записей для данного компьютера";
//    ComboBoxBI.SelectedItem = null;
//}

//private void Save_Click(object sender, RoutedEventArgs e)
//{
//    try
//    {
//        // Save changes made to the current computer
//        if (_comp.id == 0)
//            RTRS_BaseEntities.GetContext().Computer.Add(_comp);
//        RTRS_BaseEntities.GetContext().SaveChanges();

//        // Save changes made to nicModels
//        var nicModels = StPan.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
//        var existingNicModels = RTRS_BaseEntities.GetContext().NIC_Table
//            .Where(n => n.ComputerID == _comp.id && n.NIC_Model != null)
//            .ToList();

//        // Delete any NIC_Table entries that are not in the updated nicModels list
//        foreach (var existingNicModel in existingNicModels)
//        {
//            if (!nicModels.Contains(existingNicModel.NIC_Model))
//            {
//                RTRS_BaseEntities.GetContext().NIC_Table.Remove(existingNicModel);
//            }
//        }

//        // Add any new NIC_Table entries that are not in the existing list
//        foreach (var nicModel in nicModels)
//        {
//            if (!existingNicModels.Any(n => n.NIC_Model == nicModel))
//            {
//                RTRS_BaseEntities.GetContext().NIC_Table.Add(new NIC_Table
//                {
//                    ComputerID = _comp.id,
//                    NIC_Model = nicModel
//                });
//            }
//        }

//        // Save changes made to BinModels
//        var BinModels = StPan2.Children.OfType<TextBox>().Select(tb => tb.Text).ToList();
//        var existingBinModels = RTRS_BaseEntities.GetContext().NIC_Table
//            .Where(n => n.ComputerID == _comp.id && n.Built_in != null)
//            .ToList();

//        // Delete any NIC_Table entries that are not in the updated BinModels list
//        foreach (var existingBinModel in existingBinModels)
//        {
//            if (!BinModels.Contains(existingBinModel.Built_in))
//            {
//                RTRS_BaseEntities.GetContext().NIC_Table.Remove(existingBinModel);
//            }
//        }

//        // Add any new NIC_Table entries that are not in the existing list
//        foreach (var binModel in BinModels)
//        {
//            if (!existingBinModels.Any(n => n.Built_in == binModel))
//            {
//                RTRS_BaseEntities.GetContext().NIC_Table.Add(new NIC_Table
//                {
//                    ComputerID = _comp.id,
//                    Built_in = binModel
//                });
//            }
//        }

//        RTRS_BaseEntities.GetContext().SaveChanges();
//        MessageBox.Show("Данные сохранены!");
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show($"Ошибка сохранения: {ex.Message}");
//    }
//}