using UnityEditor.IMGUI.Controls;
using ABManagerCore.Interfaces;

namespace ABManagerEditor.Browser.Blocks.Manager
{
    public class TemplateTreeViewItem<TTemplate> : TreeViewItem
        where TTemplate : ITemplateInfo
    {
        internal TTemplate Item { get; }
        internal TemplateTreeViewItem() : base()
        {

        }
        internal TemplateTreeViewItem(TTemplate item) : base(item.Name.GetHashCode(), 1, item.Name)
        {
            Item = item;
        }
        public override string displayName
        {
            get => Item == null ? string.Empty : Item.Name;
        }
    }
}

