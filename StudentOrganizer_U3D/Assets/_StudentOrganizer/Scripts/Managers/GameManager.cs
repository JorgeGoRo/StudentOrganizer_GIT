using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Better.StreamingAssets;
using SimpleFileBrowser;

public class GameManager : MonoBehaviour {

    [ReadOnly] public string fileName;
    
    [FoldoutGroup("UI/Colors")] [SerializeField]
    private List<Color> courseColors = new(){Color.blue, Color.red, Color.green};

    public List<Color> CourseColors {
        get {
            return courseColors;
        }
    }
    
    private List<BaseManager> _managers;
    private Dictionary<Type, BaseManager>_managersDictionary;

    //Singleton
    private static GameManager _instance;
    [HideInInspector] public static GameManager Instance { get { return _instance; } }
  
    private void Awake() {
        if (_instance != null && _instance != this) { Destroy(this.gameObject); } else { _instance = this; }
        DontDestroyOnLoad(gameObject);
        #if UNITY_ANDROID
        CopyAssets();
        #endif
        GetManagers();
    }

    private void CopyAssets() {
        BetterStreamingAssets.Initialize();
        string file = BetterStreamingAssets.GetFiles("/")[0];
        string text = BetterStreamingAssets.ReadAllText("/" + file);
        FileBrowserHelpers.WriteTextToFile(FileBrowserHelpers.GetDirectoryName(Application.persistentDataPath) + "/" + file, text);
        
        Debug.Log(Application.persistentDataPath);
    }

    private void Start() {
        // GetManager<CourseReader>().LoadFile(() => {
        //     GetManager<StepSelector>().OpenView(Steps.PLANIFICATION);
        //     //GetManager<CourseSelector>().CreateContent();
        // });
    }

    public void GetManagers() {
        _managers = new List<BaseManager>(GetComponentsInChildren<BaseManager>());
        _managersDictionary = new Dictionary<Type, BaseManager>();
        foreach (BaseManager manager in _managers) {
            _managersDictionary.Add(manager.GetType(), manager);
            manager.GameManager = this;
        }
    }
    public T GetManager<T>() where T : BaseManager {

        T manager = _managersDictionary.ContainsKey(typeof(T)) ? (T)_managersDictionary[typeof(T)] : null;
        if (manager == null) {
            foreach (var manPair in _managersDictionary) {
                if (typeof(T).IsAssignableFrom(manPair.Key)) {
                    manager = (T)manPair.Value;
                }
            }
        }
        return manager;
    }
}
