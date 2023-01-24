using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SupervisionCourseHolder : MonoBehaviour {
    [SerializeField] private GameObject subTopicHolderPrefab;
    [SerializeField] private Transform subtopicsContentHolder;
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] [ReadOnly] private Course currentCourse;

    public Course CurrentCourse {
        get {
            return currentCourse;
        }
    }

    public Transform SubtopicsContentHolder {
        get {
            return subtopicsContentHolder;
        }
    }
    

    private void Awake() {
        CleanUI();
    }
    
    public void CreateCourses(Course course, UnityAction<SubTopic> OnClickAction) {
        currentCourse = course;
        labelText.text = course.name;
        for (int i = 0; i < CurrentCourse.topics.Count; i++) {
            for (int j = 0; j < CurrentCourse.topics[i].subTopics.Count; j++) {
                SubTopic subtopic = CurrentCourse.topics[i].subTopics[j];
                if (subtopic.isKeyIdea) {
                    SupervisionSubtopicHolder subTopicHolder = Instantiate(subTopicHolderPrefab, subtopicsContentHolder).GetComponent<SupervisionSubtopicHolder>();
                    subTopicHolder.CreateSubtopics(subtopic, OnClickAction);
                }
            }
        }
    }

    public void CleanUI() {
        for (int i = 0; i < subtopicsContentHolder.childCount; i++) {
            Destroy(subtopicsContentHolder.GetChild(i).gameObject);
        }
    }
}