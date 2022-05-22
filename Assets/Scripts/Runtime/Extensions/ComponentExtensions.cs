// Author:  Joseph Crump
// Date:    05/22/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.Prototyping
{
    /// <summary>
    /// Extension methods of <see cref="Component"/>s.
    /// </summary>
    public static class ComponentExtensions
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
        public static T GetOrAddComponent<T>(this Component component,
            bool searchChildren = false) where T : Component
        {
            return component.gameObject.GetOrAddComponent<T>(searchChildren);
        }

        /// <summary>
        /// Starts a coroutine via a <see cref="CoroutineRunner"/> on the 
        /// gameObject.
        /// </summary>
        public static Coroutine StartCoroutine(this Component component,
            IEnumerator enumerator)
        {
            var coroutineRunner = component.GetOrAddComponent<CoroutineRunner>();

            return coroutineRunner.StartCoroutine(enumerator);
        }

        /// <summary>
        /// Stop running a <see cref="Coroutine"/>.
        /// </summary>
        /// <param name="coroutine">
        /// The coroutine to stop running.
        /// </param>
        public static void StopCoroutine(this Component component,
            Coroutine coroutine)
        {
            var coroutineRunner = component.GetOrAddComponent<CoroutineRunner>();

            coroutineRunner.StopCoroutine(coroutine);
        }
    }
}
