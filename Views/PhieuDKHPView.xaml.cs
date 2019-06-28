using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Views
{
    /// <summary>
    /// Interaction logic for PhieuDKHPView.xaml
    /// </summary>
    public partial class PhieuDKHPView : UserControl
    {
        public PhieuDKHPView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double TiLeMienGiam;
            TextBox source = e.Source as TextBox;
            if(double.TryParse(source.Text, out TiLeMienGiam))
            {
                source.Text = TiLeMienGiam.ToString("0%");
            }
        }
    }
}
