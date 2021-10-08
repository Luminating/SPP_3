using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public class TreeNode
    {        
        public string Name { get; set; }
        public ObservableCollection<TreeNode> Nodes { get; set; }
        public TreeNode()
        {
            this.Nodes = new ObservableCollection<TreeNode>();
        }
        public TreeNode(string name)
        {
            this.Name = name;
            this.Nodes = new ObservableCollection<TreeNode>();
        }

    }
}
