using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopicHolder : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private List<SubtopicSlot> slots;
    [SerializeField] private GameObject suptopicPrefab;
    public Topic currentTopic;
    private void Awake() {
        slots = new List<SubtopicSlot>(GetComponentsInChildren<SubtopicSlot>());
    }
    
    public void FillTopic(Topic topic) {
        currentTopic = topic;
        titleText.text = currentTopic.topicName;
        for (int i = 0; i < currentTopic.subTopics.Count; i++) {
            SubTopic subTopic = currentTopic.subTopics[i];
            SubTopicHolder topicHolder = Instantiate(suptopicPrefab, slots[i].transform).GetComponent<SubTopicHolder>();
            topicHolder.FillSubtopic(subTopic);
        }
    }

}
