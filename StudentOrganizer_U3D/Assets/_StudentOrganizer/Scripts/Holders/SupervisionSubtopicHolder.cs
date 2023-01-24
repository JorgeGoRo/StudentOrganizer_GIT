using System;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SupervisionSubtopicHolder : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] [ReadOnly] private UIButton button;
    [SerializeField] [ReadOnly] private List<Color> colors = new List<Color>() {Color.blue, Color.red, Color.green};
    [SerializeField] [ReadOnly] private SubTopic currentSubTopic;

    public SubTopic CurrentSubTopic {
        get { return currentSubTopic; }
    }


    private void Awake() {
        button = GetComponent<UIButton>();
    }

    public void CreateSubtopics(SubTopic subTopic, UnityAction<SubTopic> onClickAction) {
        currentSubTopic = subTopic;
        nameText.text = currentSubTopic.name;
        backgroundImage.color = colors[currentSubTopic.courseID];
        button.onClickEvent.RemoveAllListeners();
        button.onClickEvent.AddListener(() => onClickAction?.Invoke(currentSubTopic));
    }

    public void UpdateStatus(SubTopic subTopic) {
        currentSubTopic = subTopic;
    }
}