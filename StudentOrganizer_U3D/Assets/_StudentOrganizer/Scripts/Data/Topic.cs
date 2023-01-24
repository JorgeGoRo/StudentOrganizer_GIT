using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Topic {
    [SerializeField] public string topicName;
    [SerializeField] public List<SubTopic> subTopics;

    /*[JsonIgnore]*/
    public int courseID;

    [JsonConstructor]
    public Topic(string topicName, List<SubTopic> subTopics) {
        this.topicName = topicName;
        this.subTopics = subTopics;
    }

    public Topic(string topicName, List<SubTopic> subTopics, int courseID) {
        this.topicName = topicName;
        this.subTopics = subTopics;
        this.courseID = courseID;
    }
}