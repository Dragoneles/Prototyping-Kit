// Author:  Joseph Crump
// Date:    07/24/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JC.Events
{
    /// <summary>
    /// Static class responsible for sending or broadcasting 
    /// <see cref="IGameEvent"/>s.
    /// </summary>
    public static class EventBus
    {
        /// <summary>
        /// Raise a <see cref="GameEvent{TSender, TArgs}"/>, passing a sender
        /// for the event 
        /// </summary>
        /// <typeparam name="TSender">
        /// Object type of the sender.
        /// </typeparam>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be invoked.
        /// </param>
        /// <param name="sender">
        /// The object responsible for invoking the event.
        /// </param>
        /// <param name="args">
        /// The arguments passed along with the event invocation.
        /// </param>
        public static void Invoke<TSender, TArgs>(GameEvent<TSender, TArgs> gameEvent, 
            TSender sender, TArgs args)
        {
            gameEvent.Invoke(sender, args);
        }

        /// <summary>
        /// Raise a <see cref="GameEvent{TSender, TArgs}"/> without indicating
        /// a sender.
        /// </summary>
        /// <typeparam name="TSender">
        /// Object type of the sender.
        /// </typeparam>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be invoked.
        /// </param>
        /// <param name="args">
        /// The arguments passed along with the event invocation.
        /// </param>
        public static void Invoke<TSender, TArgs>(GameEvent<TSender, TArgs> gameEvent,
            TArgs args)
        {
            gameEvent.Invoke(args);
        }

        /// <summary>
        /// Raise a <see cref="GameEvent{TArgs}"/>.
        /// </summary>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be invoked.
        /// </param>
        /// <param name="args">
        /// The arguments passed along with the event invocation.
        /// </param>
        public static void Invoke<TArgs>(GameEvent<TArgs> gameEvent, TArgs args)
        {
            gameEvent.Invoke(args);
        }

        /// <summary>
        /// Add a callback to a game event.
        /// </summary>
        /// <typeparam name="TSender">
        /// Object type of the sender.
        /// </typeparam>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be subscribed to.
        /// </param>
        /// <param name="callback">
        /// The callback to add to the event.
        /// </param>
        public static void Subscribe<TSender, TArgs>(GameEvent<TSender, TArgs> gameEvent, 
            UnityAction<TSender, TArgs> callback)
        {
            gameEvent.Subscribe(callback);
        }

        /// <summary>
        /// Add a callback to a game event.
        /// </summary>
        /// <typeparam name="TSender">
        /// Object type of the sender.
        /// </typeparam>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be subscribed to.
        /// </param>
        /// <param name="callback">
        /// The callback to add to the event.
        /// </param>
        public static void Subscribe<TSender, TArgs>(GameEvent<TSender, TArgs> gameEvent, 
            UnityAction<TArgs> callback)
        {
            gameEvent.Subscribe(callback);
        }

        /// <summary>
        /// Add a callback to a game event from a specific sender.
        /// </summary>
        /// <typeparam name="TSender">
        /// Object type of the sender.
        /// </typeparam>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be subscribed to.
        /// </param>
        /// <param name="callback">
        /// The callback to add to the event.
        /// </param>
        public static void Subscribe<TSender, TArgs>(GameEvent<TSender, TArgs> gameEvent,
            UnityAction<TArgs> callback, TSender sender)
        {
            gameEvent.Subscribe(callback, sender);
        }

        /// <summary>
        /// Add a callback to a game event.
        /// </summary>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be unsubscribed from.
        /// </param>
        /// <param name="callback">
        /// The callback to remove from the event.
        /// </param>
        public static void Subscribe<TArgs>(GameEvent<TArgs> gameEvent,
            UnityAction<TArgs> callback)
        {
            gameEvent.Subscribe(callback);
        }

        /// <summary>
        /// Remove a callback from a game event.
        /// </summary>
        /// <typeparam name="TSender">
        /// Object type of the sender.
        /// </typeparam>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be unsubscribed from.
        /// </param>
        /// <param name="callback">
        /// The callback to remove from the event.
        /// </param>
        public static void Unsubscribe<TSender, TArgs>(GameEvent<TSender, TArgs> gameEvent,
            UnityAction<TSender, TArgs> callback)
        {
            gameEvent.Unsubscribe(callback);
        }

        /// <summary>
        /// Remove a callback from a game event.
        /// </summary>
        /// <typeparam name="TSender">
        /// Object type of the sender.
        /// </typeparam>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be unsubscribed from.
        /// </param>
        /// <param name="callback">
        /// The callback to remove from the event.
        /// </param>
        public static void Unsubscribe<TSender, TArgs>(GameEvent<TSender, TArgs> gameEvent,
            UnityAction<TArgs> callback)
        {
            gameEvent.Unsubscribe(callback);
        }

        /// <summary>
        /// Remove game event callbacks from a specific sender.
        /// </summary>
        /// <typeparam name="TSender">
        /// Object type of the sender.
        /// </typeparam>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be unsubscribed from.
        /// </param>
        /// <param name="callback">
        /// The callback to remove from the event.
        /// </param>
        public static void Unsubscribe<TSender, TArgs>(GameEvent<TSender, TArgs> gameEvent,
            UnityAction<TArgs> callback, TSender sender)
        {
            gameEvent.Unsubscribe(callback, sender);
        }

        /// <summary>
        /// Remove a callback from a game event.
        /// </summary>
        /// <typeparam name="TArgs">
        /// Object type of the event arguments.
        /// </typeparam>
        /// <param name="gameEvent">
        /// The event to be unsubscribed from.
        /// </param>
        /// <param name="callback">
        /// The callback to remove from the event.
        /// </param>
        public static void Unsubscribe<TArgs>(GameEvent<TArgs> gameEvent,
            UnityAction<TArgs> callback)
        {
            gameEvent.Unsubscribe(callback);
        }
    }
}
