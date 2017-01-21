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

    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);
        AudioManager.instance.Play(Resources.Load<AudioClip>("Audio/butt"));
    }
}
