using TMPro;
using UnityEngine;

public class SubTopicHolder : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI nameText;
    public SubTopic currentSubTopic;
    
    public void FillSubtopic(SubTopic subTopic) {
        this.currentSubTopic = subTopic;
        nameText.text = currentSubTopic.name;
    }
}