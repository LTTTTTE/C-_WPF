using System;
using System.Collections.Generic;
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

namespace DragonApp
{
    /// <summary>
    /// Window2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void bt_register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_id.Text.Length >= 2 && tb_pswd.Password.Length >= 2&&!tb_id.Text.Contains(" "))
                {
                    List<SqlParameter> sql_params = new List<SqlParameter>();
                    sql_params.Add(new SqlParameter("Username", tb_id.Text));
                    sql_params.Add(new SqlParameter("Password", tb_pswd.Password));

                    DAL.Exec_sp("CreateUser", sql_params);

                    MessageBox.Show("회원가입 완료");
                    this.Close();
                }
                else
                {
                    tb_id.Text = "";
                    tb_pswd.Password = "";
                    MessageBox.Show("다시입력");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("으억");
                this.Close();
                throw;
            }
            finally
            {

            }
            
        }

        private void bt_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();            
        }
    }
}
