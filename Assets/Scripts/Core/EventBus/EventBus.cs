namespace TinyPlay {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    
    /// <summary>
    /// Simple Event Buss Class
    /// Developed by Ilya Rastorguev
    /// </summary>
    public static class EventBus {
        private static Dictionary<Type, ClassMap> class_register_map = new Dictionary<Type, ClassMap>();
        private static Dictionary<Type, Action<IEvent>> cached_raise = new Dictionary<Type, Action<IEvent>>();
        
        /// <summary>
        /// Initialize Event Bus
        /// </summary>
        public static void Initialize() {
            /* TODO: Initialization */
        }
        
        /// <summary>
        /// Event Bus Constructor
        /// </summary>
        static EventBus() {
            Dictionary<Type, BusMap> bus_register_map = new Dictionary<Type, BusMap>();

            Type delegateType = typeof(Action<>);
            Type delegategenericregister = delegateType.MakeGenericType(typeof(IEventReceiverBase));
            Type delegategenericraise = delegateType.MakeGenericType(typeof(IEvent));

            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var t in types) {
                if (t != typeof(IEvent) && typeof(IEvent).IsAssignableFrom(t)) {
                    Type eventhubtype = typeof(EventBusGeneric<>);
                    Type genMyClass = eventhubtype.MakeGenericType(t);
                    System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(genMyClass.TypeHandle);

                    BusMap busmap = new BusMap() {
                        register = Delegate.CreateDelegate(delegategenericregister, genMyClass.GetMethod("Subscribe")) as Action<IEventReceiverBase>,
                        unregister = Delegate.CreateDelegate(delegategenericregister, genMyClass.GetMethod("Unsubscribe")) as Action<IEventReceiverBase>
                    };

                    bus_register_map.Add(t, busmap);
                    var method = genMyClass.GetMethod("FireAsInterface");
                    cached_raise.Add(t, (Action<IEvent>)Delegate.CreateDelegate(delegategenericraise, method));
                }
            }

            foreach (var t in types) {
                if (typeof(IEventReceiverBase).IsAssignableFrom(t) && !t.IsInterface) {
                    Type[] interfaces = t.GetInterfaces().Where(x => x != typeof(IEventReceiverBase) && typeof(IEventReceiverBase).IsAssignableFrom(x)).ToArray();

                    ClassMap map = new ClassMap() {
                        buses = new BusMap[interfaces.Length]
                    };

                    for (int i = 0; i < interfaces.Length; i++) {
                        var arg = interfaces[i].GetGenericArguments()[0];
                        map.buses[i] = bus_register_map[arg];
                    }

                    class_register_map.Add(t, map);
                }
            }
        }
        
        /// <summary>
        /// Register Handler
        /// </summary>
        /// <param name="target"></param>
        public static void Subscribe(IEventReceiverBase target) {
            Type t = target.GetType();
            ClassMap map = class_register_map[t];

            foreach (var busmap in map.buses) {
                busmap.register(target);
            }
        }

        /// <summary>
        /// Unregister Handler
        /// </summary>
        /// <param name="target"></param>
        public static void Unsubscribe(IEventReceiverBase target) {
            Type t = target.GetType();
            ClassMap map = class_register_map[t];

            foreach (var busmap in map.buses)
            {
                busmap.unregister(target);
            }
        }

        /// <summary>
        /// Fire Handler
        /// </summary>
        /// <param name="ev"></param>
        public static void Fire(IEvent ev) {
            cached_raise[ev.GetType()](ev);
        }
        #region Private Classes
        /// <summary>
        /// Bus Map Class
        /// </summary>
        private class BusMap {
            public Action<IEventReceiverBase> register;
            public Action<IEventReceiverBase> unregister;
        }

        /// <summary>
        /// Class Map
        /// </summary>
        private class ClassMap {
            public BusMap[] buses;
        }
        #endregion
    }
}