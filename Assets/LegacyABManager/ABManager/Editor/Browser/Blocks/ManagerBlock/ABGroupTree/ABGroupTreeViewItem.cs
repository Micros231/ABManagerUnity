using ABManagerEditor.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace ABManagerEditor.Browser.Blocks
{
    public class ABGroupTreeViewItem : TreeViewItem
    {
        internal ABGroup Group { get; }
        public ABGroupTreeViewItem(int id, int depth) : base(id, depth)
        {

        }
        public ABGroupTreeViewItem(ABGroup group, int depth) : base(group.Name.GetHashCode(),depth,group.Name)
        {
            Group = group;
        }
        public override string displayName
        {
            get => Group == null ? string.Empty : Group.Name;
        }
    }
}

