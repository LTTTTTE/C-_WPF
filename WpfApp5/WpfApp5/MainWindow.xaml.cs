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
using Microsoft.VisualBasic;

namespace WpfApp5
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    { 
        LinkedList<Student> students = new LinkedList<Student>();
        public MainWindow()
        {
            InitializeComponent();

            Student jude = new Student
            {
                student_ID = "001",
                student_Name = "Jude",
                student_Address = "Busan 33",
                student_Age = "24",
                student_Country = "Korea"
            };
            
            DataGrid.Items.Add(jude);

            Student april = new Student
            {
                student_ID = "002",
                student_Name = "April",
                student_Address = "Seoul 188",
                student_Age = "20",
                student_Country = "Korea"
            };

            DataGrid.Items.Add(april);

            students.AddFirst(jude);
            students.AddFirst(april);
            
          
        }

        private void button_add_student_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window1 = new Window1();
            Window1.ShowDialog();
            if ((bool)Window1.DialogResult)
            {
                Student student = new Student();
                student.student_ID = Window1.textbox_ID.Text;
                student.student_Name = Window1.textbox_Name.Text;
                student.student_Address = Window1.textbox_Address.Text;
                student.student_Age = Window1.textbox_Age.Text;
                student.student_Country = Window1.textbox_Country.Text;
                DataGrid.Items.Add(student);
                students.AddFirst(student);
            }
            
        }

        private void buttton_del_student_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.ShowDialog();
            if((bool)window2.DialogResult)
            {
                try
                {
                    foreach (Student temp in students)
                    {
                        if (temp.student_ID == window2.tb_del.Text)
                        {
                            students.Remove(temp);
                            DataGrid.Items.Remove(temp);
                            MessageBox.Show("삭제완료");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("다시입력"+ ex.Message);
                }
           
            }
        }
    }
    public class Student
    {
        public string student_ID { get; set; }
        public string student_Name { get; set; }
        public string student_Address { get; set; }
        public string student_Age { get; set; }
        public string student_Country { get; set; }
    }
}
