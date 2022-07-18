// Author:  Joseph Crump
// Date:    07/18/22

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace JC.Events
{
    /// <summary>
    /// Base class for invokeable action bindings.
    /// </summary>
    internal abstract class InvokeableActionBase
    {
        protected InvokeableActionBase() { }

        protected InvokeableActionBase(object target, MethodInfo method)
        {

        }

        /// <summary>
        /// Invoke the bound action using <paramref name="args"/>.
        /// </summary>
        /// <param name="args">
        /// Parameters to pass to the bound action.
        /// </param>
        public abstract void Invoke(object[] args);
    }

    /// <summary>
    /// Event wrapper for a <see cref="UnityAction"/> with no arguments.
    /// </summary>
    class InvokeableAction : InvokeableActionBase
    {
        private event UnityAction _action;

        public InvokeableAction(object target, MethodInfo method) : base(target, method)
        {
            _action += (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), target, method);
        }

        public InvokeableAction(UnityAction action)
        {
            _action += action;
        }

        /// <summary>
        /// Invoke the bound unity action.
        /// </summary>
        public void Invoke()
        {
            _action();
        }

        public override void Invoke(object[] args)
        {
            
        }
    }
}
