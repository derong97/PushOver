using UnityEngine;
using UnityEngine.UI;

namespace MirrorTutorial
{
    public class UILobby : MonoBehaviour
    {
        public static UILobby instance;

        [SerializeField] InputField joinMathInput;
        [SerializeField] Button joinButton;
        [SerializeField] Button hostButton;
        [SerializeField] Button lobbyButton;
        [SerializeField] Canvas lobbyCanvas;

        void Start()
        {
            instance = this;
        }

        public void Host()
        {
            joinMathInput.interactable = false;
            joinButton.interactable = false;
            hostButton.interactable = false;

            Player.localPlayer.HostGame();
        }

        public void HostSuccess(bool success)
        {
            if (success)
            {
                lobbyCanvas.enabled = true;
            }
            else
            {
                joinMathInput.interactable = true;
                joinButton.interactable = true;
                hostButton.interactable = true;
            }
        }

        public void Join()
        {
            joinMathInput.interactable = false;
            joinButton.interactable = false;
            hostButton.interactable = false;

            Player.localPlayer.JoinGame(joinMathInput.text);
        }

        public void JoinSuccess(bool success)
        {
            if (success)
            {
                lobbyCanvas.enabled = true;
            }
            else
            {
                joinMathInput.interactable = true;
                joinButton.interactable = true;
                hostButton.interactable = true;
            }
        }
    }

}
