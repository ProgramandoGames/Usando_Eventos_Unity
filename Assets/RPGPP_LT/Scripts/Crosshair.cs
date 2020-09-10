using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

    new Camera camera;

    Sprite crosshair;
    Sprite hand;

    float distance = 3f;

    Image image;

    void Start() {

        image = GetComponent<Image>();

        crosshair = Resources.Load<Sprite>("UI/crosshair");
        hand      = Resources.Load<Sprite>("UI/grab_hand");

        camera = Camera.main;

    }

    void Update() {

        RaycastHit hitInfo;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, distance, LayerMask.GetMask("interactable"))) {
            image.sprite = hand;
            image.GetComponent<RectTransform>().sizeDelta = Vector2.one * 50;
        } else {
            image.sprite = crosshair;
            image.GetComponent<RectTransform>().sizeDelta = Vector2.one * 5;
        }

    }

}
