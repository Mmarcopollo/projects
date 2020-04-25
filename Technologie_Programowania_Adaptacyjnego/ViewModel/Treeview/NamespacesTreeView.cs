﻿using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Treeview
{
    public class NamespacesTreeView : TreeViewNode
    {

        private NamespaceMetadata _namespaceMeta;

        public NamespacesTreeView(NamespaceMetadata namespaceMeta)
        {
            _namespaceMeta = namespaceMeta;
            Name = namespaceMeta.NamespaceName;
            TypeOfMetadata = "namespace";
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
            if (_namespaceMeta.Types != null)
                foreach (TypeMetadata type in _namespaceMeta.Types)
                {
                    children.Add(new TypeTreeView(TypeMetadata.TypeDictionary[type.TypeName]));
                }
        }
    }
}
