using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Windows.Media.Fonts;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for FontToolbar.xaml
    /// </summary>
    public partial class FontToolbar : UserControl
    {
        public ObservableCollection<double> list;
        public event EventHandler ComboboxSizeChanged;
        public event EventHandler ComboboxFontChanged;

        public FontToolbar()
        {
            InitializeComponent();

            list = new ObservableCollection<double>();

            for (int i = 6; i < 25; i++)
            {
                list.Add(Convert.ToDouble(i));
            }

            FontSizesComboBox.ItemsSource = list;
        }

        private void FontsListComboBox_SelectionChanged(object sender, EventArgs e)
        {
            if (this.ComboboxFontChanged != null)
            {
                this.ComboboxFontChanged(sender, e);
            }
        }

        private void FontSizesComboBox_SelectionChanged(object sender, EventArgs e)
        {
            if (this.ComboboxSizeChanged != null)
            {
                this.ComboboxSizeChanged(sender, e);
            }
        }
    }
}
