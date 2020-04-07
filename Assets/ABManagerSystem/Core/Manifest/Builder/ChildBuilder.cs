using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABManagerCore.Interfaces;

namespace ABManagerCore.Manifest.Builder
{
    public abstract class ChildBuilder<TParent, TParentElement> : IChildBuilder<TParent> 
    {
        protected readonly TParent _parentBuilder;
        protected readonly TParentElement _parentElement;

        public ChildBuilder(TParent parentBuilder, TParentElement parentElement)
        {
            _parentBuilder = parentBuilder;
            _parentElement = parentElement;
        }

        public abstract TParent Complete();
        
    }
}

