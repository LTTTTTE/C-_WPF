using System;
using System.IO;
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

namespace WpfTreeVIew
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();

            var t = new TreeViewItem();

        }
        #endregion

        #region On Loaded
        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem();

                item.Header = drive;

                item.Tag = drive;

                item.Items.Add(null);

                item.Expanded += Folder_Expended;

                FolderView.Items.Add(item);
            }
        }

        private void Folder_Expended(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (item.Items.Count == 1 || item.Items[0] == null)
            {
                return;
            }

            item.Items.Clear();

            var fullpath = (string)item.Tag;
            var directories = new List<string>();

            try
            {
                var dirs = Directory.GetDirectories(fullpath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            directories.ForEach(directoryPath => {
                var subItem = new TreeViewItem()
                {
                    Header = Path.GetDirectoryName(directoryPath),
                    Tag = directoryPath
                };
            });
        }
        #endregion
    }
}
