using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CourseSelector : BaseManager {

    [SerializeField] private List<TextMeshProUGUI> courseNamesText;
    [SerializeField] private List<CourseHolder> courseHolders;

    public void FillContent() {
        for (int i = 0; i < GameManager.GetManager<CourseReader>().courses.Count; i++) {
            Course course = GameManager.GetManager<CourseReader>().courses[i];
            courseHolders[i].FillCourse(course);
            courseNamesText[i].text = course.name;
        }
    }
}