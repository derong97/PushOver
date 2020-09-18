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
        [SyncVar] public Match currentMatch;

        NetworkMatchChecker networkMatchChecker;

        GameObject playerLobbyUI;

        private void Awake() {
            networkMatchChecker = GetComponent<NetworkMatchChecker>();
        }

        public override void OnStartClient()
        {
            if (isLocalPlayer)
            {
                localPlayer = this;
            }
            else
            {
                Debug.Log($"Spawning other player UI");
                playerLobbyUI = UILobby.instance.SpawnPlayerPrefab(this);
            }
        }

        public override void OnStopClient()
        {
            Debug.Log($"Client stopped");
            ClientDisconnect();
        }

        public override void OnStopServer()
        {
            Debug.Log($"Client stopped on server");
            ServerDisconnect();
        }

        /*
            HOST GAME 
        */

        public void HostGame(bool publicMatch){
            string matchID = MatchMaker.GetRandomMatchID();
            CmdHostGame(matchID, publicMatch);
        }

        [Command]
        void CmdHostGame(string _matchID, bool publicMatch)
        {
            matchID = _matchID;
            if (MatchMaker.instance.HostGame(_matchID, gameObject, publicMatch, out playerIndex))
            {
                Debug.Log($"<color=green>Game hosted successfully</color>");
                networkMatchChecker.matchId = _matchID.ToGuid();
                TargetHostGame(true, _matchID, playerIndex);
            }
            else
            {
                Debug.Log($"<color=red>Game hosted failed</color>");
                TargetHostGame(false, _matchID, playerIndex);
            }
        }

        [TargetRpc]
        void TargetHostGame(bool success, string _matchID, int _playerIndex)
        {
            playerIndex = _playerIndex;
            matchID = _matchID;
            Debug.Log($"MatchID: {matchID} == {_matchID}");
            UILobby.instance.HostSuccess(success, _matchID);
        }

        /*
            JOIN GAME 
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
                TargetJoinGame(true, _matchID, playerIndex);
            }
            else
            {
                Debug.Log($"<color=red>Game joined failed</color>");
                TargetJoinGame(false, _matchID, playerIndex);
            }
        }

        [TargetRpc]
        void TargetJoinGame(bool success, string _matchID, int _playerIndex)
        {
            playerIndex = _playerIndex;
            matchID = _matchID;
            Debug.Log($"MatchID: {matchID} == {_matchID}");
            UILobby.instance.JoinSuccess(success, _matchID);
        }

        /*
            SEARCH GAME 
        */

        public void SearchGame()
        {
            CmdSearchGame();
        }

        [Command]
        void CmdSearchGame()
        {
            if (MatchMaker.instance.SearchGame(gameObject, out playerIndex, out matchID))
            {
                Debug.Log($"<color=green>Game Found Successfully</color>");
                networkMatchChecker.matchId = matchID.ToGuid();
                TargetSearchGame(true, matchID, playerIndex);
            }
            else
            {
                Debug.Log($"<color=red>Game Search Failed</color>");
                TargetSearchGame(false, matchID, playerIndex);
            }
        }

        [TargetRpc]
        public void TargetSearchGame(bool success, string _matchID, int _playerIndex)
        {
            playerIndex = _playerIndex;
            matchID = _matchID;
            Debug.Log($"MatchID: {matchID} == {_matchID} | {success}");
            UILobby.instance.SearchSuccess(success, _matchID);
        }

        /*
            BEGIN GAME 
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

        public void StartGame() // Server
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

        /*
            DISCONNECT GAME 
        */

        public void DisconnectGame()
        {
            CmdDisconnectGame();
        }

        [Command]
        void CmdDisconnectGame()
        {
            ServerDisconnect();
        }

        void ServerDisconnect()
        {
            MatchMaker.instance.PlayerDisconnected(this, matchID);
            networkMatchChecker.matchId = string.Empty.ToGuid();
            RpcDisconnectGame();
        }

        [ClientRpc]
        void RpcDisconnectGame()
        {
            ClientDisconnect();
        }

        void ClientDisconnect()
        {
            if (playerLobbyUI != null)
            {
                Destroy(playerLobbyUI);
            }
        }
    }
}
