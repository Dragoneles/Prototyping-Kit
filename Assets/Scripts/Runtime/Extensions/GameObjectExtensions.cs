// Author:  Joseph Crump
// Date:    05/22/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.Prototyping
{
    /// <summary>
    /// Extension methods of <see cref="GameObject"/>s.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Get a component from the gameObject. If one doesn't exist, add one.
        /// </summary>
        /// <typeparam name="T">
        /// The type of component to get/add.
        /// </typeparam>
        /// <param name="searchChildren">
        /// Whether or not to include child gameObjects when trying to get the
        /// component.
        /// </param>
        /// <returns>
        /// An existing component of type <typeparamref name="T"/>. If one
        /// doesn't exist, returns a new component with default values.
        /// </returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject, 
            bool searchChildren = false) where T : Component
        {
            T component = searchChildren ? 
                gameObject.GetComponentInChildren<T>() : 
                gameObject.GetComponent<T>();

            if (component == null)
                component = gameObject.AddComponent<T>();

            return component;
        }
    }
}
