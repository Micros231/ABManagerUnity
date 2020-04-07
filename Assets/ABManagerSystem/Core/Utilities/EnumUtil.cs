using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerCore.Utilities
{
    public static class EnumUtil 
    {
        public static T ParseEnum<T>(string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}

