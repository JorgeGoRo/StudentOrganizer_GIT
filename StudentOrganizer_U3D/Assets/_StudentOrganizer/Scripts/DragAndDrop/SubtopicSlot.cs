public class SubtopicSlot : Slot {
    protected override void Awake() {
        base.Awake();
        DropArea.DropConditions.Add(new IsSubtopicCondition());
        DropArea.DropConditions.Add(new IsNotKeyIdeaCondition());
    }
}