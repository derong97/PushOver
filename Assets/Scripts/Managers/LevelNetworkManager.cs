using UnityEngine;

namespace Mirror
{
    public class LevelNetworkManager : NetworkManager
    {
        private int expectedNumPlayers = 2;
        public Transform player1;
        public Transform player2;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // add player at correct spawn position
            Transform start = numPlayers == 0 ? player1 : player2;
            float block_height = GameObject.FindGameObjectWithTag("Block").transform.lossyScale.y;

            //playerPrefab = spawnPrefabs.Find(prefab => prefab.name == "Player1");
            GameObject player = Instantiate(playerPrefab, start.position + new Vector3(0, block_height, 0), start.rotation);
            player.AddComponent<PlayerController>();
            NetworkServer.AddPlayerForConnection(conn, player);

            if(numPlayers == expectedNumPlayers)
            {
                Timer.instance.StartTimer();
                //ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
                //NetworkServer.Spawn(ball);
            }
        }
        public override void OnServerDisconnect(NetworkConnection conn)
        {

            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
    }
}