using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour {

    Popup           popup;
    RectTransform   collectedFeedback;

    Vector2 originalPosition;

    void Start() {

        popup             = FindObjectOfType<Popup>();
        collectedFeedback = transform.Find("CollectFeedback").GetComponent<RectTransform>();


        collectedFeedback.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
        originalPosition = collectedFeedback.anchoredPosition;

    }

    void ShowTipMsg(string msg) {
        popup.ShowPopup(msg);
    }

    IEnumerator CollectedFeedbackMsg(string msg) {

        collectedFeedback.GetComponent<TextMeshProUGUI>().text = "+1 " + msg;
        collectedFeedback.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
        for (float t = 0; t <= 1.01f; t += Time.deltaTime/2f) {
            collectedFeedback.anchoredPosition += Vector2.up * 20 * Time.deltaTime;
            collectedFeedback.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1-t);
            yield return null;
        }
        collectedFeedback.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
        collectedFeedback.anchoredPosition = originalPosition;

    }

   
    public void OnItemCollected(string msg) {

        popup.ShowPopup(msg + " coletado: Abra seu inventário para visualizar o item.");

        StartCoroutine(CollectedFeedbackMsg(msg));

    }

}

























