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

namespace kolokwium
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MinMaxCB.Items.Add("Maksimum");
            MinMaxCB.Items.Add("Minimum");
            MinMaxCB.SelectedItem = MinMaxCB.Items[0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)MinMaxCB.SelectedItem == "Maksimum")
            {
                ResultTB.Text = new Genetic(2, Functions.Kolokwium, (int)RangeLeftSlider.Value, (int)RangeRightSlider.Value, true, (int)PopulationSlider.Value, (int)GenerationsSlider.Value).Result;
            }
            else
            {
                ResultTB.Text = new Genetic(2, Functions.Kolokwium, (int)RangeLeftSlider.Value, (int)RangeRightSlider.Value, false, (int)PopulationSlider.Value, (int)GenerationsSlider.Value).Result;
            }
        }

        private void GenerationsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = (Slider)sender;
            GenerationsLabel.Content = $"Generacje: {(int)slider.Value}";
        }

        private void PopulationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = (Slider)sender;
            PopulationLabel.Content = $"Populacja: {(int)slider.Value}";
        }

        private void RangeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (RangeLeftSlider.Value < RangeRightSlider.Value)
                RangeLabel.Content = $"Przedział: [{(int)RangeLeftSlider.Value}, {(int)RangeRightSlider.Value}]";
            else
            {
                if (RangeLeftSlider.Value == RangeLeftSlider.Minimum)
                    RangeRightSlider.Value++;
                else
                    RangeLeftSlider.Value--;
            }
                
        }
    }
}
