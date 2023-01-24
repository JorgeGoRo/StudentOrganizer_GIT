using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TopicHolder : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform slotsHolder;
    [SerializeField] private GameObject subtopicSlotPrefab;
    [SerializeField] private GameObject suptopicPrefab;
    [SerializeField] [ReadOnly] private Topic currentTopic;

    public Topic CurrentTopic {
        get { return currentTopic; }
    }

    private void Awake() {
        for (int i = 0; i < slotsHolder.childCount; i++) {
            Destroy(slotsHolder.GetChild(i).gameObject);
        }
    }

    public void CreateTopics(Topic topic) {
        currentTopic = topic;
        titleText.text = currentTopic.topicName;
        for (int i = 0; i < currentTopic.subTopics.Count; i++) {
            SubTopic subTopic = currentTopic.subTopics[i];
            subTopic.courseID = currentTopic.courseID;

            Transform subtopicSlotTransform = Instantiate(subtopicSlotPrefab, slotsHolder).transform;

            SubTopicHolder topicHolder = Instantiate(suptopicPrefab, subtopicSlotTransform).GetComponent<SubTopicHolder>();
            topicHolder.CreateSubtopics(subTopic);
        }
    }
}