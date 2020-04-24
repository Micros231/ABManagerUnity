using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ABManagerEditor.BuildModels
{
    [Serializable]
    public class ABResource : ABResourceBase
    {
        public IReadOnlyList<ABResourceDep> DependenciesResources => _dependenciesResources;

        [SerializeField]
        private List<ABResourceDep> _dependenciesResources = new List<ABResourceDep>();

        public ABResource(UnityEngine.Object resourceObject) : base(resourceObject)
        {

        }
        private void UpdateDependencies()
        {
            var dependencies = new List<ABResourceDep>();
            var pathDependencies = AssetDatabase.GetDependencies(_resourcePath, true);
            foreach (var pathDependency in pathDependencies)
            {
                var depObj = AssetDatabase.LoadMainAssetAtPath(pathDependency);
                if (depObj != null && depObj != ResourceObject)
                {
                    var depResource = new ABResourceDep(depObj, this);
                    dependencies.Add(depResource);
                    Debug.Log(depResource.Path);
                }
            }
            _dependenciesResources = dependencies;
        }
        protected override void CompleteUpdate()
        {
            UpdateDependencies();
        }
    }

}

