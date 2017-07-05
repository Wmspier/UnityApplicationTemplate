using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.EventSystem
{
    public class EventSystem{
    
        #region Singleton
        private static EventSystem _instanceInternal = null;
        public static EventSystem Instance
        {
            get { return _instanceInternal ?? (_instanceInternal = new EventSystem()); }
        }
        #endregion

        public delegate void EventDelegate<in T>(T e) where T : IBaseEvent;
        private delegate void EventDelegate(IBaseEvent e); 

        private readonly Dictionary<Type, EventDelegate> _delegates = new Dictionary<System.Type, EventDelegate>();
        private readonly Dictionary<Delegate, EventDelegate> _delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

        private readonly Queue _eventQueue = new Queue();

        ~EventSystem()
        {
            _eventQueue.Clear();
            _delegates.Clear();
            _delegateLookup.Clear();
        }

        /// <summary>
        /// Connect an event type to a delegate.
        /// The delegate will be called when the event is dispatched
        /// </summary>
        /// <typeparam name="T">Type of event to be connected</typeparam>
        /// <param name="del">The event delegate</param>
        public void Connect<T>(EventDelegate<T> del) where T : IBaseEvent
        {
            // Early-out if we've already registered this delegate
            if (_delegateLookup.ContainsKey(del))
                return;

            // Create a new non-generic delegate which calls our generic one.
            // This is the delegate we actually invoke.
            EventDelegate internalDelegate = (e) => del((T)e);
            _delegateLookup[del] = internalDelegate;
            
            //Try to get delegate to combine otherwise add
            EventDelegate tempDel;
            _delegates.TryGetValue(typeof(T), out tempDel);
            _delegates[typeof(T)] = (EventDelegate) Delegate.Combine(internalDelegate,tempDel);
            
        }

        /// <summary>
        /// Disconnect an event type from a delegate.
        /// All events must be disconnected when its listener is destroyed
        /// </summary>
        /// <typeparam name="T">Type of event to be disconnected</typeparam>
        /// <param name="del">The event delegate</param>
        public void Disconnect<T>(EventDelegate<T> del) where T : IBaseEvent
        {
            EventDelegate internalDelegate;
            if (_delegateLookup.TryGetValue(del, out internalDelegate))
            {
                EventDelegate tempDel;
                if (_delegates.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                    {
                        _delegates.Remove(typeof(T));
                    }
                    else
                    {
                        _delegates[typeof(T)] = tempDel;
                    }
                }

                _delegateLookup.Remove(del);
            }
        }

        /// <summary>
        /// Dispatch an event. This will call any connected delegate.
        /// </summary>
        /// <param name="e"></param>
        public void Dispatch(BaseEvent e)
        {
            EventDelegate del;
            if (_delegates.TryGetValue(e.GetType(), out del))
            {
                del.Invoke(e);
            }
        }

        /// <summary>
        /// Add an event to the queue. Queued events will be dispatched in order.
        /// </summary>
        /// <param name="e">The type of event</param>
        public void QueueEvent(BaseEvent e)
        {
           _eventQueue.Enqueue(e);
        }

        /// <summary>
        /// Update the event queue (To be called within the main update).
        /// </summary>
        /// <param name="elapsed">The elapsed time per frame</param>
        public void Update(float elapsed)
        {
            while (_eventQueue.Count > 0)
            {
                var e = _eventQueue.Dequeue() as BaseEvent;
                Dispatch(e);
            }
        }
    }
}