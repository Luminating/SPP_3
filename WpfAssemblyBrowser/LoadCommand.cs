using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Reflection;
using System.IO;
using AssemblyBrowser;

namespace WpfAssemblyBrowser
{
    public class LoadCommand : ICommand
    {
        protected ApplicationViewModel viewModel;
        public LoadCommand(ApplicationViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter) 
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                
                try
                {
                    AssemblyName testAssembly = AssemblyName.GetAssemblyName(path);
                    viewModel.AssemblyName = openFileDialog.SafeFileName;
                    AssemblyReader assemblyReader = new AssemblyReader();
                    viewModel.Nodes = assemblyReader.GetResult(path);
                }

                catch (FileNotFoundException)
                {
                    viewModel.AssemblyName = "The file cannot be found.";
                    viewModel.Nodes = null;
                }

                catch (BadImageFormatException)
                {
                    viewModel.AssemblyName = "The file is not an assembly.";
                    viewModel.Nodes = null;
                }

                catch (FileLoadException)
                {
                    viewModel.AssemblyName = "The assembly has already been loaded.";
                }


            }
        }
    }
}

// https://streletzcoder.ru/pattern-komanda-v-wpf-strogiy-variant-realizatsii-patterna-mvvm/
// http://www.codeproject.com/Articles/26288/Simplifying-the-WPF-TreeView-by-Using-the-ViewMode