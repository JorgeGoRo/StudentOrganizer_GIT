using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Doozy.Runtime.UIManager.Components;
using Newtonsoft.Json;
using SimpleFileBrowser;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CourseReader : BaseManager {
    [FoldoutGroup("UI")] [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton saveButton;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private List<UIButton> loadButtons;

    [FoldoutGroup("UI/Buttons")] [SerializeField]
    private UIButton closeAppButton;

    [FoldoutGroup("Debug")] [SerializeField]
    private StudentOrganizer studentOrganizer;

    public List<Course> Courses {
        get { return studentOrganizer.courses; }
    }

    private string filePath;
    private bool isNewFile = true;

    private void Start() {
        SetUIFunctionality();
    }

    private void SetUIFunctionality() {
        saveButton.onClickEvent.RemoveAllListeners();
        saveButton.onClickEvent.AddListener(() => { SaveFile(); });
        for (int i = 0; i < loadButtons.Count; i++) {
            UIButton loadButton = loadButtons[i];
            loadButton.onClickEvent.RemoveAllListeners();
            loadButton.onClickEvent.AddListener(() => { LoadFile(() => { GameManager.GetManager<StepSelector>().OpenView(Steps.PLANIFICATION); }); });
        }

        closeAppButton.onClickEvent.RemoveAllListeners();
        closeAppButton.onClickEvent.AddListener(() => { SaveFile(() => Application.Quit()); });
    }

    #region MODIFY COURSES

    public void SetSubtopicAsUsed(SubTopic targetSubtopic, bool isKeyIdea) {
        var course = studentOrganizer.courses[targetSubtopic.courseID];
        var topic = course.topics[targetSubtopic.topicID];
        var subTopic = topic.subTopics[targetSubtopic.subtopicID];
        subTopic.isKeyIdea = isKeyIdea;
        subTopic.week = isKeyIdea ? (GameManager.GetManager<KeyIdeaManager>().CurrentWeek + 1) : subTopic.week;
        subTopic.keyIdeaIndex = isKeyIdea ? targetSubtopic.keyIdeaIndex : -1;
        GameManager.GetManager<PlanificationManager>().UpdateSubtopicHolderStatus(subTopic);
    }

    #endregion

    #region READ COURSES

    public void SaveFile(UnityAction onFinishCallback = null) {
        Debug.Log("SAVING FILE");
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Course Files", ".json"));
        #if UNITY_ANDROID
        FileBrowser.AddQuickLink("C", Application.persistentDataPath, null);
        #endif
        #if !UNITY_ANDROID
        FileBrowser.AddQuickLink("C", "C:", null);
        #endif
        
        StartCoroutine(ShowSaveDialogCoroutine(onFinishCallback));
        // StartCoroutine(SaveNoDialogCoroutine(null));
    }

    public void LoadFile(UnityAction OnFinishCallback) {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Course Files", ".json"));
#if UNITY_ANDROID
        FileBrowser.AddQuickLink("Files", Application.persistentDataPath, null);
#endif
#if !UNITY_ANDROID
        FileBrowser.AddQuickLink("Files", "C:", null);
#endif
        StartCoroutine(ShowLoadDialogCoroutine(OnFinishCallback));
    }

    IEnumerator ShowLoadDialogCoroutine(UnityAction OnFinishCallback) {
        #if UNITY_ANDROID
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, false, Application.persistentDataPath + "/..", null, "Load Files and Folders", "Load");
        #endif
        #if !UNITY_ANDROID
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, false, null, null, "Load Files and Folders", "Load");
        #endif
        if (FileBrowser.Success) {
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
            string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
            filePath = FileBrowser.Result[0];
            #if !UNITY_ANDROID
            FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
            #endif
            string result = System.Text.Encoding.UTF8.GetString(bytes);
            ParseCourses(FileBrowserHelpers.GetFilename(FileBrowser.Result[0]), result, OnFinishCallback);
        }
    }

    IEnumerator ShowSaveDialogCoroutine(UnityAction OnFinishCallback) {
        string newFileName = FileBrowserHelpers.GetFilename(filePath).Replace(".json", "");
        newFileName += isNewFile ? "_estudiante.json" : ".json";
        yield return null;
        string studentOrganizerJson = JsonConvert.SerializeObject(studentOrganizer);
        Debug.Log("StudentOrganizerJson: " + studentOrganizerJson);
        FileBrowserHelpers.WriteTextToFile(FileBrowserHelpers.GetDirectoryName(FileBrowser.Result[0]) + "\\" + newFileName, studentOrganizerJson);
        OnFinishCallback?.Invoke();
    }

    // IEnumerator SaveNoDialogCoroutine(UnityAction OnFinishCallback) {
    //     yield return null;
    //     fileName = userNameInput.text;
    //     if (!FileBrowserHelpers.FileExists(fileName)) {
    //         FileBrowserHelpers.CreateFileInDirectory(filePath, fileName);
    //     }
    //
    //     string studentOrganizerJson = JsonConvert.SerializeObject(studentOrganizer);
    //     Debug.Log("StudentOrganizerJson: " + studentOrganizerJson);
    //     FileBrowserHelpers.WriteTextToFile(filePath + "\\" + fileName, studentOrganizerJson);
    // }

    private void ParseCourses(string fileName, string coursesJson, UnityAction OnFinishCallback) {
        studentOrganizer = JsonConvert.DeserializeObject<StudentOrganizer>(coursesJson);

        if (studentOrganizer.isNewFile) {
            isNewFile = true;
            ResetStudentOrganizer();
        } else {
            isNewFile = false;
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
                    // studentOrganizer.courses[i].topics[j].subTopics[k].week--;
                    studentOrganizer.courses[i].topics[j].subTopics[k].keyIdeaIndex = -1;
                    studentOrganizer.courses[i].topics[j].subTopics[k].isKeyIdea = false;
                }
            }
        }

        studentOrganizer.isNewFile = false;
        // SaveFile(false);
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