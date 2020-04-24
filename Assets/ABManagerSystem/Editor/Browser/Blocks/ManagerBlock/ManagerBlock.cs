using ABManagerEditor.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ABManagerCore.Consts;
using ABManagerCore.Enums;
using ABManagerEditor.Controller;
using ABManagerEditor.Browser.Blocks.Manager.LevelTemplate;


namespace ABManagerEditor.Browser.Blocks.Manager
{
    internal class ManagerBlock : AbstractTabBlock<TemplateTabType>
    {
        private AbstractBlock _levelTemplatesBlock, _standTemplatesBlock, _instanceTemplatesBlock;

        internal override void OnEnable()
        {
            if (_levelTemplatesBlock == null)
            {
                _levelTemplatesBlock = new LevelTemplatesBlock();
            }
            _levelTemplatesBlock.OnEnable();
            //_standTemplatesBlock.OnEnable();
            //_instanceTemplatesBlock.OnEnable();
        }
        internal override void OnDisable()
        {
            if (_levelTemplatesBlock != null)
            {
                _levelTemplatesBlock.OnDisable();
            }
            if (_standTemplatesBlock != null)
            {
                _standTemplatesBlock.OnDisable();
            }
            if (_instanceTemplatesBlock != null)
            {
                _instanceTemplatesBlock.OnDisable();
            }
        }
        internal override void OnGUI(Rect screenRect)
        {
            if (ABController.Current.ManagerSettings == null)
            {
                var labelStyle = new GUIStyle();
                labelStyle.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label(Messages.GUIMessages.ManagerSettingsNull, labelStyle);
                return;
            }
            else
            {
                OnGUITabToolbar(screenRect);
            }
        }
        protected override void SwitchTab()
        {
            switch (_tabType)
            {
                case TemplateTabType.LevelTemplates:
                    _currentBlock = _levelTemplatesBlock;
                    break;
                case TemplateTabType.StandTemplates:
                    _currentBlock = _standTemplatesBlock;
                    break;
                case TemplateTabType.InstanceTemaplates:
                    _currentBlock = _instanceTemplatesBlock;
                    break;
            }
        }
    }
}

