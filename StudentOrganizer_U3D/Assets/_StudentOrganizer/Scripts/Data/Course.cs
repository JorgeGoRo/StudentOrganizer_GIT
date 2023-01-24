using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Course {
    [SerializeField] public string name;
    [SerializeField] public List<Topic> topics;

    /*[JsonIgnore]*/
    public int courseID;

    [JsonConstructor]
    public Course(string name, List<Topic> topics) {
        this.name = name;
        this.topics = topics;
    }

    public Course(string name, List<Topic> topics, int courseID) {
        this.name = name;
        this.topics = topics;
        this.courseID = courseID;
    }
}