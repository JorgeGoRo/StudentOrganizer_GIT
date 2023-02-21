using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public enum Steps {
    FIRST_VIEW,
    PLANIFICATION,
    SUPERVISION,
    AUTO_EVALUATION,
    LAST_REVIEW
}

public class StepSelector : BaseManager {
    [FoldoutGroup("UI")] [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton planificationButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton supervisionButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton autoEvaluationButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton lastReviewButton;

    [FoldoutGroup("UI/Views")] [SerializeField]
    private UIContainer firstView;

    [FoldoutGroup("UI/Views")] [SerializeField] private UIContainer planificationView;

    [FoldoutGroup("UI/Views")] [SerializeField]
    private UIContainer supervisionView;

    [FoldoutGroup("UI/Views")] [SerializeField]
    private UIContainer autoEvaluationView;

    [FoldoutGroup("UI/Views")] [SerializeField]
    private UIContainer lastReviewView;

    private void Start() {
        SetUIFunctionality();
    }

    private void SetUIFunctionality() {
        planificationButton.onClickEvent.RemoveAllListeners();
        planificationButton.onClickEvent.AddListener(() => OpenView(Steps.PLANIFICATION));
        supervisionButton.onClickEvent.RemoveAllListeners();
        supervisionButton.onClickEvent.AddListener(() => OpenView(Steps.SUPERVISION));
        autoEvaluationButton.onClickEvent.RemoveAllListeners();
        autoEvaluationButton.onClickEvent.AddListener(() => OpenView(Steps.AUTO_EVALUATION));
        lastReviewButton.onClickEvent.RemoveAllListeners();
        lastReviewButton.onClickEvent.AddListener(() => OpenView(Steps.LAST_REVIEW));
    }

    public void OpenView(Steps currentStep) {
        firstView.Hide();
        planificationView.Hide();
        supervisionView.Hide();
        autoEvaluationView.Hide();
        lastReviewView.Hide();
        if (currentStep == Steps.FIRST_VIEW) {
            firstView.Show();
        }
        
        if (currentStep == Steps.PLANIFICATION) {
            planificationView.Show();
            GameManager.GetManager<PlanificationManager>().OpenView();
        }

        if (currentStep == Steps.SUPERVISION) {
            supervisionView.Show();
            GameManager.GetManager<SupervisionManager>().OpenView();
        }

        if (currentStep == Steps.AUTO_EVALUATION) {
            autoEvaluationView.Show();
            GameManager.GetManager<AutoEvaluationManager>().OpenView();
        }

        if (currentStep == Steps.LAST_REVIEW) {
            lastReviewView.Show();
            GameManager.GetManager<LastReviewManager>().OpenView();
        }
    }
}