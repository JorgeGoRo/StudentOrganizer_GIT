using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleFileBrowser;
using UnityEngine;

public class CourseReader : BaseManager {
    
    public bool showFileBrowser = false;
    public List<Course> courses;

    private void Start() {
        if (showFileBrowser) {
            FileBrowser.SetFilters(true, new FileBrowser.Filter("Course Files", ".json", ".csv"));
            FileBrowser.AddQuickLink("C", "C:", null);
            StartCoroutine(ShowLoadDialogCoroutine());
        }
    }

    IEnumerator ShowLoadDialogCoroutine() {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");
        if (FileBrowser.Success) {
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

            string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
            FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
        }
    }

    public void GenerateCourses() {
        //SUBTOPICS
        List<SubTopic> subTopics1 = new List<SubTopic>();
        subTopics1.Add(new SubTopic("1.1", 0, "Descripción 1.1"));
        subTopics1.Add(new SubTopic("1.2", 0, "Descripción 1.2"));
        subTopics1.Add(new SubTopic("1.3", 0, "Descripción 1.3"));

        List<SubTopic> subTopics2 = new List<SubTopic>();
        subTopics2.Add(new SubTopic("2.1", 0, "Descripción 2.1"));
        subTopics2.Add(new SubTopic("2.2", 0, "Descripción 2.2"));
        subTopics2.Add(new SubTopic("2.3", 0, "Descripción 2.3"));
        subTopics2.Add(new SubTopic("2.4", 0, "Descripción 2.4"));

        List<SubTopic> subTopics3 = new List<SubTopic>();
        subTopics3.Add(new SubTopic("3.1", 0, "Descripción 3.1"));
        subTopics3.Add(new SubTopic("3.2", 0, "Descripción 3.2"));
        subTopics3.Add(new SubTopic("3.3", 0, "Descripción 3.3"));
        subTopics3.Add(new SubTopic("3.4", 0, "Descripción 3.4"));
        subTopics3.Add(new SubTopic("3.5", 0, "Descripción 3.5"));
            
        //TOPICS
        List<Topic> topics1 = new List<Topic>();
        topics1.Add(new Topic("Tema 1", subTopics1));
            
        List<Topic> topics2 = new List<Topic>();
        topics2.Add(new Topic("Tema 1", subTopics1));
        topics2.Add(new Topic("Tema 2", subTopics2));
            
        List<Topic> topics3 = new List<Topic>();
        topics3.Add(new Topic("Tema 1", subTopics1));
        topics3.Add(new Topic("Tema 2", subTopics2));
        topics3.Add(new Topic("Tema 3", subTopics3));
            
        courses = new List<Course>();
            
        courses.Add(new Course("Programación", topics1));
        courses.Add(new Course("Emprendimiento", topics2));
        courses.Add(new Course("Neurociencia", topics3));
    }
}