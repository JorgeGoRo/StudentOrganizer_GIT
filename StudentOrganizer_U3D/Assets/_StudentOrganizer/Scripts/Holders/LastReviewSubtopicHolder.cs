using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LastReviewSubtopicHolder : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI subtopicText;
    [SerializeField] [ReadOnly] private UIButton button;
    [SerializeField] [ReadOnly] private SubTopic currentSubTopic;

    public SubTopic CurrentSubTopic {
        get { return currentSubTopic; }
    }

    private void Awake() {
        button = GetComponent<UIButton>();
    }

    public void FillSubtopic(SubTopic subtopic, UnityAction<SubTopic> OnClickAction) {
        currentSubTopic = subtopic;
        subtopicText.text = currentSubTopic.name + " " + currentSubTopic.description;
        button.onClickEvent.RemoveAllListeners();
        button.onClickEvent.AddListener(() => {
            OnClickAction?.Invoke(currentSubTopic);
        });
    }
}