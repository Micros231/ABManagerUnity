using ABManagerEditor.Controller;
using ABManagerEditor.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace ABManagerEditor.Browser.Blocks
{
    internal class ManagerBlock : AbstractBlock
    {
        private GroupsBlock _groupsBlock;
        private AssetsBlock _assetsBlock;

        private readonly ABManagerController _controller;
        public ManagerBlock(ABManagerController controller)
        {
            _controller = controller;
        }
        internal override void OnEnable()
        {
            if (_groupsBlock == null)
            {
                _groupsBlock = new GroupsBlock(_controller);
            }
            if (_assetsBlock == null)
            {
                _assetsBlock = new AssetsBlock(_controller.Creators.AssetCreator);
            }
           
            _groupsBlock.OnEnable();
            _assetsBlock.OnEnable();

            _groupsBlock.OnSelectABGroup += OnSelectGroup;
        }
        internal override void OnDisable()
        {
            if (_groupsBlock != null)
                _groupsBlock.OnDisable();
            if (_assetsBlock != null)
                _assetsBlock.OnDisable();

            _groupsBlock.OnSelectABGroup -= OnSelectGroup;
        }
        internal override void OnGUI(Rect screenRecct)
        {
            float leftWidth = screenRecct.width / 2;
            float rightWidth = screenRecct.width - leftWidth;
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            _groupsBlock.OnGUI(new Rect(screenRecct.x, screenRecct.y, leftWidth, screenRecct.height));
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            _assetsBlock.OnGUI(new Rect(screenRecct.x + leftWidth, screenRecct.y, rightWidth, screenRecct.height));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        private void OnSelectGroup(ABGroup group)
        {
            _assetsBlock.UpdateGroup(group);
        }
    }
}

