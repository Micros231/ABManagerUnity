using ABManagerCore.Consts;
using System;
using UnityEngine;

namespace ABManagerEditor.Browser.Blocks
{
    internal abstract class AbstractTabBlock<TEnum> : AbstractBlock
        where TEnum : struct, Enum
    {
        protected TEnum _tabType;
        protected AbstractBlock _currentBlock;

        protected void OnGUITabToolbar(Rect screenRect)
        {
            GUILayout.BeginHorizontal(GUILayout.Height(Sizes.Heights.height_15));
            PreviousGUITab();
            GUILayout.Space(Sizes.Spaces.space_15);
            var genericTypeTab = _tabType.GetType();
            if (!genericTypeTab.IsEnum)
            {
                throw new InvalidCastException("TabType is not eunm");
            }
            _tabType = (TEnum)(object)GUILayout.Toolbar((int)(object)_tabType, Enum.GetNames(genericTypeTab));
            GUILayout.Space(Sizes.Spaces.space_15);
            PostGUITab();
            GUILayout.EndHorizontal();
            GUILayout.Space(Sizes.Spaces.space_15);

            UpdateCurrentBlock(screenRect);
        }
        protected abstract void SwitchTab();
        protected virtual void PreviousGUITab()
        {

        }
        protected virtual void PostGUITab()
        {

        }
        private void UpdateCurrentBlock(Rect screenRect)
        {
            float positionYCurrentBlock = screenRect.y + Sizes.Spaces.space_30;
            float sizeHeightCurrentBlock = screenRect.height - positionYCurrentBlock;
            Vector2 positionCurrentBlock = new Vector2(screenRect.x, positionYCurrentBlock);
            Vector2 sizeCurrentBlock = new Vector2(screenRect.width, sizeHeightCurrentBlock);
            Rect currentRect = new Rect(positionCurrentBlock, sizeCurrentBlock);
            SwitchTab();
            if (_currentBlock == null)
            {
                var labelStyle = new GUIStyle();
                labelStyle.alignment = TextAnchor.MiddleCenter;
                labelStyle.normal.textColor = Color.red;
                labelStyle.fontStyle = FontStyle.Bold;
                labelStyle.fontSize = Sizes.FontSizes.size_14;
                GUI.Label(currentRect, Messages.GUIMessages.BlockNull, labelStyle);
                return;
            }
            _currentBlock.OnGUI(currentRect);
        }

    }
}

