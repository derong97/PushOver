using UnityEngine;
using UnityEngine.EventSystems;

public class OnTouch : MonoBehaviour, IPointerDownHandler {

    private Transform currentCharacter;
    private Vector3 touchPosWorld;
    private TouchPhase touchPhase = TouchPhase.Ended;

    // Using mouse pointer to select character
    public void OnPointerDown (PointerEventData eventData) {
        currentCharacter = eventData.pointerEnter.gameObject.transform.parent;
        print (currentCharacter.name);
        if (currentCharacter != null) {
            int index = currentCharacter.GetSiblingIndex();
            Character character = SpawnCharacter.instance.characters[index];
            SpawnCharacter.instance.ShowCharacterInSlot(0, character);
        } else {
            SpawnCharacter.instance.ShowCharacterInSlot(0, null);
        }
    }

    // Using screen touch to select character
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase)
        {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            if (hitInfo.collider != null)
            {
                GameObject touchedObject = hitInfo.transform.gameObject;
                Debug.Log("Touched " + touchedObject.transform.name);
            }
        }
    }
}