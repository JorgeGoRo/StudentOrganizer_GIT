using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {
    
    protected DropArea DropArea;
    public DropCheckCondition dropCheckCondition = DropCheckCondition.TRUE_FOR_ALL;
    protected virtual void Awake() {
        DropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        DropArea.dropCheckCondition = dropCheckCondition;
    }

    protected virtual void OnItemDropped(DraggableComponent draggable) {
        draggable.transform.position = transform.position;
        draggable.transform.SetParent(transform);
    }
}
