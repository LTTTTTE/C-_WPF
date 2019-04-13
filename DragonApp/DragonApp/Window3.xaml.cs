using System;
using System.Collections.Generic;
using System.IO;
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

namespace DragonApp
{
    /// <summary>
    /// Window3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            var hi = "SDFEG";
            InitializeComponent();
        }

        private void bt_test_Click(object sender, RoutedEventArgs e)
        {
            var IDBoxElement = new TextBox();
            IDBoxElement.FontSize = 20;
            IDBoxElement.Foreground = new SolidColorBrush(Color.FromRgb(120, 120, 0));
            IDBoxElement.Text = "Enter ID here...";
            IDBoxElement.LostFocus += (s, ee) => { if (string.IsNullOrWhiteSpace(IDBoxElement.Text)) IDBoxElement.Text = "Enter ID here..."; };
            IDBoxElement.GotFocus += (s, ee) => IDBoxElement.Text = string.Empty;
            IDBoxElement.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            using (FileStream fs = new FileStream("test.txt", System.IO.FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                tb_test.Text = sr.ReadToEnd();
            }
        }
    }
}
