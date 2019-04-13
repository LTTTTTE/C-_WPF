using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;

namespace DragonApp
{
    /// <summary>
    /// Window5.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window5 : Window
    {
        DataClasses1DataContext db;

        public Window5()
        {
            InitializeComponent();

            db = new DataClasses1DataContext();
            grid_weather.ItemsSource = db.weathers;

        }
        public static IEnumerable<FrameworkElement> GetItemsInColumn(DataGrid dg, int col)
        {
            if (dg.Columns.Count <= col)
                throw new IndexOutOfRangeException("Column " + col + " is out range, the datagrid only contains " + dg.Columns.Count + " columns.");
            foreach (object item in dg.Items)
            {
                yield return dg.Columns[col].GetCellContent(item);
            }
        }
    }
}
