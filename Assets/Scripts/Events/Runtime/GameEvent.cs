// Author:  Joseph Crump
// Date:    07/24/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JC.Events
{
    /// <summary>
    /// Class wrapping an event that can be broadcasted via the 
    /// <see cref="EventBus"/>.
    /// </summary>
    /// <typeparam name="TArgs">
    /// Type of arguments passed with the event invocation.
    /// </typeparam>
    public class GameEvent<TArgs> : IGameEvent
    {
        private readonly UnityEvent<TArgs> _event = new();

        /// <summary>
        /// Trigger the event, passing <paramref name="args"/> to all 
        /// listeners.
        /// </summary>
        /// <param name="args">
        /// The arguments passed with the event.
        /// </param>
        internal void Invoke(TArgs args)
        {
            _event.Invoke(args);
        }

        /// <summary>
        /// Bind a callback to the event.
        /// </summary>
        /// <param name="callback">
        /// The callback to bind to the event.
        /// </param>
        internal void Subscribe(UnityAction<TArgs> callback)
        {
            _event.AddListener(callback);
        }

        /// <summary>
        /// Unbind a callback from the event.
        /// </summary>
        /// <param name="callback">
        /// The callback to unbind from the event.
        /// </param>
        internal void Unsubscribe(UnityAction<TArgs> callback)
        {
            _event.RemoveListener(callback);
        }
    }

    /// <summary>
    /// Class wrapping an event that can be broadcasted via the 
    /// <see cref="EventBus"/>.
    /// </summary>
    /// <typeparam name="TSender">
    /// Type of object responsible for raising the event.
    /// </typeparam>
    /// <typeparam name="TArgs">
    /// Type of arguments passed with the event invocation.
    /// </typeparam>
    public class GameEvent<TSender, TArgs> : GameEvent<TArgs>, IGameEvent
    {
        private readonly UnityEvent<TSender, TArgs> _eventWithSender = new();
        private readonly Dictionary<TSender, GameEvent<TArgs>> _eventDictionary = new();

        /// <summary>
        /// Bind a callback to the event that will receive messages from any
        /// sender.
        /// </summary>
        /// <param name="callback">
        /// The callback to bind to the event.
        /// </param>
        internal void Subscribe(UnityAction<TSender, TArgs> callback)
        {
            _eventWithSender.AddListener(callback);
        }

        /// <summary>
        /// Bind a callback to the event that will only receive messages from
        /// the specified <paramref name="sender"/>.
        /// </summary>
        /// <param name="sender">
        /// The object responsible for raising the event.
        /// </param>
        /// <param name="callback">
        /// The callback to bind to the event.
        /// </param>
        internal void Subscribe(UnityAction<TArgs> callback, TSender sender)
        {
            var senderSpecifiedEvent = GetOrAddEvent(sender);
            senderSpecifiedEvent.Subscribe(callback);
        }

        /// <summary>
        /// Unbind a callback from the event.
        /// </summary>
        /// <param name="callback">
        /// The callback to unbind from the event.
        /// </param>
        internal void Unsubscribe(UnityAction<TSender, TArgs> callback)
        {
            _eventWithSender.RemoveListener(callback);
        }

        /// <summary>
        /// Unbind a callback from a specific event sender.
        /// </summary>
        /// <param name="sender">
        /// The object being observed by the callback target.
        /// </param>
        /// <param name="callback">
        /// The callback to unbind from the event.
        /// </param>
        internal void Unsubscribe(UnityAction<TArgs> callback, TSender sender)
        {
            if (!_eventDictionary.ContainsKey(sender))
                return;

            _eventDictionary[sender].Unsubscribe(callback);
        }

        /// <summary>
        /// Trigger the event, passing <paramref name="args"/> to all 
        /// listeners.
        /// </summary>
        /// <param name="sender">
        /// The object responsible for raising the event.
        /// </param>
        /// <param name="args">
        /// The arguments passed with the event.
        /// </param>
        internal void Invoke(TSender sender, TArgs args)
        {
            _eventWithSender.Invoke(sender, args);
            
            if (_eventDictionary.ContainsKey(sender))
                _eventDictionary[sender].Invoke(args);
        }

        private GameEvent<TArgs> GetOrAddEvent(TSender key)
        {
            if (!_eventDictionary.ContainsKey(key))
            {
                _eventDictionary.Add(key, new GameEvent<TArgs>());
            }

            return _eventDictionary[key];
        }
    }
}
