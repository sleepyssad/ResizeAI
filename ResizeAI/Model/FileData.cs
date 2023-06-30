using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResizeAI.Model
{
    public partial class FileData : ObservableObject
    {
        [ObservableProperty]
        string path;

        [ObservableProperty]
        string name;
    }
}
