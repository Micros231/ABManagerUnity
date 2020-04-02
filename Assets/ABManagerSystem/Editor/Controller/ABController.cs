using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABManagerEditor.Controller
{
    public class ABController : ABControllerAbstract
    {
        public ABBuilder Builder { get; }

        public ABController()
        {
            Builder = new ABBuilder();
        }
    }
}

