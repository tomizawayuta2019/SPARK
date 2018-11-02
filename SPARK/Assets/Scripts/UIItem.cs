using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour {
    public ItemState item;
    public RectTransform rect;
    Vector2 defPos;

	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();

        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.BeginDrag;
        entry.callback.AddListener((data) => { StartDrag(); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.EndDrag;
        entry.callback.AddListener((data) => { EndDrag(); });
        trigger.triggers.Add(entry);
    }

    private void StartDrag() {
        defPos = rect.position;
        ItemManager.instance.ItemDragStart(this);
    }

    public void Drag() {
        rect.position = Input.mousePosition;
    }

    private void EndDrag() {
        rect.position = defPos;
    }
}
