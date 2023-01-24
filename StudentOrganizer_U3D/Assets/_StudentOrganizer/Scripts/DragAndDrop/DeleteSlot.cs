using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSlot : Slot
{
    protected override void Awake() {
        base.Awake();
        DropArea.DropConditions.Add(new IsKeyIdeaCondition());
        DropArea.OnDropHandler += OnItemDropped;
    }

    protected override void OnItemDropped(DraggableComponent draggable) {
        base.OnItemDropped(draggable);
        if (draggable.GetComponent<KeyIdeaItem>()) {
            draggable.GetComponent<KeyIdeaHolder>().DeleteKeyIdea();
            Destroy(draggable.gameObject);
        } 
    }
}
