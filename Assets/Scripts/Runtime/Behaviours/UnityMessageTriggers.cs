// Author:  Joseph Crump
// Date:    05/22/22

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace JC.Prototyping
{
    /// <summary>
    /// Enumerator of UnityMessage types.
    /// </summary>
    public enum UnityMessageType
    {
        // Lifecycle
        Awake,
        Start,
        Update,
        FixedUpdate,
        LateUpdate,
        OnEnable,
        OnDisable,
        OnDestroy,

        // Collision (3D)
        OnCollisionEnter,
        OnCollisionStay,
        OnCollisionExit,
        OnTriggerEnter,
        OnTriggerStay,
        OnTriggerExit,

        // Collision (2D)
        OnCollisionEnter2D,
        OnCollisionStay2D,
        OnCollisionExit2D,
        OnTriggerEnter2D,
        OnTriggerStay2D,
        OnTriggerExit2D,

        // Mouse
        OnMouseDown,
        OnMouseDrag,
        OnMouseEnter,
        OnMouseExit,
        OnMouseOver,
        OnMouseUp,
        OnMouseUpAsButton,

        // Visibility
        OnBecameVisible,
        OnBecameInvisible,

        // Hierarchy
        OnTransformParentChanged,
        OnBeforeTransformParentChanged,
        OnTransformChildrenChanged,

        // Application
        OnApplicationFocus,
        OnApplicationPause,
        OnApplicationQuit,
    }

    /// <summary>
    /// Component that dispatches UnityEvents that correspond
    /// to the built-in Unity messages.
    /// </summary>
    public class UnityMessageTriggers : MonoBehaviour
    {
        [HideInInspector]
        public List<UnityMessageType> UsedMessageTypes = new();

        // A hashset is constructed on awake and used to index message types
        // because it's much more efficient than checking the serialized list
        private HashSet<UnityMessageType> usedMessageTypes = new();

        #region UnityEvents
        [SerializeField] private UnityEvent awake = new();
        [SerializeField] private UnityEvent start = new();
        [SerializeField] private UnityEvent update = new();
        [SerializeField] private UnityEvent fixedUpdate = new();
        [SerializeField] private UnityEvent lateUpdate = new();
        [SerializeField] private UnityEvent onEnable = new();
        [SerializeField] private UnityEvent onDisable = new();
        [SerializeField] private UnityEvent onDestroy = new();

        [SerializeField] private UnityEvent onBecameVisible = new();
        [SerializeField] private UnityEvent onBecameInvisible = new();

        [SerializeField] private UnityEvent<bool> onApplicationFocus = new();
        [SerializeField] private UnityEvent<bool> onApplicationPause = new();
        [SerializeField] private UnityEvent onApplicationQuit = new();

        [SerializeField] private UnityEvent<Collision> onCollisionEnter = new();
        [SerializeField] private UnityEvent<Collision> onCollisionStay = new();
        [SerializeField] private UnityEvent<Collision> onCollisionExit = new();
        [SerializeField] private UnityEvent<Collider> onTriggerEnter = new();
        [SerializeField] private UnityEvent<Collider> onTriggerStay = new();
        [SerializeField] private UnityEvent<Collider> onTriggerExit = new();

        [SerializeField] private UnityEvent<Collision2D> onCollisionEnter2D = new();
        [SerializeField] private UnityEvent<Collision2D> onCollisionStay2D = new();
        [SerializeField] private UnityEvent<Collision2D> onCollisionExit2D = new();
        [SerializeField] private UnityEvent<Collider2D> onTriggerEnter2D = new();
        [SerializeField] private UnityEvent<Collider2D> onTriggerStay2D = new();
        [SerializeField] private UnityEvent<Collider2D> onTriggerExit2D = new();

        [SerializeField] private UnityEvent onMouseDown = new();
        [SerializeField] private UnityEvent onMouseDrag = new();
        [SerializeField] private UnityEvent onMouseEnter = new();
        [SerializeField] private UnityEvent onMouseExit = new();
        [SerializeField] private UnityEvent onMouseOver = new();
        [SerializeField] private UnityEvent onMouseUp = new();
        [SerializeField] private UnityEvent onMouseUpAsButton = new();

        [SerializeField] private UnityEvent onTransformParentChanged = new();
        [SerializeField] private UnityEvent onBeforeTransformParentChanged = new();
        [SerializeField] private UnityEvent onTransformChildrenChanged = new();
        #endregion

        #region Lifecycle Messages
        private void Awake()
        {
            usedMessageTypes = UsedMessageTypes.ToHashSet();

            if (usedMessageTypes.Contains(UnityMessageType.Awake)) 
                awake.Invoke();
        }

        private void Start()
        {
            if (usedMessageTypes.Contains(UnityMessageType.Start))
                start.Invoke();
        }

        private void Update()
        {
            if (usedMessageTypes.Contains(UnityMessageType.Update))
                update.Invoke();
        }

        private void FixedUpdate()
        {
            if (usedMessageTypes.Contains(UnityMessageType.FixedUpdate))
                fixedUpdate.Invoke();
        }

        private void LateUpdate()
        {
            if (usedMessageTypes.Contains(UnityMessageType.LateUpdate))
                lateUpdate.Invoke();
        }

        private void OnEnable()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnEnable))
                onEnable.Invoke();
        }

        private void OnDisable()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnDisable))
                onDisable.Invoke();
        }

        private void OnDestroy()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnDestroy))
                onDestroy.Invoke();
        }
        #endregion

        #region 2D Collision Messages
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnCollisionEnter2D))
                onCollisionEnter2D.Invoke(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnCollisionStay2D))
                onCollisionStay2D.Invoke(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnCollisionExit2D))
                onCollisionExit2D.Invoke(collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnTriggerEnter2D))
                onTriggerEnter2D.Invoke(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnTriggerStay2D))
                onTriggerStay2D.Invoke(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnTriggerExit2D))
                onTriggerExit2D.Invoke(collision);
        }
        #endregion

        #region 3D Collision Messages
        private void OnCollisionEnter(Collision collision)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnCollisionEnter))
                onCollisionEnter.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnCollisionStay))
                onCollisionStay.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnCollisionExit))
                onCollisionExit.Invoke(collision);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnTriggerEnter))
                onTriggerEnter.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnTriggerStay))
                onTriggerStay.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnTriggerExit))
                onTriggerExit.Invoke(other);
        }
        #endregion

        #region Mouse Messages
        private void OnMouseDown()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnMouseDown))
                onMouseDown.Invoke();
        }

        private void OnMouseDrag()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnMouseDrag))
                onMouseDrag.Invoke();
        }

        private void OnMouseEnter()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnMouseEnter))
                onMouseEnter.Invoke();
        }

        private void OnMouseExit()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnMouseExit))
                onMouseExit.Invoke();
        }

        private void OnMouseOver()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnMouseOver))
                onMouseOver.Invoke();
        }

        private void OnMouseUp()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnMouseUp))
                onMouseUp.Invoke();
        }

        private void OnMouseUpAsButton()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnMouseUpAsButton))
                onMouseUpAsButton.Invoke();
        }
        #endregion

        #region Visibility Messages
        private void OnBecameVisible()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnBecameVisible))
                onBecameVisible.Invoke();
        }

        private void OnBecameInvisible()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnBecameInvisible))
                onBecameInvisible.Invoke();
        }
        #endregion

        #region Hierarchy Messages
        private void OnTransformParentChanged()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnTransformParentChanged))
                onTransformParentChanged.Invoke();
        }

        private void OnBeforeTransformParentChanged()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnBeforeTransformParentChanged))
                onBeforeTransformParentChanged.Invoke();
        }

        private void OnTransformChildrenChanged()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnTransformChildrenChanged))
                onTransformChildrenChanged.Invoke();
        }
        #endregion

        #region Application Messages
        private void OnApplicationFocus(bool focus)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnApplicationFocus))
                onApplicationFocus.Invoke(focus);
        }

        private void OnApplicationPause(bool pause)
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnApplicationPause))
                onApplicationPause.Invoke(pause);
        }

        private void OnApplicationQuit()
        {
            if (usedMessageTypes.Contains(UnityMessageType.OnApplicationQuit))
                onApplicationQuit.Invoke();
        }
        #endregion
    }
}
