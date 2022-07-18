// Author:  Joseph Crump
// Date:    07/18/22

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

#if UNITY_EDITOR
[assembly: InternalsVisibleTo("JC.Events.Editor")]
#endif

namespace JC.Events
{
    [Serializable]
    internal class SerializedArgument<T>
    {
        [SerializeField, SerializeReference]
        protected T _value;
    }
}
