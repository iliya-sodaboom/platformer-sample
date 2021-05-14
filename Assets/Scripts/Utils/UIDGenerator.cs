namespace TinyPlay {
    /// <summary>
    /// Object UID Generator
    /// </summary>
    public static class UIDGenerator {
        // Current Object ID
        public static int currentUID = 0;

        /// <summary>
        /// Get UID
        /// </summary>
        /// <returns></returns>
        public static int GetUID() {
            int uid = currentUID;
            currentUID++;
            return uid;
        }
    }
}