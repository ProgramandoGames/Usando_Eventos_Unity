using UnityEngine;

public class EventManager : MonoBehaviour {

    Player    player;
    UIManager uiManager;

    Inventory inventory;

    void Start() {

        player    = FindObjectOfType<Player>();
        uiManager = FindObjectOfType<UIManager>();

        inventory = FindObjectOfType<Inventory>();

        player.ItemCollected += uiManager.OnItemCollected;

        inventory.Opened += player.OnInventoryOpened;

    }

}



