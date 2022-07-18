using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.Events
{
    public class TestBehavior : MonoBehaviour
    {
        [SerializeField]
        private SerializedArgument<float> _testFloat;

        [SerializeField]
        private SerializedArgument<Color> _testColor;
    }
}
