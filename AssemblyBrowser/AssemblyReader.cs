using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace AssemblyBrowser
{
    public class AssemblyReader 
    {
        private Assembly asm; 
        private ObservableCollection<TreeNode> nodes = new ObservableCollection<TreeNode>();
        public ObservableCollection<TreeNode> GetResult(string path)
        {
            asm = Assembly.LoadFrom(path);
            Type[] asmTypes = asm.GetTypes();
            foreach (var type in asmTypes)
            {
                TreeNode currentSpaceNameNode = new TreeNode();
                bool isNameSpacePresent = false;
                foreach (TreeNode node in nodes)
                {
                    if (node.Name.Equals(type.Namespace))
                    {
                        isNameSpacePresent = true;
                        currentSpaceNameNode = node;
                    }
                }
                if (!isNameSpacePresent)
                {
                    currentSpaceNameNode.Name = type.Namespace;
                    nodes.Add(currentSpaceNameNode);
                }

                TreeNode dataTypeNode = new TreeNode();
                dataTypeNode.Name = type.Name;
                currentSpaceNameNode.Nodes.Add(dataTypeNode);
                TreeNode fieldsNode = new TreeNode("fields:");
                TreeNode methodsNode = new TreeNode("methods:");
                TreeNode propertiesNode = new TreeNode("properties:");
                dataTypeNode.Nodes.Add(fieldsNode);
                dataTypeNode.Nodes.Add(propertiesNode);
                dataTypeNode.Nodes.Add(methodsNode);

                foreach (FieldInfo field in type.GetFields().ToList())
                {
                    fieldsNode.Nodes.Add(new TreeNode(field.FieldType.Name + "  " + field.Name));
                }

                foreach (MethodInfo method in type.GetMethods().ToList())
                {
                    string signature = "(";
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length != 0)
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            signature += parameters[i];
                            if (i != parameters.Length - 1) signature += ", ";
                        }
                    }
                    signature += ")";
                    methodsNode.Nodes.Add(new TreeNode(method.ReturnType.Name + "  " + method.Name + signature));
                }

                foreach (PropertyInfo property in type.GetProperties().ToList())
                {
                    propertiesNode.Nodes.Add(new TreeNode(property.PropertyType.Name + "  " + property.Name));
                }
            }

            return nodes;

        }
    }
}
