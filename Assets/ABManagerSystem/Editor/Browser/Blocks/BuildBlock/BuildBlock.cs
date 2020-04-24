using ABManagerCore.Consts;
using ABManagerEditor.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerEditor.Browser.Blocks.Build
{
    internal class BuildBlock : AbstractBlock
    {
        internal override void OnGUI(Rect screenRect)
        {
            var labelStyle = new GUIStyle();
            labelStyle.alignment = TextAnchor.MiddleCenter;
            if (ABController.Current.ManagerSettings == null)
            {
                GUILayout.Label(Messages.GUIMessages.ManagerSettingsNull, labelStyle);
                return;
            }
            if (ABController.Current.HostSettings == null)
            {
                GUILayout.Label(Messages.GUIMessages.HostSettingsNull, labelStyle);
                return;
            }
        }
    }
}

