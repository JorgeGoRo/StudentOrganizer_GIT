using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.Colors.Models;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LastReviewManager : BaseManager {
    [FoldoutGroup("UI")] [FoldoutGroup("UI/Holders")] [SerializeField]
    private Transform lastRevSubtopicContentHolder;

    [FoldoutGroup("UI/Holders")] [SerializeField]
    private Transform lastRevSubtopicHiddenHolder;

    [FoldoutGroup("UI/Holders")] [SerializeField]
    private GameObject foregroundBlocker;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton closeLastReviewButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private List<UIButton> reviewCourseButtons;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private List<UIButton> filterButtons;

    [FoldoutGroup("UI/Dropdown")] [SerializeField]
    private TMP_Dropdown extraStrategyDropdown;

    [FoldoutGroup("UI/Labels")] [SerializeField]
    private TextMeshProUGUI keyIdeaText;

    [FoldoutGroup("UI/Labels")] [SerializeField]
    private TextMeshProUGUI studyStrategyText;

    [FoldoutGroup("UI/Prefabs")] [SerializeField]
    private GameObject lastRevSubtopicPrefab;

    [FoldoutGroup("UI/Prefabs")] [SerializeField]
    private Color selectedColor = Color.red;

    [FoldoutGroup("UI/Colors")] [SerializeField]
    private Color filterSelectedColor = Color.green;

    private void Start() {
        SetUIFunctionality();
        CleanUI();
    }

    private void CleanUI() {
        for (int i = 0; i < lastRevSubtopicContentHolder.childCount; i++) {
            Destroy(lastRevSubtopicContentHolder.GetChild(i).gameObject);
        }

        for (int i = 0; i < lastRevSubtopicHiddenHolder.childCount; i++) {
            Destroy(lastRevSubtopicHiddenHolder.GetChild(i).gameObject);
        }
    }

    private void SetUIFunctionality() {
        for (int i = 0; i < reviewCourseButtons.Count; i++) {
            UIButton reviewCourseButton = reviewCourseButtons[i];
            int courseIndex = i;
            reviewCourseButton.onClickEvent.RemoveAllListeners();
            reviewCourseButton.onClickEvent.AddListener(() => {
                for (int j = 0; j < reviewCourseButtons.Count; j++) {
                    reviewCourseButtons[j].GetComponentInChildren<Image>().color = Color.white;
                }

                reviewCourseButton.GetComponentInChildren<Image>().color = GameManager.CourseColors[courseIndex];
                CreateContent(courseIndex);
            });
        }

        for (int i = 0; i < filterButtons.Count; i++) {
            UIButton filterButton = filterButtons[i];
            int satistaction = i;
            filterButton.onClickEvent.RemoveAllListeners();
            filterButton.onClickEvent.AddListener(() => { FilterBySatisfaction(satistaction); });
        }

        closeLastReviewButton.onClickEvent.RemoveAllListeners();
        closeLastReviewButton.onClickEvent.AddListener(() => { GameManager.GetManager<StepSelector>().OpenView(Steps.AUTO_EVALUATION); });
    }

    private void SetExtraStrategy(SubTopic subTopic, int strategyIndex) {
        throw new System.NotImplementedException();
    }

    public void OpenView() {
        List<Course> courses = GameManager.GetManager<CourseReader>().Courses;
        for (int i = 0; i < reviewCourseButtons.Count; i++) {
            reviewCourseButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = courses[i].name;
        }

        reviewCourseButtons[0].onClickEvent?.Invoke();
        // CreateContent();
    }

    private void CreateContent(int courseIndex = 0) {
        foregroundBlocker.SetActive(true);
        CleanUI();
        Course course = GameManager.GetManager<CourseReader>().Courses[courseIndex];
        List<Topic> topics = course.topics;
        for (int i = 0; i < topics.Count; i++) {
            Topic topic = topics[i];
            for (int j = 0; j < topic.subTopics.Count; j++) {
                SubTopic subTopic = topic.subTopics[j];
                if (subTopic.isKeyIdea) {
                    LastReviewSubtopicHolder lrSubtopicHolder = Instantiate(lastRevSubtopicPrefab, lastRevSubtopicContentHolder).GetComponent<LastReviewSubtopicHolder>();
                    lrSubtopicHolder.FillSubtopic(subTopic, SetSubtopicData);
                }
            }
        }

        FilterBySatisfaction();
    }

    private void SetSubtopicData(SubTopic subTopic = null) {
        foregroundBlocker.SetActive(false);
        keyIdeaText.text = subTopic == null ? "" : subTopic.description;
        studyStrategyText.text = subTopic == null ? "-" : StudyStrategyConst.STRATEGIES[subTopic.studyStrategy];
        extraStrategyDropdown.SetValueWithoutNotify((int) subTopic.extraStudyStrategy);
        extraStrategyDropdown.onValueChanged.RemoveAllListeners();
        extraStrategyDropdown.onValueChanged.AddListener((strategyIndex) => {
            subTopic.extraStudyStrategy = (StudyStrategy) strategyIndex;
            // SetExtraStrategy(subTopic, strategyIndex);
        });
    }

    private void FilterBySatisfaction(int satisfaction = 2) {
        foregroundBlocker.SetActive(true);
        //Change colors
        for (int i = 0; i < filterButtons.Count; i++) {
            filterButtons[i].GetComponentInChildren<Image>().color = satisfaction == i ? filterSelectedColor : Color.white;
        }

        //Get current ones
        List<Transform> currentSubtopicHolders = new List<Transform>();
        for (int i = 0; i < lastRevSubtopicContentHolder.childCount; i++) {
            currentSubtopicHolders.Add(lastRevSubtopicContentHolder.GetChild(i));
        }

        //Get hidden ones
        List<Transform> hiddenSubtopicHolders = new List<Transform>();
        for (int i = 0; i < lastRevSubtopicHiddenHolder.childCount; i++) {
            LastReviewSubtopicHolder lrSubtopicHolder = lastRevSubtopicHiddenHolder.GetChild(i).GetComponent<LastReviewSubtopicHolder>();
            if (lrSubtopicHolder.CurrentSubTopic.satisfaction == satisfaction) {
                hiddenSubtopicHolders.Add(lastRevSubtopicHiddenHolder.GetChild(i));
            }
        }

        //Swap
        for (int i = 0; i < currentSubtopicHolders.Count; i++) {
            currentSubtopicHolders[i].parent = lastRevSubtopicHiddenHolder;
        }

        for (int i = 0; i < hiddenSubtopicHolders.Count; i++) {
            hiddenSubtopicHolders[i].parent = lastRevSubtopicContentHolder;
        }
    }
}