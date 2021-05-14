namespace TinyPlay {
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Event Bus Generic Class
    /// Developed by Ilya Rastorguev
    /// </summary>
    public static class EventBusGeneric<T> where T : struct, IEvent {
        private static HashSet<IEventReciever<T>> hash;
        
        private static IEventReciever<T>[] buffer;
        private static int count;
        private static int blocksize = 256;
        
        static EventBusGeneric() {
            hash = new HashSet<IEventReciever<T>>();
            buffer = new IEventReciever<T>[0];
        }

        /// <summary>
        /// Register Handler
        /// </summary>
        /// <param name="handler"></param>
        public static void Subscribe(IEventReceiverBase handler) {
            count++;
            hash.Add(handler as IEventReciever<T>);
            if(buffer.Length < count) {
                buffer = new IEventReciever<T>[count + blocksize];
            }
            
            hash.CopyTo(buffer);
        }

        /// <summary>
        /// Unregister Handler
        /// </summary>
        /// <param name="handler"></param>
        public static void Unsubscribe(IEventReceiverBase handler) {
            hash.Remove(handler as IEventReciever<T>);
            hash.CopyTo(buffer);
            count--;
        }

        /// <summary>
        /// Fire Event
        /// </summary>
        /// <param name="e"></param>
        public static void Fire(T e) {
            for (int i = 0; i < count; i++) {
                buffer[i].OnEventFire(e);
            }
        }

        /// <summary>
        /// Fire Interface Event
        /// </summary>
        /// <param name="e"></param>
        public static void FireAsInterface(IEvent e) {
            Fire((T)e);
        }

        /// <summary>
        /// Clear Handlers
        /// </summary>
        public static void Clear() {
            hash.Clear();
        }
    }
}