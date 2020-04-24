using UnityEngine;

namespace ABManagerEditor.Browser.Blocks
{
    internal abstract class AbstractBlock 
    {
        internal AbstractBlock() { }
        internal virtual void OnEnable()
        {

        }
        internal virtual void OnDisable()
        {
        }
        internal abstract void OnGUI(Rect screenRect);
    }
}

