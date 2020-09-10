using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public delegate void IntentoryHandler();

    public event IntentoryHandler Opened;

    GameObject elements;

    bool active = false;

    void Start() {

        elements = transform.Find("Elements").gameObject;
        elements.SetActive(active);

    }

    void Update() {

        
        if(Input.GetKeyDown(KeyCode.I)) {

            active = !active;
            elements.SetActive(active);

            Opened?.Invoke();

        }


    }

}


