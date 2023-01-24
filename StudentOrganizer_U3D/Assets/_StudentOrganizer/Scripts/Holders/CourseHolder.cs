using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class CourseHolder : MonoBehaviour {
    [SerializeField] private GameObject topicHolderPrefab;
    [SerializeField] private List<TopicHolder> topicHolders;
    [SerializeField] [ReadOnly] private Course currentCourse;

    public Course CurrentCourse {
        get { return currentCourse; }
    }

    public int currentCoursePack;

    private void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        topicHolders = new List<TopicHolder>();
        currentCoursePack = 0;
    }

    public void CreateCourses(Course course) {
        currentCourse = course;
        for (int i = 0; i < currentCourse.topics.Count; i++) {
            Topic topic = currentCourse.topics[i];
            topic.courseID = currentCourse.courseID;
            TopicHolder topicHolder = Instantiate(topicHolderPrefab, transform).GetComponent<TopicHolder>();
            topicHolder.CreateTopics(topic);
            topicHolders.Add(topicHolder);
            topicHolders[i].gameObject.SetActive(i <= 3);
        }
    }

    public void EnableCourses(bool next) {
        if (!next) {
            if (0 <= (currentCoursePack - 1)) {
                currentCoursePack--;
            } else {
                currentCoursePack = 0;
                return;
            }
        } else {
            if ((topicHolders.Count - 1) >= ((currentCoursePack + 1) * 4)) {
                currentCoursePack++;
            } else {
                return;
            }
        }

        int min = currentCoursePack * 4;
        int max = (currentCoursePack + 1) * 4;
        for (int i = 0; i < topicHolders.Count; i++) {
            topicHolders[i].gameObject.SetActive(i >= min && i < max);
        }
    }

    public void ResetCourses() {
        currentCoursePack = 0;
        int min = currentCoursePack * 4;
        int max = (currentCoursePack + 1) * 4;
        for (int i = 0; i < topicHolders.Count; i++) {
            topicHolders[i].gameObject.SetActive(i >= min && i < max);
        }
    }
}