using UnityEngine;

namespace ABManagerEditor.BuildModels
{
    [System.Serializable]
    public class ABResourceDep : ABResourceBase
    {
        public ABResource Parent => _parent;
        [SerializeField]
        private ABResource _parent;

        public ABResourceDep(Object resourceObject, ABResource parent) : base(resourceObject)
        {
            _parent = parent;
        }
        protected override void CompleteUpdate()
        {
            //
        }
    }
}

