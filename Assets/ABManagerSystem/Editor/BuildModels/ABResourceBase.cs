using System;
using UnityEditor;
using UnityEngine;
using ABManagerCore.Enums;
using ABManagerCore.Interfaces;

namespace ABManagerEditor.BuildModels
{
    [Serializable]
    public abstract class ABResourceBase : IResourceInfo, IEquatable<ABResourceBase>
    {
        public string Name
        {
            get => _resourceName;
            internal set => _resourceName = value;
        }
        public string Path => _resourcePath;
        public GUID ResourceGUID => new GUID(_resourceGUIDString);
        public UnityEngine.Object ResourceObject => _resourceObject;
        public ResourceType ResourceType => _resourceType;

        [SerializeField]
        protected string _resourceName;
        [SerializeField]
        protected string _resourcePath;
        [SerializeField]
        protected string _resourceGUIDString;
        [SerializeField]
        protected UnityEngine.Object _resourceObject;
        [SerializeField]
        protected ResourceType _resourceType;

        public ABResourceBase(UnityEngine.Object resourceObject)
        {
            if (resourceObject == null)
            {
                throw new NullReferenceException("Вы передали в качестве ресурса null");
            }
            _resourceObject = resourceObject;
            Update();
            _resourceName = _resourcePath;
        }
        public void Update()
        {
            if (CheckResourceIsNotValid())
            {
                UpdateResource();
            }
            CompleteUpdate();
        }
        protected abstract void CompleteUpdate();
        private void UpdateResource()
        {
            if (_resourceObject is SceneAsset)
            {
                _resourceType = ResourceType.Scene;
            }
            else
            {
                _resourceType = ResourceType.Asset;
            }
            _resourcePath = AssetDatabase.GetAssetPath(_resourceObject);
            _resourceGUIDString = AssetDatabase.AssetPathToGUID(_resourcePath);
        }
        private bool CheckResourceIsNotValid()
        {
            if (_resourceObject == null)
            {
                return true;
            }
            var currentPath = AssetDatabase.GetAssetPath(_resourceObject);
            var currentGUID = AssetDatabase.AssetPathToGUID(currentPath);
            if (currentPath == _resourcePath)
            {
                return false;
            }
            if (currentGUID == _resourceGUIDString)
            {
                return false;
            }
            return true;
        }

        public virtual bool Equals(ABResourceBase other)
        {
            if (other == null)
            {
                return false;
            }
            return Path == other.Path &&
                ResourceGUID == other.ResourceGUID &&
                ResourceObject == other.ResourceObject ? true : false;
        }
    }
}

