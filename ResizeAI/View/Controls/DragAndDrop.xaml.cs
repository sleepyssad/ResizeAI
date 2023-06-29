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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using static System.Net.WebRequestMethods;


namespace ResizeAI.View.Controls
{

    public partial class DragAndDrop : UserControl
    {
        readonly string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        bool isMousePressed = false;

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(DragAndDrop), new PropertyMetadata(null));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }


        public event EventHandler<DragAndDropEventArgs> OnStorage;

        public DragAndDrop()
        {
            InitializeComponent();
        }


        private void Grid_Drop(object sender, DragEventArgs e)
        {
            List<string> files = new List<string>();

            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);

                    foreach (string path in paths)
                    {
                        if (Directory.Exists(path))
                        {
                            // found folder
                            foreach (string imagePath in Directory.GetFiles(path).Where(f => imageExtensions.Contains(System.IO.Path.GetExtension(f).ToLower())))
                            {
                                files.Add(imagePath);
                            }
                        }
                        else
                        {
                            if (imageExtensions.Contains(System.IO.Path.GetExtension(path).ToLower()))
                            {
                                // found image
                                files.Add(path);
                            }
                            else
                            {
                                MessageBox.Show("You can only use images or folders!");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

                return;
            }

            if (files.Count == 0)
            {
                MessageBox.Show("Not found");
                return;
            }

            var DropEventArgs = new DragAndDropEventArgs(files);


            OnStorage?.Invoke(this, DropEventArgs);

            if (Command != null && Command.CanExecute(DropEventArgs))
            {
                Command.Execute(DropEventArgs);
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMousePressed = true;
        }

        private async void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            List<string> files = new List<string>();

            if (isMousePressed)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = true;
                dialog.Filter = "Images (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    foreach (string path in dialog.FileNames)
                    {
                        if (imageExtensions.Contains(System.IO.Path.GetExtension(path).ToLower()))
                        {
                            // found image
                            files.Add(path);
                        }
                        else
                        {
                            MessageBox.Show("You can only use images or folders!");
                        }
                    }
                }
                else
                {
                    isMousePressed = false;
                    return;
                }
            }

            if (files.Count == 0)
            {
                MessageBox.Show("Not found");
                return;
            }

            var DropEventArgs = new DragAndDropEventArgs(files);


            OnStorage?.Invoke(this, DropEventArgs);

            if (Command != null && Command.CanExecute(DropEventArgs))
            {
                Command.Execute(DropEventArgs);
            }

            isMousePressed = false;
        }
    }
}
