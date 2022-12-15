using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topic {
    public string topicName;
    public List<SubTopic> subTopics;

    public Topic(string topicName, List<SubTopic> subTopics) {
        this.topicName = topicName;
        this.subTopics = subTopics;
    }
}
