using System;
using UnityEditor;
using UnityEngine;

namespace ABManagerEditor.Controller
{
    internal class ABController : ABControllerAbstract
    {
        public static ABController Current => ControllerNested.instance;
        internal ABBuilder Builder { get; }
        internal ABCreators Creators { get; }
        internal ABSaver Saver { get; }
        internal ABController()
        {
            Builder = new ABBuilder();
            Creators = new ABCreators();
            Saver = new ABSaver();
        }

        private class ControllerNested
        {
            static ControllerNested() { }
            internal static readonly ABController instance = new ABController();
        }
    }
}

