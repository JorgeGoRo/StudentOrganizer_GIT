using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubTopicHolder : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private GameObject keyIdeaHolderPrefab;
    [SerializeField] [ReadOnly] private List<Color> colors = new List<Color>() {Color.blue, Color.red, Color.green};
    [SerializeField] [ReadOnly] private SubTopic currentSubTopic;

    public SubTopic CurrentSubTopic {
        get { return currentSubTopic; }
    }


    public void CreateSubtopics(SubTopic subTopic) {
        currentSubTopic = subTopic;
        nameText.text = currentSubTopic.name;
        backgroundImage.color = colors[currentSubTopic.courseID];
        if (currentSubTopic.week != -1) {
            ConvertToKeyIdea(GameManager.Instance.GetManager<KeyIdeaSelector>().GetKeyIdeaHolderByIndex(currentSubTopic.keyIdeaIndex));
        }
    }

    public void ConvertToKeyIdea(Transform newParent = null) {
        KeyIdeaHolder keyIdeaHolder = Instantiate(keyIdeaHolderPrefab, newParent != null ? newParent : transform.parent).GetComponent<KeyIdeaHolder>();
        keyIdeaHolder.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        keyIdeaHolder.FillKeyIdea(currentSubTopic);
    }

    public void UpdateStatus(SubTopic subTopic) {
        currentSubTopic = subTopic;
    }
}