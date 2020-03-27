using UnityEditor;
using UnityEngine;

namespace ABManagerEditor.Models
{
    public enum TypeAsset
    {
        Default,
        Scene,
        Directory
    }
    [System.Serializable]
    public class ABAsset
    {
        #region Public Properties
        public string Name 
        { 
            get => _name;
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
            }
        }
        public string PathAsset { get => _pathAsset; }
        public GUID GUIDAsset { get => new GUID(_guidAsset); }
        public Object AssetObject { get => _assetObject; }
        public TypeAsset TypeAsset { get => _typeAsset; }
        #endregion

        [SerializeField]
        private string _name;
        [SerializeField]
        private string _pathAsset;
        [SerializeField]
        private Object _assetObject;
        [SerializeField]
        private TypeAsset _typeAsset = TypeAsset.Default;
        [SerializeField]
        private string _guidAsset;
        

        public ABAsset(Object assetObject)
        {
            ChangeObject(assetObject);
        }
        public void ChangeObject(Object assetObject)
        {
            if (assetObject == null)
            {
                throw new System.NullReferenceException("Вы передали в качестве ассета null");
            }
            _assetObject = assetObject;
            Update();
        }
        public void Update()
        {
            if (_assetObject is SceneAsset)
                _typeAsset = TypeAsset.Scene;
            else if (_assetObject is DefaultAsset)
                _typeAsset = TypeAsset.Directory;
            else
                _typeAsset = TypeAsset.Default;
            _pathAsset = AssetDatabase.GetAssetPath(_assetObject);
            _name = _pathAsset;
            _guidAsset = AssetDatabase.AssetPathToGUID(_pathAsset);
        }
    }
}

