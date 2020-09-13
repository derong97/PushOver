using UnityEngine;
using Mirror;

namespace MirrorTutorial
{
    public class AutoHostClient : MonoBehaviour
    {
        [SerializeField] NetworkManager networkManger;

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

