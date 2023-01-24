using Unity.VisualScripting;
using UnityEngine;

public class KeyIdeaSlot : Slot {
    protected override void Awake() {
        base.Awake();
        //Clean previous
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        DropArea.DropConditions.Add(new IsSubtopicCondition());
        DropArea.DropConditions.Add(new IsKeyIdeaCondition());
        DropArea.OnDropHandler += OnItemDropped;
    }
    
    protected override void OnItemDropped(DraggableComponent draggable) {
        //If current SuptopicHolder is USED we skip return it to starting position
        if (draggable.GetComponent<SubtopicItem>()) {
            if (draggable.GetComponent<SubTopicHolder>().CurrentSubTopic.isKeyIdea) {
                draggable.transform.SetParent(draggable.startParent);
                draggable.GetComponent<RectTransform>().anchoredPosition = draggable.startPosition;
                return;
            }
        }
        //Check if there are previous childs and move them
        if (draggable.GetComponent<KeyIdeaItem>()) {
            if (transform.childCount > 0) {
                Transform previousChild = transform.GetChild(0);
                previousChild.SetParent(draggable.startParent);
                previousChild.GetComponent<RectTransform>().anchoredPosition = draggable.startPosition;
                previousChild.GetComponent<KeyIdeaHolder>().UpdateKeyIdeaIndex();
            }
        } 
        base.OnItemDropped(draggable);
        //Clone current SuptopicHolder and place it to origin. Also convert current SubtopicHolder to KeyIkeaHolder
        if (draggable.GetComponent<SubtopicItem>()) {
            DraggableComponent clone = Instantiate(draggable.gameObject, draggable.startParent).GetComponent<DraggableComponent>();
            clone.GetComponent<RectTransform>().anchoredPosition = draggable.startPosition;
            draggable.GetComponent<SubTopicHolder>().ConvertToKeyIdea();
            Destroy(draggable.gameObject); 
        }
        //Update KeyIdeaIndex
        if (draggable.GetComponent<KeyIdeaItem>()) {
            draggable.GetComponent<KeyIdeaHolder>().UpdateKeyIdeaIndex();
        }
    }
}