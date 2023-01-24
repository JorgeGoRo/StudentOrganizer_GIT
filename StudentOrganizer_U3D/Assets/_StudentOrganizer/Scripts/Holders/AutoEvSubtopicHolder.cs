using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoEvSubtopicHolder : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI subtopicText;
    [SerializeField] private TextMeshProUGUI strategyText;
    [SerializeField] private Image satisfactionImage;
    [SerializeField] private List<Sprite> satisfactionIcons;
    [SerializeField] [ReadOnly] private SubTopic currentSubTopic;
    public SubTopic CurrentSubTopic {
        get {
            return currentSubTopic;
        }
    }
    
    public void FillSubtopic(SubTopic subtopic) {
        currentSubTopic = subtopic;
        subtopicText.text = currentSubTopic.name + " " + currentSubTopic.description;
        strategyText.text = currentSubTopic.studyStrategy.ToString();
        satisfactionImage.sprite = satisfactionIcons[currentSubTopic.satisfaction];
    }
}