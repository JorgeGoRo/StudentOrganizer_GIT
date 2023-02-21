using System;
using System.Collections.Generic;
using System.Linq;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlanificationManager : BaseManager {
    [FoldoutGroup("UI")] [FoldoutGroup("UI/Labels")] [SerializeField]
    private List<TextMeshProUGUI> courseNamesText;

    [FoldoutGroup("UI/Holders and Tabs")] [SerializeField]
    private List<CourseHolder> courseHolders;

    [FoldoutGroup("UI/Holders and Tabs")] [SerializeField]
    private List<UITab> uiTabs;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton autoAssignButton;
    
    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton previousButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton nextButton;


    [FoldoutGroup("Debug")] [SerializeField]
    private int courseIndex = 0;


    private void Start() {
        CleanUI();
        SetUIFunctionality();
    }


    private void CleanUI() {
        for (int i = 0; i < courseHolders.Count; i++) {
            for (int j = 0; j < courseHolders[i].transform.childCount; j++) {
                Destroy(courseHolders[i].transform.GetChild(j).gameObject);
            }
        }
    }

    private void SetUIFunctionality() {
        for (int i = 0; i < uiTabs.Count; i++) {
            UITab tab = uiTabs[i];
            int tabIndex = i;
            tab.onClickEvent.AddListener(() => { SetCourseIndex(tabIndex); });
        }

        previousButton.onClickEvent.RemoveAllListeners();
        previousButton.onClickEvent.AddListener(PreviousTopics);
        nextButton.onClickEvent.RemoveAllListeners();
        nextButton.onClickEvent.AddListener(NextTopics);
        autoAssignButton.onClickEvent.RemoveAllListeners();
        autoAssignButton.onClickEvent.AddListener(AutoAssign);
    }

    public void OpenView() {
        CleanUI();
        CreateContent();
        SetCourseIndex(0);
        uiTabs[0].targetContainer.Show();
    }

    private void CreateContent() {
        List<Course> courses = GameManager.GetManager<CourseReader>().Courses;
        for (int i = 0; i < courses.Count; i++) {
            Course course = courses[i];
            courseHolders[i].CreateCourses(course);
            courseNamesText[i].text = course.name;
        }
    }

    private void SetCourseIndex(int newCourseIndex) {
        courseIndex = newCourseIndex;
        courseHolders[courseIndex].ResetCourses();
    }

    private void PreviousTopics() {
        courseHolders[courseIndex].EnableCourses(false);
    }

    private void NextTopics() {
        courseHolders[courseIndex].EnableCourses(true);
    }

    public void UpdateSubtopicHolderStatus(SubTopic subTopic) {
        //Get Course
        var courseH = courseHolders[subTopic.courseID];
        //Get Topic
        var topicH = courseH.transform.GetChild(subTopic.topicID);
        //Get SubTopics
        List<SubTopicHolder> subtopicsH = new List<SubTopicHolder>();
        subtopicsH = topicH.GetComponentsInChildren<SubTopicHolder>().ToList();
        //Get Subtopic
        var subTopicH = subtopicsH.Find((sth) => sth.CurrentSubTopic.subtopicID == subTopic.subtopicID);
        subTopicH.UpdateStatus(subTopic);
    }
    
    private void AutoAssign() {
        //Set UI
        GameManager.GetManager<KeyIdeaManager>().CleanUI();
        List<Course> courses = GameManager.GetManager<CourseReader>().Courses;
        Dictionary<int, List<SubTopic>> weekSubtopicDic = new Dictionary<int, List<SubTopic>>();
        for (int i = 0; i < courses.Count; i++) {
            Course course = courses[i];
            for (int j = 0; j < course.topics.Count; j++) {
                Topic topic = course.topics[j];
                for (int k = 0; k < topic.subTopics.Count; k++) {
                    SubTopic subTopic = topic.subTopics[k]; 
                    //Set KeyIdeaIndex
                    if (!weekSubtopicDic.ContainsKey(subTopic.week)) {
                        weekSubtopicDic.Add(subTopic.week, new List<SubTopic>());
                    }

                    subTopic.isKeyIdea = true;
                    subTopic.keyIdeaIndex = weekSubtopicDic[subTopic.week].Count;
                    weekSubtopicDic[subTopic.week].Add(subTopic);
                    //Create subtopic
                    GameManager.GetManager<KeyIdeaManager>().CreateKeyIdea(subTopic);
                }
            }
        }

        // foreach (KeyValuePair<int, List<SubTopic>> weekSubtopics in weekSubtopicDic) {
        //     for (int i = 0; i < weekSubtopics.Value.Count; i++) {
        //         GameManager.GetManager<KeyIdeaManager>().CreateKeyIdea(weekSubtopics.Value[i]);
        //     }
        // }
        //GameManager.GetManager<KeyIdeaManager>().UpdateWeek();
        
        // OpenView();
    }
}