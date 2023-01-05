public class IsSubtopicCondition : DropCondition {
    public override bool Check(DraggableComponent draggable) {
        return draggable.GetComponent<SubtopicItem>() != null;
    }
}

public class IsNotSubtopicCondition : DropCondition {
    public override bool Check(DraggableComponent draggable) {
        return draggable.GetComponent<SubtopicItem>() == null;
    }
}