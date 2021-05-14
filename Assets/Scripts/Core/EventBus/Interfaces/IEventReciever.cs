namespace TinyPlay {
    /// <summary>
    /// Simple IEvent Reciever Interface for Different types
    /// Developed by Ilya Rastorguev
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventReciever <T> : IEventReceiverBase where T : struct, IEvent {
        /// <summary>
        /// On Event Fire
        /// </summary>
        /// <param name="e"></param>
        void OnEventFire(T e);
    }
}