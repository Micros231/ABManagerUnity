using UnityEngine;
using UnityEditor;
using ABManagerEditor.Models;
using ABManagerEditor.Controller;
using System;

namespace ABManagerEditor.Browser.Blocks
{
    internal class MainBlock : AbstractBlock
    {
        private enum TabType
        {
            Manager = 0,
            Settings = 1,
            Build = 2
        }
        private AbstractBlock _managerBlock, _settingsBlock, _buildBlock;
        private TabType _tabType;
        private readonly ABManagerController _controller;
        internal MainBlock(ABManagerController controller)
        {
            _controller = controller;
        }

        internal override void OnEnable()
        {
            if (_managerBlock == null)
            {
                _managerBlock = new ManagerBlock(_controller);
            }             
            if (_settingsBlock == null)
            {
                _settingsBlock = new SettingsBlock(_controller);
            }              
            if (_buildBlock == null)
            {
                _buildBlock = new BuildBlock(_controller);
            }
                
            _managerBlock.OnEnable();
            _settingsBlock.OnEnable();
            _buildBlock.OnEnable();
        }
        internal override void OnDisable()
        {
            if (_managerBlock != null)
                _managerBlock.OnDisable();
            if (_settingsBlock != null)
                _settingsBlock.OnDisable();
            if (_buildBlock != null)
                _buildBlock.OnDisable();

        }
        internal override void OnGUI(Rect screenRect)
        {
            GUILayout.BeginHorizontal(GUILayout.Height(30));
            _tabType = (TabType)GUILayout.Toolbar((int)_tabType, Enum.GetNames(typeof(TabType)));
            GUILayout.EndHorizontal();
           
            Rect newPosition = new Rect(screenRect.x, screenRect.y + 30, screenRect.width, screenRect.height - (screenRect.y + 30));
            switch (_tabType)
            {
                case TabType.Manager:
                    _managerBlock.OnGUI(newPosition);
                    break;
                case TabType.Settings:
                    _settingsBlock.OnGUI(newPosition);
                    break;
                case TabType.Build:
                    _buildBlock.OnGUI(newPosition);
                    break;
            }
        }
    }
}

