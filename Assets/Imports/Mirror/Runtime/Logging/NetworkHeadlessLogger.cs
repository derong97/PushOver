using UnityEngine;

namespace Mirror.Logging
{
    /// <summary>
    /// Used to replace log hanlder with Console Color LogHandler
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/NetworkHeadlessLogger")]
    [HelpURL("https://mirror-networking.com/docs/Components/NetworkHeadlessLogger.html")]
    public class NetworkHeadlessLogger : MonoBehaviour
    {
#pragma warning disable
        [SerializeField] bool showExceptionStackTrace = false;
#pragma warning restore

        void Awake()
        {
#if UNITY_SERVER
            LogFactory.ReplaceLogHandler(new ConsoleColorLogHandler(showExceptionStackTrace));
#endif
        }
    }
}
