using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverOnEnable : MonoBehaviour {
    [SerializeField] GameObject target;
    EnableCheck enableCheck;

    private void Start()
    {
        enableCheck = target.GetComponent<EnableCheck>();
        if (enableCheck == null) { enableCheck = target.AddComponent<EnableCheck>(); }
        target.SetActive(false);
    }

    public void OnMouseOver()
    {
        enableCheck.AddEvent();
    }

    public void OnMouseExit()
    {
        enableCheck.RemoveEvent();
    }

    private class EnableCheck : MonoBehaviour
    {
        int count;

        public void AddEvent()
        {
            count++;
            SetEnable(true);
        }

        public void RemoveEvent()
        {
            count--;
            if (count == 0) { SetEnable(false); }
        }

        private void SetEnable(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
