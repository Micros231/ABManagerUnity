using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABManagerEditor.BuildModels;

namespace ABManagerEditor.Browser.Blocks.Manager.LevelTemplate
{
    internal class LevelTemplatesBlock : AbstractBlock
    {
        private AbstractTreeInfoBlock<ABLevelTemplate, LevelTemplatesTreeView> _levelTreeInfoBlock;

        internal override void OnEnable()
        {
            if (_levelTreeInfoBlock == null)
            {
                _levelTreeInfoBlock = new LevelTemplatesTreeInfo();
            }
            _levelTreeInfoBlock.OnEnable();
        }
        internal override void OnDisable()
        {
            if (_levelTreeInfoBlock != null)
            {
                _levelTreeInfoBlock.OnDisable();
            }
        }
        internal override void OnGUI(Rect screenRect)
        {
            _levelTreeInfoBlock.OnGUI(screenRect);
        }
    }
}

