using UnityEngine;
using Mirror;

namespace MirrorTutorial
{
    public class Player : NetworkBehaviour
    {
        public static Player localPlayer;
        [SyncVar] public string matchID;

        NetworkMatchChecker networkMatchChecker;

        private void Start() {
            if(isLocalPlayer){
                localPlayer = this;
            }
            networkMatchChecker = GetComponent<NetworkMatchChecker>();
        }

        public void HostGame(){
            string matchID = MatchMaker.GetRandomMatchID();
            CmdHostGame(matchID);
        }

        [Command]
        void CmdHostGame(string _matchID)
        {
            matchID = _matchID;
            if (MatchMaker.instance.HostGame(_matchID, gameObject))
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
            Debug.Log($"MatchID: {matchID} --- {_matchID}");
            UILobby.instance.HostSuccess(success);
        }

        public void JoinGame(string _inputID)
        {
            CmdJoinGame(_inputID);
        }

        [Command]
        void CmdJoinGame(string _matchID)
        {
            matchID = _matchID;
            if (MatchMaker.instance.JoinGame(_matchID, gameObject))
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
            Debug.Log($"MatchID: {matchID} --- {_matchID}");
            UILobby.instance.JoinSuccess(success);
        }
    }
}
