using System;
using UnityEngine;
using ABManagerCore.Enums;
using ABManagerCore.Consts;
using ABManagerEditor.Controller;
using ABManagerEditor.Browser.Blocks.Build;
using ABManagerEditor.Browser.Blocks.Manager;
using ABManagerEditor.Browser.Blocks.Settings;

namespace ABManagerEditor.Browser.Blocks
{
    internal class MainBlock : AbstractTabBlock<MainTabType>
    {
        private AbstractBlock _managerBlock, _settingsBlock, _buildBlock;
        internal override void OnEnable()
        {
            if (_managerBlock == null)
            {
                _managerBlock = new ManagerBlock();
            }
            if (_settingsBlock == null)
            {
                _settingsBlock = new SettingsBlock();
            }
            if (_buildBlock == null)
            {
                _buildBlock = new BuildBlock();
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
            OnGUITabToolbar(screenRect);
        }
        protected override void PreviousGUITab()
        {
            GUILayout.Space(Sizes.Spaces.space_15);
            if (GUILayout.Button(Names.SaveSettings, GUILayout.Width(Sizes.Widths.width_120)))
            {
                if (ABController.Current.ManagerSettings != null && ABController.Current.HostSettings != null)
                {
                    ABController.Current.Saver.SaveAllSettings();
                }
                else
                {
                    Debug.LogWarning("Manager and Host Settings is null");
                }

            }
        }
        protected override void SwitchTab()
        {
            switch (_tabType)
            {
                case MainTabType.Manager:
                    _currentBlock = _managerBlock;
                    break;
                case MainTabType.Settings:
                    _currentBlock = _settingsBlock;
                    break;
                case MainTabType.Build:
                    _currentBlock = _buildBlock;
                    break;
            }
        }
    }
}

