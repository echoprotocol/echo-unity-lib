using System;
using CustomTools.Attributes;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Enum;
using CustomTools.Extensions.Core.LayerMask;
using UnityEngine;


namespace CustomTools.TriggerBehaviour {

    [RequireComponent( typeof( Collider ) )]
    public abstract class TriggerMonoBehaviour<T> : MonoBehaviour {

        Collider cachedCollider;


        public Collider CachedCollider {
            get {
                if ( cachedCollider.IsNull() ) {
                    cachedCollider = GetComponent<Collider>();
                }
                return cachedCollider;
            }
        }

        protected virtual void Awake() {
            CachedCollider.isTrigger = true;
        }

        protected abstract void TriggerEnter( T target );
        protected abstract void TriggerStay( T target );
        protected abstract void TriggerExit( T target );
    }


    [RequireComponent( typeof( Collider ) )]
    public abstract class TriggerMaskMonoBehaviour : TriggerMonoBehaviour<GameObject>  {

        [SerializeField] protected LayerMask triggerMask;


        void OnTriggerEnter( Collider other ) {
            if ( triggerMask.Contains( other.gameObject.layer ) ) {
                TriggerEnter( other.gameObject );
            }
        }

        void OnTriggerStay( Collider other ) {
            if ( triggerMask.Contains( other.gameObject.layer ) ) {
                TriggerStay( other.gameObject );
            }
        }

        void OnTriggerExit( Collider other ) {
            if ( triggerMask.Contains( other.gameObject.layer ) ) {
                TriggerExit( other.gameObject );
            }
        }
    }


    [RequireComponent( typeof( Collider ) )]
    public abstract class TriggerMaskMonoBehaviour<T> : TriggerMonoBehaviour<T> where T : class {

        [Flags]
        public enum FindIn {

            Parent = 1,
            Self = 1 << 1,
            Childs = 1 << 2
        }


        [SerializeField] protected LayerMask triggerMask;
        [SerializeField, EnumFlags] protected FindIn findMask = 0;


        public virtual FindIn FindMask {
            get { return findMask; }
        }

        protected T FindTarget( GameObject source ) {
            if ( FindMask.Contains( FindIn.Parent ) ) {
                var targetInParent = source.GetComponentInParent<T>();
                if ( targetInParent != null ) {
                    return targetInParent;
                }
            }
            if ( FindMask.Contains( FindIn.Self ) ) {
                var targetInSelf = source.GetComponent<T>();
                if ( targetInSelf != null ) {
                    return targetInSelf;
                }
            }
            if ( FindMask.Contains( FindIn.Childs ) ) {
                var targetInChild = source.GetComponentInChildren<T>();
                if ( targetInChild != null ) {
                    return targetInChild;
                }
            }
            return null;
        }

        void OnTriggerEnter( Collider other ) {
            if ( triggerMask.Contains( other.gameObject.layer ) ) {
                TriggerEnter( FindTarget( other.gameObject ) );
            }
        }

        void OnTriggerStay( Collider other ) {
            if ( triggerMask.Contains( other.gameObject.layer ) ) {
                TriggerStay( FindTarget( other.gameObject ) );
            }
        }

        void OnTriggerExit( Collider other ) {
            if ( triggerMask.Contains( other.gameObject.layer ) ) {
                TriggerExit( FindTarget( other.gameObject ) );
            }
        }
    }
}