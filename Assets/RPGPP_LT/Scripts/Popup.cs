using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour {

    TextMeshProUGUI text;
    RectTransform rect;

    float hidePosition = 1183;
    float viewPosition = 769;

    WaitForSeconds stayTime = new WaitForSeconds(3);

    float speed = 3;

    void Start() {
        rect = GetComponent<RectTransform>();
        text = transform.Find("Msg").GetComponent<TextMeshProUGUI>();
    }

    public void ShowPopup(string msg) {
        StartCoroutine(Show(msg));
    }

    IEnumerator Show(string msg) {

        text.text = msg;

        // entry
        for (float t= 0; t <= 1.0f; t += speed * Time.deltaTime) {
            rect.anchoredPosition = new Vector2(Mathf.Lerp(hidePosition,
                                                           viewPosition, t),
                                                rect.anchoredPosition.y);
            yield return null;
        }

        yield return stayTime;

        // exit
        for (float t = 0; t <= 1.0f; t += speed * Time.deltaTime) {
            rect.anchoredPosition = new Vector2(Mathf.Lerp(viewPosition,
                                                           hidePosition, t),
                                                rect.anchoredPosition.y);
            yield return null;
        }

    }

}
