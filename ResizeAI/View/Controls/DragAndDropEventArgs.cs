using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResizeAI.View.Controls
{
    public class DragAndDropEventArgs : EventArgs
    {
        private readonly List<string> pathList;

        public DragAndDropEventArgs(List<string> list)
        {
            pathList = list;
        }

        public List<string> PathList
        {
            get { return pathList; }
        }
    }
}
