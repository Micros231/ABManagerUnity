using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using ABManagerEditor.Models;

namespace ABManagerEditor.Browser.Blocks
{
    public class ABAssetTreeItem : TreeViewItem
    {
        internal ABAsset Asset { get; }
        public ABAssetTreeItem(int id, int depth) : base(id, depth)
        {

        }
        public ABAssetTreeItem(ABAsset asset, int depth) : base(asset.Name.GetHashCode(), depth, asset.Name)
        {
            Asset = asset;
        }
        public override string displayName
        {
            get => Asset == null ? string.Empty : Asset.Name;
        }
    }
}

