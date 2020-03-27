using ABManagerEditor.Models;
using System;

namespace ABManagerEditor.Interfaces
{
    internal interface ISelectABGroup
    {
        event Action<ABGroup> OnSelectABGroup;
    }
}

