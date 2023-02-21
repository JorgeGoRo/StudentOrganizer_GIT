using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public enum StudyStrategy {
    NONE,
    PARAPHRASING,
    ANALOGIES,
    EXAMPLES,
    TAKING_NOTES,
    RESUME,
    SORT_OUT,
    CONCEPTUAL_MAP,
    SCHEME
}

[Serializable]
public class SubTopic {
    //Basic info
    [SerializeField] public string name;
    [SerializeField] public int week;
    [SerializeField] public string description;
    //Advanced info
    [SerializeField] public int courseID;
    [SerializeField] public int topicID;
    [SerializeField] public int subtopicID;
    [SerializeField] public int keyIdeaIndex;
    [SerializeField] public bool isKeyIdea;
    [SerializeField] public StudyStrategy studyStrategy;
    [SerializeField] public StudyStrategy extraStudyStrategy;
    [SerializeField] public int stimatedTime;
    [SerializeField] public int satisfaction; //0 worst, 2 best
    

    [JsonConstructor]
    public SubTopic(string name, int week, string description) {
        this.name = name;
        this.week = week;
        this.description = description;
        this.isKeyIdea = false;
        this.studyStrategy = StudyStrategy.NONE;
        this.extraStudyStrategy = StudyStrategy.NONE;
        this.stimatedTime = 0;
        this.satisfaction = -1;
    }

    public SubTopic(string name, int week, string description, int courseID, int topicID, int subtopicID, int keyIdeaIndex, bool isKeyIdea, StudyStrategy studyStrategy, StudyStrategy extraStudyStrategy, int stimatedTime, int satisfaction) {
        this.name = name;
        this.week = week;
        this.description = description;
        this.courseID = courseID;
        this.topicID = topicID;
        this.subtopicID = subtopicID;
        this.keyIdeaIndex = keyIdeaIndex;
        this.isKeyIdea = isKeyIdea;
        this.studyStrategy = studyStrategy;
        this.extraStudyStrategy = extraStudyStrategy;
        this.stimatedTime = stimatedTime;
        this.satisfaction = satisfaction;
    }

    
}