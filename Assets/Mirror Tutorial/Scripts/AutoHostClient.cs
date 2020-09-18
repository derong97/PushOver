using UnityEngine;
using Mirror;

namespace MirrorTutorial
{
    public class AutoHostClient : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] NetworkManager networkManger;
#pragma warning restore 0649

        void Start()
        {
            if (!Application.isBatchMode) // Batch mode means headless build
            {
                Debug.Log("===Client Build===");
                networkManger.StartClient();
            }
            else{
                Debug.Log("===Server Build===");
            }
        }

        public void JoinLocal()
        {
            networkManger.networkAddress = "localhost";
            networkManger.StartClient();
        }
    }

}

