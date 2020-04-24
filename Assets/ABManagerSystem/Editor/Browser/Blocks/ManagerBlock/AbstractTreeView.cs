using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
namespace ABManagerEditor.Browser.Blocks.Manager
{
    internal abstract class AbstractTreeView<TItem> : TreeView, ISelectTreeItem<TItem>
    {
        public event Action<TItem> OnSelectItem;
        protected bool ContextOnItem;
        internal AbstractTreeView(TreeViewState state) : base(state)
        {
            Refresh();
        }
        


        protected override TreeViewItem BuildRoot()
        {
            var root = CreateGroupTreeView();
            return root;
        }
        protected override bool CanRename(TreeViewItem item)
        {
            return true;
        }
        protected abstract TreeViewItem CreateGroupTreeView();
        public abstract void SelectItem(TItem item);

        internal void Refresh()
        {
            Reload();
            SetFocusAndEnsureSelectedItem();
        }
    }
}

