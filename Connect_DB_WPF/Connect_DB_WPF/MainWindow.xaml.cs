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

namespace Connect_DB_WPF
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext dc = new DataClasses1DataContext(
            Properties.Settings.Default.MyLoginAppConnectionString); //데이터콘텍스트생성

        public MainWindow()
        {
            InitializeComponent();

            if (dc.DatabaseExists()) dg_test.ItemsSource = dc.Users; //데이터그리드 내용에 데이터콘텍스트.user테이블 내용 대입
          
        }
         
        private void bt_save_Click(object sender, RoutedEventArgs e)
        {
            dc.SubmitChanges();
            MessageBox.Show("SUCCESSFULLY SAVED");
        }

        private void bt_insert_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();

            if ((bool)window1.DialogResult)
            {
                Users newusers = new Users();
                newusers.Username = window1.tb_user.Text;
                newusers.Password = window1.tb_pswd.Text;
                dc.Users.InsertOnSubmit(newusers);
                dc.SubmitChanges();
                dg_test.ItemsSource = dc.Users;
                MessageBox.Show("Successfully Add");
            }

        }

        private void bt_delete_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void bt_refresh_Click(object sender, RoutedEventArgs e)
        {
            dg_test.ItemsSource = null;
            dg_test.ItemsSource = dc.Users;
            dg_test.Items.Refresh();    
        }
    }
}
