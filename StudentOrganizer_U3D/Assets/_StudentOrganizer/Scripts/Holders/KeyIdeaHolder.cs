using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyIdeaHolder : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] [ReadOnly] private List<Color> colors = new List<Color>() {Color.blue, Color.red, Color.green};
    [SerializeField] [ReadOnly] private SubTopic currentSubTopic;

    public SubTopic CurrentSubTopic {
        get { return currentSubTopic; }
    }
    // public int keyIdeaIndex;

    public void FillKeyIdea(SubTopic subTopic) {
        currentSubTopic = subTopic;
        if (currentSubTopic.keyIdeaIndex == -1) {
            for (int i = 0; i < transform.parent.parent.childCount; i++) {
                if (transform.parent.parent.GetChild(i) == transform.parent) {
                    currentSubTopic.keyIdeaIndex = i;
                    break;
                }
            }

            GameManager.Instance.GetManager<CourseReader>().SetSubtopicAsUsed(currentSubTopic, true);
        }

        nameText.text = currentSubTopic.name;
        backgroundImage.color = colors[currentSubTopic.courseID];
        GameManager.Instance.GetManager<KeyIdeaSelector>().HideKeyIdeaIfNeeded(this);
    }

    public void DeleteKeyIdea() {
        GameManager.Instance.GetManager<CourseReader>().SetSubtopicAsUsed(currentSubTopic, false);
    }

    public void UpdateKeyIdeaIndex() {
        for (int i = 0; i < transform.parent.parent.childCount; i++) {
            if (transform.parent.parent.GetChild(i) == transform.parent) {
                currentSubTopic.keyIdeaIndex = i;
                break;
            }
        }
    }
}