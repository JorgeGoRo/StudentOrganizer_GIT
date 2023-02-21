using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LastReviewSubtopicHolder : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI subtopicText;
    [SerializeField] private Image background;
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
        background.color = GameManager.Instance.CourseColors[subtopic.courseID];
        subtopicText.text = currentSubTopic.name + " " + currentSubTopic.description;
        button.onClickEvent.RemoveAllListeners();
        button.onClickEvent.AddListener(() => {
            OnClickAction?.Invoke(currentSubTopic);
        });
    }
}