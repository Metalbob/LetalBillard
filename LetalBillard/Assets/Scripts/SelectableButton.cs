using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class SelectableButton : Button {

    public UnityEvent onSelect;

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        onSelect.Invoke();
    }
}
