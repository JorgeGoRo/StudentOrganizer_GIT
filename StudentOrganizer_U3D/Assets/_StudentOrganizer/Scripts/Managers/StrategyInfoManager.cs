using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StrategyInfoManager : BaseManager {
    [FoldoutGroup("UI")] [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton paraphraseButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton analogyButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton exampleButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton takingNotesButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton resumeButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton sourtOutButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton conceptualMapButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton schemeButton;

    [FoldoutGroup("UI/Text")] [SerializeField]
    private TextMeshProUGUI explanationText;

    [FoldoutGroup("UI/Text")] [SerializeField]
    private ScrollRect textScrollRect;

    private void Start() {
        SetUIFunctionality();
        paraphraseButton.onClickEvent?.Invoke();
    }

    private void SetUIFunctionality() {
        paraphraseButton.onClickEvent.AddListener(() => { SetExplanationText(StudyStrategy.PARAPHRASING); });
        analogyButton.onClickEvent.AddListener(() => { SetExplanationText(StudyStrategy.ANALOGIES); });
        exampleButton.onClickEvent.AddListener(() => { SetExplanationText(StudyStrategy.EXAMPLES); });
        takingNotesButton.onClickEvent.AddListener(() => { SetExplanationText(StudyStrategy.TAKING_NOTES); });
        resumeButton.onClickEvent.AddListener(() => { SetExplanationText(StudyStrategy.RESUME); });
        sourtOutButton.onClickEvent.AddListener(() => { SetExplanationText(StudyStrategy.SORT_OUT); });
        conceptualMapButton.onClickEvent.AddListener(() => { SetExplanationText(StudyStrategy.CONCEPTUAL_MAP); });
        schemeButton.onClickEvent.AddListener(() => { SetExplanationText(StudyStrategy.SCHEME); });
    }

    private void SetExplanationText(StudyStrategy studyStrategy) {
        paraphraseButton.GetComponentInChildren<Image>().color = studyStrategy == StudyStrategy.PARAPHRASING ? Color.blue : Color.white;
        analogyButton.GetComponentInChildren<Image>().color = studyStrategy == StudyStrategy.ANALOGIES ? Color.blue : Color.white;
        exampleButton.GetComponentInChildren<Image>().color = studyStrategy == StudyStrategy.EXAMPLES ? Color.blue : Color.white;
        takingNotesButton.GetComponentInChildren<Image>().color = studyStrategy == StudyStrategy.TAKING_NOTES ? Color.blue : Color.white;
        resumeButton.GetComponentInChildren<Image>().color = studyStrategy == StudyStrategy.RESUME ? Color.blue : Color.white;
        sourtOutButton.GetComponentInChildren<Image>().color = studyStrategy == StudyStrategy.SORT_OUT ? Color.blue : Color.white;
        conceptualMapButton.GetComponentInChildren<Image>().color = studyStrategy == StudyStrategy.CONCEPTUAL_MAP ? Color.blue : Color.white;
        schemeButton.GetComponentInChildren<Image>().color = studyStrategy == StudyStrategy.SCHEME ? Color.blue : Color.white;
        switch (studyStrategy) {
            case StudyStrategy.PARAPHRASING: {
                explanationText.text = StudyStrategyConst.PARAPHRASING_TITLE + " \n\n";
                explanationText.text += StudyStrategyConst.PARAPHRASING;
                break;
            }
            case StudyStrategy.ANALOGIES: {
                explanationText.text = StudyStrategyConst.ANALOGIES_TITLE + " \n\n";
                explanationText.text += StudyStrategyConst.ANALOGIES;
                break;
            }
            case StudyStrategy.EXAMPLES: {
                explanationText.text = StudyStrategyConst.EXAMPLES_TITLE + " \n\n";
                explanationText.text += StudyStrategyConst.EXAMPLES;
                break;
            }
            case StudyStrategy.TAKING_NOTES: {
                explanationText.text = StudyStrategyConst.TAKING_NOTES_TITLE + " \n\n";
                explanationText.text += StudyStrategyConst.TAKING_NOTES;
                break;
            }
            case StudyStrategy.RESUME: {
                explanationText.text = StudyStrategyConst.RESUME_TITLE + " \n\n";
                explanationText.text += StudyStrategyConst.RESUME;
                break;
            }
            case StudyStrategy.SORT_OUT: {
                explanationText.text = StudyStrategyConst.SORT_OUT_TITLE + " \n\n";
                explanationText.text += StudyStrategyConst.SORT_OUT;
                break;
            }
            case StudyStrategy.CONCEPTUAL_MAP: {
                explanationText.text = StudyStrategyConst.CONCEPTUAL_MAP_TITLE + " \n\n";
                explanationText.text += StudyStrategyConst.CONCEPTUAL_MAP;
                break;
            }
            case StudyStrategy.SCHEME: {
                explanationText.text = StudyStrategyConst.SCHEME_TITLE + " \n\n";
                explanationText.text += StudyStrategyConst.SCHEME;
                break;
            }
        }

        textScrollRect.verticalNormalizedPosition = 1f;
    }
}