public class IsKeyIdeaCondition : DropCondition {
    public override bool Check(DraggableComponent draggable) {
        return draggable.GetComponent<KeyIdeaItem>() != null;
    }
}

public class IsNotKeyIdeaCondition : DropCondition {
    public override bool Check(DraggableComponent draggable) {
        return draggable.GetComponent<KeyIdeaItem>() == null;
    }
}