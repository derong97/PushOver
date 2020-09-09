using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCharacter : MonoBehaviour {
    public static SpawnCharacter instance;
    public List<Character> characters = new List<Character> ();
    public GameObject charCellPrefab;
    public Transform playerSlotsContainer;

    private void Awake () {
        instance = this;
    }

    private void Start () {
        foreach (Character character in characters) {
            SpawnCharacterCell (character);
        }
    }

    private void SpawnCharacterCell (Character character) {
        GameObject charCell = Instantiate (charCellPrefab, transform);

        Image artwork = charCell.transform.Find ("Image").GetComponent<Image> ();
        Text name = charCell.transform.Find ("TextBG").GetComponentInChildren<Text> ();

        artwork.sprite = character.characterSprite;
        name.text = character.characterName;
        charCell.name = name.text;

        Vector2 pixelSize = new Vector2 (artwork.sprite.texture.width, artwork.sprite.texture.height);
        Vector2 pixelPivot = artwork.sprite.pivot;
        Vector2 uiPivot = new Vector2 (pixelPivot.x / pixelSize.x, pixelPivot.y / pixelSize.y);

        artwork.GetComponent<RectTransform> ().pivot = uiPivot;
        artwork.GetComponent<RectTransform> ().sizeDelta *= character.zoom;
    }

    public void ShowCharacterInSlot (int player, Character character) {
        bool nullChar = (character == null);
        Sprite artwork = nullChar? null : character.characterSprite;
        string name = nullChar? string.Empty : character.characterName;
        string playerNickname = nullChar? string.Empty: "Player " + player.ToString ();
        string playerNum = nullChar? string.Empty: "P" + player.ToString ();

        Transform slot = playerSlotsContainer.GetChild (player);
        slot.Find ("Image").GetComponent<Image> ().sprite = artwork;
        slot.Find ("CharName").GetComponent<Text> ().text = name;
        slot.Find ("PlayerName").GetComponentInChildren<Text> ().text = playerNickname;
    }
}