using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AssemblyBrowser;

namespace WpfAssemblyBrowser
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private ICommand _loadCommand;
        private string assemblyName;

        public string AssemblyName
        {
            get { return assemblyName; }
            set
            {
                assemblyName = value;
                OnPropertyChanged("AssemblyName");
            }
        }

        private ObservableCollection<TreeNode> nodes;
        public ObservableCollection<TreeNode> Nodes
        {
            get { return nodes; }
            set
            {
                nodes = value;
                OnPropertyChanged("Nodes");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand Load
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new LoadCommand(this);
                }
                return _loadCommand;
            }
        }
    }
}
