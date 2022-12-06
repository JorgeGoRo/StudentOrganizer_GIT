using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSubtopicCondition : DropCondition
{
    public override bool Check(DraggableComponent draggable) {
        return draggable.GetComponent<SubtopicItem>() != null;
    }
}
