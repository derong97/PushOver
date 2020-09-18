using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

namespace MirrorTutorial
{
    public class Player : NetworkBehaviour
    {
        public static Player localPlayer;
        [SyncVar] public string matchID;
        [SyncVar] public int playerIndex; // in relation to other players in the game

        NetworkMatchChecker networkMatchChecker;

        private void Start() {
            networkMatchChecker = GetComponent<NetworkMatchChecker>();

            if (isLocalPlayer){
                localPlayer = this;
            }
            else
            {
                UILobby.instance.SpawnPlayerPrefab(this);
            }
        }

        /*
            HOST MATCH 
        */

        public void HostGame(){
            string matchID = MatchMaker.GetRandomMatchID();
            CmdHostGame(matchID);
        }

        [Command]
        void CmdHostGame(string _matchID)
        {
            matchID = _matchID;
            if (MatchMaker.instance.HostGame(_matchID, gameObject, out playerIndex))
            {
                Debug.Log($"<color=green>Game hosted successfully</color>");
                networkMatchChecker.matchId = _matchID.ToGuid();
                TargetHostGame(true, _matchID);
            }
            else
            {
                Debug.Log($"<color=red>Game hosted failed</color>");
                TargetHostGame(false, _matchID);
            }
        }

        [TargetRpc]
        void TargetHostGame(bool success, string _matchID)
        {
            matchID = _matchID;
            Debug.Log($"MatchID: {matchID} --- {_matchID}");
            UILobby.instance.HostSuccess(success, _matchID);
        }

        /*
            JOIN MATCH 
        */

        public void JoinGame(string _inputID)
        {
            CmdJoinGame(_inputID);
        }

        [Command]
        void CmdJoinGame(string _matchID)
        {
            matchID = _matchID;
            if (MatchMaker.instance.JoinGame(_matchID, gameObject, out playerIndex))
            {
                Debug.Log($"<color=green>Game joined successfully</color>");
                networkMatchChecker.matchId = _matchID.ToGuid();
                TargetJoinGame(true, _matchID);
            }
            else
            {
                Debug.Log($"<color=red>Game joined failed</color>");
                TargetJoinGame(false, _matchID);
            }
        }

        [TargetRpc]
        void TargetJoinGame(bool success, string _matchID)
        {
            matchID = _matchID;
            Debug.Log($"MatchID: {matchID} --- {_matchID}");
            UILobby.instance.JoinSuccess(success, _matchID);
        }

        /*
            BEGIN MATCH 
        */

        public void BeginGame()
        {
            CmdBeginGame();
        }

        [Command]
        void CmdBeginGame()
        {
            MatchMaker.instance.BeginGame(matchID);
            Debug.Log($"<color=green>Game beginning</color>");
        }

        public void StartGame()
        {
            TargetBeginGame();
        }

        [TargetRpc]
        void TargetBeginGame()
        {
            Debug.Log($"MatchID: {matchID} | Beginning");
            // Additively load game scene
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}
