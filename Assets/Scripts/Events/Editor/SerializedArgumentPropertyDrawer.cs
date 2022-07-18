// Author:  Joseph Crump
// Date:    07/18/22

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JC.Events.Editor
{
    /// <summary>
    /// Custom property drawer for a <see cref="SerializedArgument{T}"/>
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializedArgument<>))]
    public class SerializedArgumentPropertyDrawer : PropertyDrawer
    {
        SerializedProperty _value;
        Type _valueType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _value = property.FindPropertyRelative("_value");
            _valueType = fieldInfo.FieldType.GenericTypeArguments[0];

            EditorGUI.PropertyField(position, _value, label);
        }
    }
}
