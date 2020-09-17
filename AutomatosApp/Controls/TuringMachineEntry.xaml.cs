using AutomatosData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace proj_automatos.Controls
{
    /// <summary>
    /// Interaction logic for TuringMachineEntry.xaml
    /// </summary>
    public partial class TuringMachineEntry : UserControl
    {
        public TuringMachineEntry()
        {
            InitializeComponent();
        }

        public TuringMachineEntry(TuringMachineDataEntry turingMachineDataEntry, bool isSource)
        {
            InitializeComponent();

            if (isSource)
            {
                Border.Width = 50;
                Border.Width = 50;
                Border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#d2dae2");
                Arrow.Visibility = Visibility.Visible;
            }

            EntryContent.Content = turingMachineDataEntry.Data;
        }

        public void SetSource(bool isSource)
        {
            if (isSource)
            {
                Border.Width = 50;
                Border.Width = 50;
                Border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#d2dae2");
                Arrow.Visibility = Visibility.Hidden;
            } else
            {
                Border.Width = 40;
                Border.Width = 40;
                Border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#303952");
                Arrow.Visibility = Visibility.Hidden;
            }
        }

        public void SetData(char data)
        {
            EntryContent.Content = data;
        }
    }
}
