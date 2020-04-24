using ABManagerEditor.BuildModels;
using ABManagerCore.Consts;
using UnityEngine;
using UnityEditor;

namespace ABManagerEditor.Browser.Blocks.Manager.LevelTemplate
{
    internal class LevelTemplatesTreeInfo : AbstractTreeInfoBlock<ABLevelTemplate, LevelTemplatesTreeView>
    {

        protected override void InitializeTreeView()
        {
            TreeView = new LevelTemplatesTreeView(TreeViewState);
        }
        protected override string OnNameTreeViewGUI()
        {
            return "LevelTemplates:";
        }
        protected override void OnPostTreeViewGUI()
        {
            GUILayout.Button("Create");
        }
        protected override void OnCurrentItmeIsSelectedGUI()
        {
            CurrentItem.Name = EditorGUILayout.TextField("Имя:", CurrentItem.Name);
            var sceneResourceCount = CurrentItem.SceneResource != null ? 1 : 0;
            var countResources = sceneResourceCount + CurrentItem.Resources.Count;
            EditorGUILayout.LabelField("Количество ресурсов:", countResources.ToString());
        }

    }
}

