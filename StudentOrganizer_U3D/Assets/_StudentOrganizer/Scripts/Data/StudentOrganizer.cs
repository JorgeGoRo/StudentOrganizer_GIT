using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class StudentOrganizer {
    [SerializeField] public bool isNewFile;
    [SerializeField] public List<Course> courses;

    [JsonConstructor]
    public StudentOrganizer(bool isNewFile, List<Course> courses) {
        this.isNewFile = isNewFile;
        this.courses = courses;
    }
}