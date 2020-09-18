using UnityEngine;
using UnityEngine.UI;

namespace MirrorTutorial
{
    public class UIPlayer : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] Text text;
        Player player;
#pragma warning restore 0649

        public void SetPlayer(Player player)
        {
            this.player = player;
            text.text = "Player " + player.playerIndex.ToString();
        }
    }
}