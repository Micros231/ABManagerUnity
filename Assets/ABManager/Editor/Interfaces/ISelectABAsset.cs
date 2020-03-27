using ABManagerEditor.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerEditor.Interfaces
{
    internal interface ISelectABAsset
    {
        event Action<ABAsset> OnSelectABAsset;
    }
}

