using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DropCheckCondition {
    TRUE_FOR_ALL,
    TRUE_FOR_ANY
}
public class DropArea : MonoBehaviour {
    public List<DropCondition> DropConditions = new List<DropCondition>();
    public DropCheckCondition dropCheckCondition;
    public event Action<DraggableComponent> OnDropHandler;

    public bool Accepts(DraggableComponent draggable) {
        if (dropCheckCondition == DropCheckCondition.TRUE_FOR_ALL) {
            return DropConditions.TrueForAll(cond => cond.Check(draggable));
        } else if (dropCheckCondition == DropCheckCondition.TRUE_FOR_ANY) {
            return DropConditions.Exists(cond => cond.Check(draggable));
        } else {
            return DropConditions.TrueForAll(cond => cond.Check(draggable));
        }
    }

    public void Drop(DraggableComponent draggable) {
        OnDropHandler?.Invoke(draggable);
    }
}