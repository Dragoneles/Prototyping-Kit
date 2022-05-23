// Author:  Joseph Crump
// Date:    05/22/22

using System;
using UnityEngine;
using UnityEditor;
using MenuFunction = UnityEditor.GenericMenu.MenuFunction;

namespace JC.Prototyping.Editor
{
    /// <summary>
    /// Custom editor for the <see cref="UnityMessageTriggers"/> component.
    /// </summary>
    [CustomEditor(typeof(UnityMessageTriggers))]
    public class UnityMessageTriggersEditor : UnityEditor.Editor
    {
        UnityMessageTriggers component => target as UnityMessageTriggers;

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            if (AddOrRemoveButton())
                ShowAddOrRemoveContextMenu();

            EditorGUILayout.Space();
            ShowUsedUnityEvents();

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        private void ShowUsedUnityEvents()
        {
            foreach (var item in component.UsedMessageTypes)
            {
                ShowUnityEventForMessage(item);
            }
        }

        private void ShowUnityEventForMessage(UnityMessageType messageType)
        {
            string propertyName = messageType.ToString().ToLowerFirstChar();
            var property = serializedObject.FindProperty(propertyName);

            UnityEventProperty(property);
        }

        private bool AddOrRemoveButton()
        {
            var label = new GUIContent("Add/Remove Message");
            return GUILayout.Button(label, GUILayout.Height(30f));
        }

        private void ShowAddOrRemoveContextMenu()
        {
            var menu = new GenericMenu();

            string submenu = string.Empty;
            var enumType = typeof(UnityMessageType);
            foreach (var messageName in Enum.GetNames(enumType))
            {
                var message = Enum.Parse<UnityMessageType>(messageName);

                UpdateSubmenu(message, ref submenu);

                var label = new GUIContent(text: $"{submenu}/{message}");
                bool messageIsUsed = component.UsedMessageTypes.Contains(message);

                menu.AddItem(label, messageIsUsed, GetMenuFunction(message, messageIsUsed));
            }

            menu.AddSeparator(string.Empty);

            AddClearAllItemToMenu(menu);

            menu.ShowAsContext();
        }

        private MenuFunction GetMenuFunction(UnityMessageType messageType, bool messageIsUsed)
        {
            return (messageIsUsed) 
                ? () => RemoveMessage(messageType) 
                : () => AddMessage(messageType);
        }

        private void AddClearAllItemToMenu(GenericMenu menu)
        {
            var clearContent = new GUIContent("Clear All", "Clear all messages");
            menu.AddItem(clearContent, false, ClearUsedMessageTypes);
        }

        private void AddMessage(UnityMessageType messageType)
        {
            Undo.RecordObject(target, $"Add [{messageType}] Unity Event");
            component.UsedMessageTypes.Add(messageType);
        }

        private void RemoveMessage(UnityMessageType messageType)
        {
            Undo.RecordObject(target, $"Remove [{messageType}] Unity Event");
            component.UsedMessageTypes.Remove(messageType);
        }

        private void ClearUsedMessageTypes()
        {
            Undo.RecordObject(target, $"Clear Unity Messages");
            component.UsedMessageTypes.Clear();
        }

        private static void UnityEventProperty(SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property, includeChildren: true);
        }

        private void UpdateSubmenu(UnityMessageType message, ref string subMenu)
        {
            switch (message)
            {
                case UnityMessageType.Awake:
                    subMenu = "Lifecycle";
                    break;
                case UnityMessageType.Start:
                    break;
                case UnityMessageType.Update:
                    break;
                case UnityMessageType.FixedUpdate:
                    break;
                case UnityMessageType.LateUpdate:
                    break;
                case UnityMessageType.OnEnable:
                    break;
                case UnityMessageType.OnDisable:
                    break;
                case UnityMessageType.OnDestroy:
                    break;
                case UnityMessageType.OnBecameVisible:
                    subMenu = "Visibility";
                    break;
                case UnityMessageType.OnBecameInvisible:
                    break;
                case UnityMessageType.OnApplicationFocus:
                    subMenu = "Application";
                    break;
                case UnityMessageType.OnApplicationPause:
                    break;
                case UnityMessageType.OnApplicationQuit:
                    break;
                case UnityMessageType.OnCollisionEnter:
                    subMenu = "3D Collision";
                    break;
                case UnityMessageType.OnCollisionStay:
                    break;
                case UnityMessageType.OnCollisionExit:
                    break;
                case UnityMessageType.OnTriggerEnter:
                    break;
                case UnityMessageType.OnTriggerStay:
                    break;
                case UnityMessageType.OnTriggerExit:
                    break;
                case UnityMessageType.OnCollisionEnter2D:
                    subMenu = "2D Collision";
                    break;
                case UnityMessageType.OnCollisionStay2D:
                    break;
                case UnityMessageType.OnCollisionExit2D:
                    break;
                case UnityMessageType.OnTriggerEnter2D:
                    break;
                case UnityMessageType.OnTriggerStay2D:
                    break;
                case UnityMessageType.OnTriggerExit2D:
                    break;
                case UnityMessageType.OnMouseDown:
                    subMenu = "Mouse";
                    break;
                case UnityMessageType.OnMouseDrag:
                    break;
                case UnityMessageType.OnMouseEnter:
                    break;
                case UnityMessageType.OnMouseExit:
                    break;
                case UnityMessageType.OnMouseOver:
                    break;
                case UnityMessageType.OnMouseUp:
                    break;
                case UnityMessageType.OnMouseUpAsButton:
                    break;
                case UnityMessageType.OnTransformParentChanged:
                    subMenu = "Hierarchy";
                    break;
                case UnityMessageType.OnTransformChildrenChanged:
                    break;
                default:
                    break;
            }
        }
    }
}
