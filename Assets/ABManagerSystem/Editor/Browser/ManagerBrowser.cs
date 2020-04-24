using UnityEngine;
using UnityEditor;
using ABManagerEditor.Controller;
using ABManagerCore.Consts;
using ABManagerEditor.Browser.Blocks;

namespace ABManagerEditor.Browser
{
    public class ManagerBrowser : EditorWindow
    {
        private MainBlock _mainBlock;

        [MenuItem(MenuItemPaths.ABManagerPath)]
        public static void ShowBrowser()
        {
            ManagerBrowser browser = GetWindow<ManagerBrowser>();
            browser.titleContent = new GUIContent(Names.ManagerName);
            browser.Show();
        }
        private void OnEnable()
        {
            if (_mainBlock == null)
                _mainBlock = new MainBlock();
            _mainBlock.OnEnable();
        }
        private void OnDisable()
        {
            if (_mainBlock != null)
                _mainBlock.OnDisable();
            ABController.Current.Saver.SaveAllSettings();
        }
        private void OnGUI()
        {
            Rect screenRect = new Rect(0, 0, position.width, position.height);
            _mainBlock.OnGUI(screenRect);
        }
    }
}

