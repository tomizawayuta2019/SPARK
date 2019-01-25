using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LongClick : MonoBehaviour {
    private EventTrigger eventTrigger;
    [SerializeField] bool isClick;

    [SerializeField] UnityEvent longClickEvent;

    private void Start()
    {
        eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => isClick = true);

        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => isClick = false);

        eventTrigger.triggers.Add(entry);
    }

    private void Update()
    {
        if (isClick)
        {
            longClickEvent.Invoke();
        }
    }
}
