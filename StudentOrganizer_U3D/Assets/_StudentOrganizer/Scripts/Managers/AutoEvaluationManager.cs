using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AutoEvaluationManager : BaseManager {
    [FoldoutGroup("UI")] [FoldoutGroup("UI/Holders")] [SerializeField]
    private Transform autoEvTopicsHolder;

    [FoldoutGroup("UI/Holders")] [SerializeField]
    private Transform autoEvSubtopicsHolder;

    [FoldoutGroup("UI/Holders")] [SerializeField]
    private GameObject foregroundBlocker;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private List<UIButton> courseButtons;

    [FoldoutGroup("UI/Labels")] [SerializeField]
    private List<TextMeshProUGUI> stimatedTimesPerCourseTexts;

    [FoldoutGroup("UI/Labels")] [SerializeField]
    private TextMeshProUGUI stimatedTopicTimeText;

    [FoldoutGroup("UI/Prefabs")] [SerializeField]
    private GameObject autoEvTopicPrefab;


    private void Start() {
        SetUIFunctionality();
        CleanUI();
    }

    private void CleanUI() {
        for (int i = 0; i < autoEvTopicsHolder.childCount; i++) {
            Destroy(autoEvTopicsHolder.GetChild(i).gameObject);
        }

        for (int i = 0; i < autoEvSubtopicsHolder.childCount; i++) {
            Destroy(autoEvSubtopicsHolder.GetChild(i).gameObject);
        }
    }

    private void SetUIFunctionality() {
        for (int i = 0; i < courseButtons.Count; i++) {
            UIButton courseButton = courseButtons[i];
            int courseIndex = i;
            courseButton.onClickEvent.RemoveAllListeners();
            courseButton.onClickEvent.AddListener(() => {
                for (int j = 0; j < courseButtons.Count; j++) {
                    courseButtons[j].GetComponentInChildren<Image>().color = Color.white;
                }

                courseButton.GetComponentInChildren<Image>().color = Color.red;
                CreateContent(courseIndex);
            });
        }
    }

    public void OpenView() {
        SetCoursesTime();
        CreateContent();
    }

    private void SetCoursesTime() {
        List<Course> courses = GameManager.GetManager<CourseReader>().Courses;
        for (int i = 0; i < courseButtons.Count; i++) {
            UIButton courseButton = courseButtons[i];
            Course course = courses[i];
            courseButton.GetComponentInChildren<TextMeshProUGUI>().text = course.name;
            int courseTime = 0;
            for (int j = 0; j < course.topics.Count; j++) {
                for (int k = 0; k < course.topics[j].subTopics.Count; k++) {
                    courseTime += course.topics[j].subTopics[k].stimatedTime;
                }
            }

            TimeSpan timeSpan = TimeSpan.FromMinutes(courseTime);
            stimatedTimesPerCourseTexts[i].text = timeSpan.ToString(@"hh\:mm");
        }
    }

    private void SetTopicTime(Topic topic) {
        int topicTime = 0;
        for (int i = 0; i < topic.subTopics.Count; i++) {
            topicTime += topic.subTopics[i].stimatedTime;
        }

        TimeSpan timeSpan = TimeSpan.FromMinutes(topicTime);
        stimatedTopicTimeText.text = timeSpan.ToString(@"hh\:mm");
        foregroundBlocker.SetActive(false);
    }

    private void CreateContent(int courseIndex = 0) {
        foregroundBlocker.SetActive(true);
        CleanUI();
        Course course = GameManager.GetManager<CourseReader>().Courses[courseIndex];
        List<Topic> topics = course.topics;
        for (int i = 0; i < topics.Count; i++) {
            Topic topic = topics[i];
            AutoEvTopicHolder aeTopicHolder = Instantiate(autoEvTopicPrefab, autoEvTopicsHolder).GetComponent<AutoEvTopicHolder>();
            aeTopicHolder.FillTopic(topic, autoEvSubtopicsHolder, SetTopicTime);
        }
    }
}