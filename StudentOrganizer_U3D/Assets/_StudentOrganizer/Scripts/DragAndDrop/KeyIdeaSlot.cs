using Unity.VisualScripting;

public class KeyIdeaSlot : Slot {
    protected override void Awake() {
        base.Awake();
        DropArea.DropConditions.Add(new IsSubtopicCondition());
        //DropArea.DropConditions.Add(new IsKeyIdeaCondition());
    }
    
    protected virtual void OnItemDropped(DraggableComponent draggable) {
        DraggableComponent clone = Instantiate(draggable.gameObject, draggable.startParent).GetComponent<DraggableComponent>();
        clone.transform.position = draggable.startPosition;
        base.OnItemDropped(draggable);
        
    }
}