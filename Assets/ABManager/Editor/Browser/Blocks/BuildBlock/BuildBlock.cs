using ABManagerEditor.Controller;
using ABManagerEditor.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerEditor.Browser.Blocks
{
    internal class BuildBlock : AbstractBlock
    {
        private readonly ABManagerController _controller;

        internal BuildBlock(ABManagerController controller)
        {
            _controller = controller;
        }
        internal override void OnGUI(Rect position)
        {
            GUILayout.Label("Билдинг", new GUIStyle { alignment = TextAnchor.MiddleCenter });
            if (GUILayout.Button("Сбилдить все не кастомные группы"))
            {
                _controller.Builder.BuildAll();
            }
            
        }
    }
}

