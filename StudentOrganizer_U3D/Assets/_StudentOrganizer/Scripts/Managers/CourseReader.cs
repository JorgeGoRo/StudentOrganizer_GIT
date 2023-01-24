using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Doozy.Runtime.UIManager.Components;
using Newtonsoft.Json;
using SimpleFileBrowser;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class CourseReader : BaseManager {
    [FoldoutGroup("Settings")] [SerializeField]
    private bool showFileBrowser = false;

    [FoldoutGroup("UI")] [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton saveButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton loadButton;

    [FoldoutGroup("Debug")] [SerializeField]
    private StudentOrganizer studentOrganizer;

    public List<Course> Courses {
        get { return studentOrganizer.courses; }
    }

    private void Start() {
        SetUIFunctionality();
    }

    private void SetUIFunctionality() {
        saveButton.onClickEvent.AddListener(() => { SaveFile(); });
        loadButton.onClickEvent.AddListener(() => { LoadFile(GameManager.GetManager<CourseSelector>().CreateContent); });
    }

    #region MODIFY COURSES

    public void SetSubtopicAsUsed(SubTopic targetSubtopic, bool isUsed) {
        var course = studentOrganizer.courses[targetSubtopic.courseID];
        var topic = course.topics[targetSubtopic.topicID];
        var subTopic = topic.subTopics[targetSubtopic.subtopicID];
        subTopic.isKeyIdea = isUsed;
        subTopic.week = isUsed ? GameManager.GetManager<KeyIdeaSelector>().CurrentWeek : -1;
        subTopic.keyIdeaIndex = isUsed ? targetSubtopic.keyIdeaIndex : -1;
        GameManager.GetManager<CourseSelector>().UpdateSubtopicHolderStatus(subTopic);
    }

    #endregion

    #region READ COURSES

    public void ReadCourses(UnityAction OnFinishCallback) {
        if (showFileBrowser) {
            LoadFile(OnFinishCallback);
        } else {
            CreateTestCourses();
        }
    }

    private void SaveFile() {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Course Files", ".json"));
        FileBrowser.AddQuickLink("C", "C:", null);
        StartCoroutine(ShowSaveDialogCoroutine(null));
    }

    private void LoadFile(UnityAction OnFinishCallback) {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Course Files", ".json"));
        FileBrowser.AddQuickLink("C", "C:", null);
        StartCoroutine(ShowLoadDialogCoroutine(OnFinishCallback));
    }

    IEnumerator ShowLoadDialogCoroutine(UnityAction OnFinishCallback) {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, false, null, null, "Load Files and Folders", "Load");
        if (FileBrowser.Success) {
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
            string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
            FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);

            string result = System.Text.Encoding.UTF8.GetString(bytes);
            ParseCourses(result, OnFinishCallback);
        }
    }

    IEnumerator ShowSaveDialogCoroutine(UnityAction OnFinishCallback) {
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.FilesAndFolders, false, null, ".json", "Save File", "Save");
        if (FileBrowser.Success) {
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);
            string path = FileBrowserHelpers.GetDirectoryName(FileBrowser.Result[0]);
            Debug.Log("Path: " + path);
            string fileName = FileBrowserHelpers.GetFilename(FileBrowser.Result[0]);
            Debug.Log("FileName: " + fileName);
            if (!FileBrowserHelpers.FileExists(fileName)) {
                FileBrowserHelpers.CreateFileInDirectory(path, fileName);
            }

            string studentOrganizerJson = JsonConvert.SerializeObject(studentOrganizer);
            Debug.Log("StudentOrganizerJson: " + studentOrganizerJson);
            FileBrowserHelpers.WriteTextToFile(path + "\\" + fileName, studentOrganizerJson);
        }
    }

    private void ParseCourses(string coursesJson, UnityAction OnFinishCallback) {
        studentOrganizer = JsonConvert.DeserializeObject<StudentOrganizer>(coursesJson);

        if (studentOrganizer.isNewFile) {
            ResetStudentOrganizer();
        }

        OnFinishCallback?.Invoke();
    }

    private void ResetStudentOrganizer() {
        //Assign parentID to child structures
        for (int i = 0; i < studentOrganizer.courses.Count; i++) {
            studentOrganizer.courses[i].courseID = i;
            for (int j = 0; j < studentOrganizer.courses[i].topics.Count; j++) {
                studentOrganizer.courses[i].topics[j].courseID = i;
                for (int k = 0; k < studentOrganizer.courses[i].topics[j].subTopics.Count; k++) {
                    studentOrganizer.courses[i].topics[j].subTopics[k].courseID = i;
                    studentOrganizer.courses[i].topics[j].subTopics[k].topicID = j;
                    studentOrganizer.courses[i].topics[j].subTopics[k].subtopicID = k;
                    studentOrganizer.courses[i].topics[j].subTopics[k].week = -1;
                    studentOrganizer.courses[i].topics[j].subTopics[k].keyIdeaIndex = -1;
                    studentOrganizer.courses[i].topics[j].subTopics[k].isKeyIdea = false;
                }
            }
        }

        studentOrganizer.isNewFile = false;
    }

    private void CreateTestCourses() {
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

        List<Course> coursesAux = new List<Course>();

        coursesAux.Add(new Course("Programación", topics1));
        coursesAux.Add(new Course("Emprendimiento", topics2));
        coursesAux.Add(new Course("Neurociencia", topics3));
        studentOrganizer = new StudentOrganizer(true, coursesAux);

        Debug.Log(JsonConvert.SerializeObject(studentOrganizer));
    }

    #endregion
}