using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ResizeAI.Model;
using ResizeAI.View.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace ResizeAI.ViewModel
{
    public partial class PrimaryViewModel : ObservableObject
    {
        public ICommand StorageCommand { get; set; }

        public ICommand DeleteFileCommand { get; set; }

        [ObservableProperty]
        ObservableCollection<FileData> files;

        public PrimaryViewModel() 
        {
            Files = new ObservableCollection<FileData>();
            StorageCommand = new RelayCommand<DragAndDropEventArgs>(Storage);
            DeleteFileCommand = new RelayCommand<string>(DeleteFile);
        }

        private void Storage(DragAndDropEventArgs eventArgs)
        {
            foreach (var file in eventArgs.PathList)
            {
                if (Files.FirstOrDefault(f => f.Path.Equals(file)) is null)
                {
                    Files.Add(new FileData { Name = file, Path = file });
                }
            }
        }

        private void DeleteFile(string path)
        {
            if (Files.FirstOrDefault(f => f.Path.Equals(path)) is FileData file)
            {
                Files.Remove(file);
            }
        }
    }
}
