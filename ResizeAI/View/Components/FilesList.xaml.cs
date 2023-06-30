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

namespace ResizeAI.View.Components
{
    public partial class FilesList : UserControl
    {
        public FilesList()
        {
            InitializeComponent();
        }

        private void FilesSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is ScrollViewer container)
            {
                // 40 is a magic number because on the first and second launch content.ActualHeight is not 0, which is strange because the ListView is empty
                DragAndDropControl.Margin = new Thickness(0, 0, 0, container.ActualHeight >= 5 ? container.ActualHeight + 10 : 2);
            }
        }
    }
}
