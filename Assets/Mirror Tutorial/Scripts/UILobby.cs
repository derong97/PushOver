using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MirrorTutorial
{
    public class UILobby : MonoBehaviour
    {
        public static UILobby instance;

#pragma warning disable 0649
        [Header("Host Join")]
        [SerializeField] InputField joinMatchInput;
        [SerializeField] List<Selectable> lobbySelectables = new List<Selectable>();
        [SerializeField] Canvas lobbyCanvas;
        [SerializeField] Canvas searchCanvas;
        bool searching = false;

        [Header("Lobby")]
        [SerializeField] Transform UIPlayerParent;
        [SerializeField] GameObject UIPlayerPrefab;
        [SerializeField] Text matchIDText;
        [SerializeField] GameObject beginGameButton;

        GameObject playerLobbyUI;
#pragma warning restore 0649


        void Start()
        {
            instance = this;
        }

        public void HostPublic()
        {
            lobbySelectables.ForEach(x => x.interactable = false);

            Player.localPlayer.HostGame(true);
        }


        public void HostPrivate()
        {
            lobbySelectables.ForEach(x => x.interactable = false);

            Player.localPlayer.HostGame(false);
        }        
        
        public void HostSuccess(bool success, string matchID)
        {
            if (success)
            {
                lobbyCanvas.enabled = true;
                if (playerLobbyUI != null)
                {
                    Destroy(playerLobbyUI);
                }
                playerLobbyUI = SpawnPlayerPrefab(Player.localPlayer);
                matchIDText.text = matchID;
                beginGameButton.SetActive(true);
            }
            else
            {
                lobbySelectables.ForEach(x => x.interactable = true);
            }
        }

        public void Join()
        {
            lobbySelectables.ForEach(x => x.interactable = false);
            Player.localPlayer.JoinGame(joinMatchInput.text.ToUpper());
        }

        public void JoinSuccess(bool success, string matchID)
        {
            if (success)
            {
                lobbyCanvas.enabled = true;
                if (playerLobbyUI != null)
                {
                    Destroy(playerLobbyUI);
                }
                playerLobbyUI = SpawnPlayerPrefab(Player.localPlayer);
                matchIDText.text = matchID;
            }
            else
            {
                lobbySelectables.ForEach(x => x.interactable = true);
            }
        }

        public void DisconnectGame()
        {
            if (playerLobbyUI != null)
            {
                Destroy(playerLobbyUI);
            }
            Player.localPlayer.DisconnectGame();
            lobbyCanvas.enabled = false;
            lobbySelectables.ForEach(x => x.interactable = true);
            beginGameButton.SetActive(false);
        }

        public GameObject SpawnPlayerPrefab(Player player)
        {
            GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPlayerParent);
            newUIPlayer.GetComponent<UIPlayer>().SetPlayer(player);
            newUIPlayer.transform.SetSiblingIndex(player.playerIndex - 1);
            return newUIPlayer;
        }

        public void BeginGame()
        {
            Player.localPlayer.BeginGame();
        }

        public void SearchGame()
        {  
            StartCoroutine(Searching());
        }

        IEnumerator Searching()
        {
            searchCanvas.enabled = true;
            searching = true;

            float searchInterval = 1;
            float currentTime = 1;

            while (searching)
            {
                if (currentTime > 0)
                {
                    currentTime -= Time.deltaTime;
                }
                else
                {
                    currentTime = searchInterval;
                    Player.localPlayer.SearchGame();
                }
                yield return null;
            }
            searchCanvas.enabled = false;
        }

        public void SearchSuccess(bool success, string matchID)
        {
            if (success)
            {
                searchCanvas.enabled = false;
                searching = false;
                JoinSuccess(success, matchID);
            }
        }

        public void CancelSearchGame()
        {
            searching = false;
        }
    }
}
