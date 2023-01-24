using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SupervisionManager : BaseManager {
    [FoldoutGroup("UI")] [FoldoutGroup("UI/Holders")] [SerializeField]
    private Transform coursesContentHolder;

    [FoldoutGroup("UI/Holders")] [SerializeField]
    private Transform hiddenSubtopicsHolder;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton previousButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton nextButton;


    [FoldoutGroup("UI/Labels")] [SerializeField]
    private TextMeshProUGUI currentWeekText;

    [FoldoutGroup("UI/Labels")] [SerializeField]
    private TextMeshProUGUI currentTimeText;

    [FoldoutGroup("UI/Prefabs")] [SerializeField]
    private GameObject supervisionCoursePrefab;

    [FoldoutGroup("UI/Strategy")] [SerializeField]
    private TextMeshProUGUI keyIdeaName;

    [FoldoutGroup("UI/Strategy")] [SerializeField]
    private TMP_Dropdown strategyDropdown;

    [FoldoutGroup("UI/Strategy")] [SerializeField]
    private TMP_InputField timeInput;

    [FoldoutGroup("UI/Strategy")] [SerializeField]
    private List<UIButton> satisfationButtons;

    [FoldoutGroup("UI/Strategy")] [SerializeField]
    private GameObject foregroundBlocker;

    [FoldoutGroup("UI/Strategies Info")] [SerializeField]
    private UIButton infoButton;

    [FoldoutGroup("UI/Strategies Info")] [SerializeField]
    private UIButton closeInfoButton;

    [FoldoutGroup("UI/Strategies Info")] [SerializeField]
    private UIContainer strategiesInfoHolder;

    [FoldoutGroup("Debug")] [SerializeField]
    private int maxWeeks = 15;

    [FoldoutGroup("Debug")] [SerializeField]
    private int currentWeek = 0;


    private void Start() {
        CleanUI();
        SetUIFunctionality();
    }

    private void CleanUI() {
        for (int i = 0; i < coursesContentHolder.childCount; i++) {
            coursesContentHolder.GetChild(i).GetComponent<SupervisionCourseHolder>().CleanUI();
            Destroy(coursesContentHolder.GetChild(i).gameObject);
        }

        for (int i = 0; i < hiddenSubtopicsHolder.childCount; i++) {
            Destroy(hiddenSubtopicsHolder.GetChild(i).gameObject);
        }
    }

    private void SetUIFunctionality() {
        currentWeekText.text = "SEMANA " + (currentWeek + 1).ToString("D1");

        previousButton.onClickEvent.RemoveAllListeners();
        previousButton.onClickEvent.AddListener(PreviousWeek);

        nextButton.onClickEvent.RemoveAllListeners();
        nextButton.onClickEvent.AddListener(NextWeek);

        infoButton.onClickEvent.RemoveAllListeners();
        infoButton.onClickEvent.AddListener(() => { ShowStudyStrategiesInfo(); });

        closeInfoButton.onClickEvent.RemoveAllListeners();
        closeInfoButton.onClickEvent.AddListener(() => { ShowStudyStrategiesInfo(false); });
    }

    public void OpenView() {
        CreateContent();
    }

    private void ShowStudyStrategiesInfo(bool isShowing = true) {
        if (isShowing) {
            strategiesInfoHolder.Show();
        } else {
            strategiesInfoHolder.Hide();
        }
    }

    public void CreateContent() {
        CleanUI();
        List<Course> courses = GameManager.GetManager<CourseReader>().Courses;
        for (int i = 0; i < courses.Count; i++) {
            Course course = courses[i];
            SupervisionCourseHolder supervisionCourseHolder = Instantiate(supervisionCoursePrefab, coursesContentHolder).GetComponent<SupervisionCourseHolder>();
            supervisionCourseHolder.CreateCourses(course, SetCurrentKeyIdea);
        }

        currentWeek = 0;
        currentWeekText.text = "SEMANA " + (currentWeek + 1).ToString("D1");
        ShowCurrentWeekContent();
        UpdateTime();
    }

    #region STRATEGY

    private void SetCurrentKeyIdea(SubTopic subtopic) {
        //Foreground blocker
        foregroundBlocker.SetActive(false);
        //Name Text
        keyIdeaName.text = subtopic.name;
        //Dropdown
        strategyDropdown.SetValueWithoutNotify((int) subtopic.studyStrategy);
        strategyDropdown.onValueChanged.RemoveAllListeners();
        strategyDropdown.onValueChanged.AddListener((newValue) => { subtopic.studyStrategy = (StudyStrategy) newValue; });
        //Time Input
        timeInput.onValueChanged.RemoveAllListeners();
        timeInput.text = subtopic.stimatedTime.ToString();
        timeInput.onValueChanged.AddListener(((newTime) => {
            subtopic.stimatedTime = int.Parse(newTime);
            UpdateTime();
        }));
        UpdateTime();
        //Satisfation Buttons
        for (int i = 0; i < satisfationButtons.Count; i++) {
            UIButton satisfationButton = satisfationButtons[i];
            int satisfationLevel = i;
            satisfationButton.GetComponentInChildren<Image>().color = subtopic.satisfaction == satisfationLevel ? Color.green : Color.white;
            satisfationButton.onClickEvent.RemoveAllListeners();
            satisfationButton.onClickEvent.AddListener(() => {
                subtopic.satisfaction = satisfationLevel;
                for (int j = 0; j < satisfationButtons.Count; j++) {
                    UIButton satisfationButtonAux = satisfationButtons[j];
                    satisfationButtonAux.GetComponentInChildren<Image>().color = subtopic.satisfaction == j ? Color.green : Color.white;
                }
            });
        }
    }

    #endregion

    #region WEEKS

    private void NextWeek() {
        if (currentWeek + 1 > maxWeeks - 1) {
            currentWeek = maxWeeks - 1;
        } else {
            currentWeek++;
            currentWeekText.text = "SEMANA " + (currentWeek + 1).ToString("D1");
            ShowCurrentWeekContent();
        }
    }

    private void PreviousWeek() {
        if (currentWeek - 1 >= 0) {
            currentWeek--;
            currentWeekText.text = "SEMANA " + (currentWeek + 1).ToString("D1");
            ShowCurrentWeekContent();
        } else {
            currentWeek = 0;
        }
    }

    private void ShowCurrentWeekContent() {
        //Retrieve keyIdeasToShow
        List<Transform> subtopicToShow = new List<Transform>();
        for (int i = 0; i < hiddenSubtopicsHolder.childCount; i++) {
            SupervisionSubtopicHolder supervisionSubtopicHolder = hiddenSubtopicsHolder.GetChild(i).GetComponent<SupervisionSubtopicHolder>();
            if (supervisionSubtopicHolder.CurrentSubTopic.week == currentWeek) {
                subtopicToShow.Add(supervisionSubtopicHolder.transform);
            }
        }

        //Retrieve keyIdeasToHide
        List<Transform> subtopicsToHide = new List<Transform>();
        for (int i = 0; i < coursesContentHolder.childCount; i++) {
            if (coursesContentHolder.GetChild(i).childCount != 0) {
                SupervisionCourseHolder supervisionCourseHolder = coursesContentHolder.GetChild(i).GetComponent<SupervisionCourseHolder>();
                for (int j = 0; j < supervisionCourseHolder.SubtopicsContentHolder.childCount; j++) {
                    SupervisionSubtopicHolder supervisionSubtopicHolder = supervisionCourseHolder.SubtopicsContentHolder.GetChild(j).GetComponent<SupervisionSubtopicHolder>();
                    if (supervisionSubtopicHolder.CurrentSubTopic.week != currentWeek) {
                        subtopicsToHide.Add(supervisionSubtopicHolder.transform);
                    }
                }
            }
        }

        //Swap places
        for (int i = 0; i < subtopicToShow.Count; i++) {
            subtopicToShow[i].parent = coursesContentHolder.GetChild(subtopicToShow[i].GetComponent<SupervisionSubtopicHolder>().CurrentSubTopic.courseID).GetComponent<SupervisionCourseHolder>().SubtopicsContentHolder;
            subtopicToShow[i].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }

        for (int i = 0; i < subtopicsToHide.Count; i++) {
            subtopicsToHide[i].parent = hiddenSubtopicsHolder;
            subtopicsToHide[i].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }

        UpdateTime(subtopicToShow);
        foregroundBlocker.SetActive(true);
    }

    private void UpdateTime(List<Transform> supervisionSubtopicsHolder = null) {
        if (supervisionSubtopicsHolder == null) {
            supervisionSubtopicsHolder = new List<Transform>();
            for (int i = 0; i < coursesContentHolder.childCount; i++) {
                if (coursesContentHolder.GetChild(i).childCount != 0) {
                    SupervisionCourseHolder supervisionCourseHolder = coursesContentHolder.GetChild(i).GetComponent<SupervisionCourseHolder>();
                    for (int j = 0; j < supervisionCourseHolder.SubtopicsContentHolder.childCount; j++) {
                        SupervisionSubtopicHolder supervisionSubtopicHolder = supervisionCourseHolder.SubtopicsContentHolder.GetChild(j).GetComponent<SupervisionSubtopicHolder>();
                        if (supervisionSubtopicHolder.CurrentSubTopic.week == currentWeek) {
                            supervisionSubtopicsHolder.Add(supervisionSubtopicHolder.transform);
                        }
                    }
                }
            }
        }

        int totalTimeValue = 0;
        for (int i = 0; i < supervisionSubtopicsHolder.Count; i++) {
            int timeValue = supervisionSubtopicsHolder[i].GetComponent<SupervisionSubtopicHolder>().CurrentSubTopic.stimatedTime;
            totalTimeValue += timeValue;
        }

        TimeSpan timeSpan = TimeSpan.FromMinutes(totalTimeValue);
        currentTimeText.text = timeSpan.ToString(@"hh\:mm");
    }

    #endregion
}