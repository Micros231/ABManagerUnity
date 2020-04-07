using System;
using UnityEngine;

namespace ABManagerCore.Interfaces
{
    public interface IBundleInfo
    {
        string Name { get; }
        Guid GUIDName { get; }
        Hash128 Hash { get; }
    }
}

