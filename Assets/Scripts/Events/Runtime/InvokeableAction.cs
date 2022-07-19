// Author:  Joseph Crump
// Date:    07/18/22

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (method.IsStatic && target != null)
            {
                throw new ArgumentException($"{nameof(target)} must be null when " +
                    $"{nameof(method)} is static.");
            }
            
            if (!method.IsStatic && target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
        }

        /// <summary>
        /// Invoke the bound action using <paramref name="args"/>.
        /// </summary>
        /// <param name="args">
        /// Parameters to pass to the wrapped delegate.
        /// </param>
        public abstract void Invoke(object[] args);

        protected static void EnsureArgumentIsValid<T>(object arg)
        {
            if (arg != null && (arg is not T))
                throw new ArgumentException($"{nameof(arg)} (type {arg.GetType()})" +
                    $" is not assignable from {typeof(T)}.");
        }
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

        public InvokeableAction(UnityAction unityAction)
        {
            _action += unityAction;
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
            _action();
        }
    }

    /// <summary>
    /// Event wrapper for a <see cref="UnityAction{T0}"/>.
    /// </summary>
    /// <typeparam name="T0">
    /// Type of the first parameter passed to the wrapped delegate.
    /// </typeparam>
    class InvokeableAction<T0> : InvokeableActionBase
    {
        private event UnityAction<T0> _action;
        private const int _numberOfArgs = 1;

        public InvokeableAction(object target, MethodInfo method) : base(target, method)
        {
            _action += (UnityAction<T0>)Delegate.CreateDelegate(typeof(UnityAction<T0>), target, method);
        }

        public InvokeableAction(UnityAction<T0> unityAction)
        {
            _action += unityAction;
        }

        /// <summary>
        /// Invoke the bound unity action.
        /// </summary>
        /// <param name="arg0">
        /// First argument passed to the wrapped delegate.
        /// </param>
        public void Invoke(T0 arg0)
        {
            _action.Invoke(arg0);
        }

        public override void Invoke(object[] args)
        {
            if (args.Length != _numberOfArgs)
                throw new ArgumentException($"{nameof(args)} array should have " +
                    $"{_numberOfArgs} values but has {args.Length} instead.");

            EnsureArgumentIsValid<T0>(args[0]);

            _action.Invoke((T0)args[0]);
        }
    }

    /// <summary>
    /// Event wrapper for a <see cref="UnityAction{T0, T1}"/>.
    /// </summary>
    /// <typeparam name="T0">
    /// Type of the first parameter passed to the wrapped delegate.
    /// </typeparam>
    /// <typeparam name="T1">
    /// Type of the second parameter passed to the wrapped delegate.
    /// </typeparam>
    class InvokeableAction<T0, T1> : InvokeableActionBase
    {
        private event UnityAction<T0, T1> _action;
        private const int _numberOfArgs = 2;

        public InvokeableAction(object target, MethodInfo method) : base(target, method)
        {
            _action += (UnityAction<T0, T1>)Delegate.CreateDelegate(typeof(UnityAction<T0, T1>), target, method);
        }

        public InvokeableAction(UnityAction<T0, T1> unityAction)
        {
            _action += unityAction;
        }

        /// <summary>
        /// Invoke the bound unity action.
        /// </summary>
        /// <param name="arg0">
        /// First argument passed to the wrapped delegate.
        /// </param>
        /// <param name="arg1">
        /// Second argument passed to the wrapped delegate.
        /// </param>
        public void Invoke(T0 arg0, T1 arg1)
        {
            _action?.Invoke(arg0, arg1);
        }

        public override void Invoke(object[] args)
        {
            if (args.Length != _numberOfArgs)
                throw new ArgumentException($"{nameof(args)} array should have " +
                    $"{_numberOfArgs} values but has {args.Length} instead.");

            EnsureArgumentIsValid<T0>(args[0]);

            _action?.Invoke((T0)args[0], (T1)args[1]);
        }
    }

    [Serializable]
    class SerializedAction
    {
        [SerializeField, SerializeReference]
        private object _target;

        [SerializeField]
        private string _methodName;

        [SerializeField]
        private string _targetAssemblyName;

        [SerializeField, SerializeReference]
        private ISerializedArgument[] _arguments;

        /// <summary>
        /// Find the runtime delegate using the serialized data.
        /// </summary>
        public InvokeableActionBase GetRuntimeAction()
        {
            if (_target == null)
                return null;

            var types = _arguments.Select(arg => arg.Type).ToArray();
            var methodInfo = UnityEventBase.GetValidMethodInfo(_target, _methodName, types);

            if (_arguments.Length == 0)
                return new InvokeableAction(_target, methodInfo);
            else
                return MakeGenericAction(types, _target, _methodName);
        }

        private static InvokeableActionBase MakeGenericAction(Type[] typeArgs, object target, string methodName)
        {
            Type genericType = null;

            switch (typeArgs.Length)
            {
                case 1:
                    genericType = typeof(InvokeableAction<>);
                    break;
                case 2:
                    genericType = typeof(InvokeableAction<,>);
                    break;

                default:
                    break;
            }

            return Activator.CreateInstance(
                type: genericType.MakeGenericType(typeArgs), 
                args: new object[] { target, methodName }) as InvokeableActionBase;
        }
    }
}
