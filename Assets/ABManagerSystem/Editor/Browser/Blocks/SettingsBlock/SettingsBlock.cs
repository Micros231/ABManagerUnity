using UnityEngine;
using ABManagerCore.Enums;

namespace ABManagerEditor.Browser.Blocks.Settings
{
    internal class SettingsBlock : AbstractTabBlock<SettingsTabType>
    {
        private AbstractBlock _managerSettings, _hostSettings;
        internal override void OnEnable()
        {
            if (_managerSettings == null)
            {
                _managerSettings = new ManagerSettingsBlock();
            }
            if (_hostSettings == null)
            {
                _hostSettings = new HostSettingsBlock();
            }
            _managerSettings.OnEnable();
            _hostSettings.OnEnable();
        }
        internal override void OnDisable()
        {
            if (_managerSettings != null)
            {
                _managerSettings.OnDisable();
            }
            if (_hostSettings != null)
            {
                _hostSettings.OnDisable();
            }
        }
        internal override void OnGUI(Rect screenRect)
        {
            OnGUITabToolbar(screenRect);
        }

        protected override void SwitchTab()
        {
            switch (_tabType)
            {
                case SettingsTabType.HostSettings:
                    _currentBlock = _hostSettings;
                    break;
                case SettingsTabType.ManagerSettings:
                    _currentBlock = _managerSettings;
                    break;
            }
        }
    }
}

