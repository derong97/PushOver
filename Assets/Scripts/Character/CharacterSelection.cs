using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    public int index;
    private static GameObject characterSelected = null;
    private static GameObject characterItem = null;

    private void Start() {
        index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount ; i++) {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in characterList) {
            go.SetActive(false);
        }

        if(characterList[index]){
            characterList[index].SetActive(true);
            characterSelected = characterList[index];
        }
    }

    public void Toggle(bool isLeft) {
        characterList[index].SetActive(false);

        if(isLeft) {
            index--;
            if(index<0) index = characterList.Length - 1;
        }else {
            index++;
            if(index==characterList.Length) index = 0;
        }

        characterList[index].SetActive(true);
    }

    public void ConfirmButton() {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene("Hosp");
    }

    public static GameObject getCharacter()
    {
        return characterSelected;
    }
    public static void setItem(GameObject _characterItem)
    {
        characterItem = _characterItem;
    }

    public static GameObject getItem()
    {
        return characterItem;
    }

    public void Back()
    {
        SceneManager.LoadScene("Start");
    }
}
