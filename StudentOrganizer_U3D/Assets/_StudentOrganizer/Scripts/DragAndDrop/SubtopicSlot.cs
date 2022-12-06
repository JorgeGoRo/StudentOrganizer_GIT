using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtopicSlot : Slot
{
   protected override void Awake() {
      base.Awake();
      DropArea.DropConditions.Add(new IsSubtopicCondition());
      
   }
}
