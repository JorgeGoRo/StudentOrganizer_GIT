using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AutoEvTopicHolder : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI topicText;
    [SerializeField] private GameObject autoEvSubtopicPrefab;
    [SerializeField] [ReadOnly] private UIButton button;
    [SerializeField] [ReadOnly] private Topic currentTopic;

    public Topic CurrentTopic {
        get { return currentTopic; }
    }

    private void Awake() {
        button = GetComponent<UIButton>();
    }

    private void CreateSubtopics(Transform autoEvSubtopicsHolder) {
        for (int i = 0; i < currentTopic.subTopics.Count; i++) {
            SubTopic subtopic = currentTopic.subTopics[i];
            if (subtopic.isKeyIdea) {
                AutoEvSubtopicHolder aeSubtopicHolder = Instantiate(autoEvSubtopicPrefab, autoEvSubtopicsHolder).GetComponent<AutoEvSubtopicHolder>();
                aeSubtopicHolder.FillSubtopic(subtopic);
            }
        }
    }

    public void FillTopic(Topic topic, Transform autoEvSubtopicsHolder, UnityAction<Topic> OnClickAction) {
        currentTopic = topic;
        topicText.text = topic.topicName;
        button.onClickEvent.RemoveAllListeners();
        button.onClickEvent.AddListener(() => {
            CleanUI(autoEvSubtopicsHolder);
            CreateSubtopics(autoEvSubtopicsHolder);
            OnClickAction?.Invoke(currentTopic);
        });
    }

    public void Execute() {
        button.onClickEvent?.Invoke();
    }
    
    private void CleanUI(Transform autoEvSubtopicsHolder) {
        for (int i = 0; i < autoEvSubtopicsHolder.childCount; i++) {
            Destroy(autoEvSubtopicsHolder.GetChild(i).gameObject);
        }
    }
}