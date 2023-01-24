using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class KeyIdeaSelector : BaseManager {
    [FoldoutGroup("UI")] [FoldoutGroup("UI/Holders")] [SerializeField]
    private Transform keyIdeasHolder;

    [FoldoutGroup("UI/Holders")] [SerializeField]
    private Transform hiddenKeyIdeasHolder;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton previousButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton nextButton;

    [FoldoutGroup("UI/Labels")] [SerializeField]
    private TextMeshProUGUI currentWeekText;

    [FoldoutGroup("Debug")] [SerializeField]
    private int maxWeeks = 15;

    [FoldoutGroup("Debug")] [SerializeField]
    private int currentWeek = 0;

    public int CurrentWeek => currentWeek;

    private void Start() {
        SetUIFunctionality();
    }

    private void SetUIFunctionality() {
        currentWeekText.text = "SEMANA " + (currentWeek + 1).ToString("D1");
        previousButton.onClickEvent.AddListener(PreviousWeek);
        nextButton.onClickEvent.AddListener(NextWeek);
    }

    private void NextWeek() {
        if (currentWeek + 1 > maxWeeks - 1) {
            currentWeek = maxWeeks - 1;
        } else {
            currentWeek++;
            currentWeekText.text = "SEMANA " + (currentWeek + 1).ToString("D1");
            ShowCurrentWeekKeyIdeas();
        }
    }

    private void PreviousWeek() {
        if (currentWeek - 1 >= 0) {
            currentWeek--;
            currentWeekText.text = "SEMANA " + (currentWeek + 1).ToString("D1");
            ShowCurrentWeekKeyIdeas();
        } else {
            currentWeek = 0;
        }
    }

    private void ShowCurrentWeekKeyIdeas() {
        //Retrieve keyIdeasToShow
        List<Transform> keyIdeasToShow = new List<Transform>();
        for (int i = 0; i < hiddenKeyIdeasHolder.childCount; i++) {
            KeyIdeaHolder keyIdeaHolder = hiddenKeyIdeasHolder.GetChild(i).GetComponent<KeyIdeaHolder>();
            if (keyIdeaHolder.CurrentSubTopic.week == currentWeek) {
                keyIdeasToShow.Add(keyIdeaHolder.transform);
            }
        }

        //Retrieve keyIdeasToHide
        List<Transform> keyIdeasToHide = new List<Transform>();
        for (int i = 0; i < keyIdeasHolder.childCount; i++) {
            if (keyIdeasHolder.GetChild(i).childCount != 0) {
                KeyIdeaHolder keyIdeaHolder = keyIdeasHolder.GetChild(i).GetChild(0).GetComponent<KeyIdeaHolder>();
                keyIdeasToHide.Add(keyIdeaHolder.transform);
            }
        }

        //Swap places
        for (int i = 0; i < keyIdeasToShow.Count; i++) {
            keyIdeasToShow[i].parent = keyIdeasHolder.GetChild(keyIdeasToShow[i].GetComponent<KeyIdeaHolder>().CurrentSubTopic.keyIdeaIndex);
            keyIdeasToShow[i].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }

        for (int i = 0; i < keyIdeasToHide.Count; i++) {
            keyIdeasToHide[i].parent = hiddenKeyIdeasHolder;
            keyIdeasToHide[i].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
    }

    public void HideKeyIdeaIfNeeded(KeyIdeaHolder keyIdeaToHide) {
        if (keyIdeaToHide.CurrentSubTopic.week != currentWeek) {
            keyIdeaToHide.transform.parent = hiddenKeyIdeasHolder;
            keyIdeaToHide.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
    }

    public Transform GetKeyIdeaHolderByIndex(int keyIdeaIndex) {
        return keyIdeasHolder.GetChild(keyIdeaIndex);
    }
}