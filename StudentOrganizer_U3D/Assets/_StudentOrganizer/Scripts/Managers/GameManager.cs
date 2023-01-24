using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private List<BaseManager> _managers;
    private Dictionary<Type, BaseManager>_managersDictionary;

    //Singleton
    private static GameManager _instance;
    [HideInInspector] public static GameManager Instance { get { return _instance; } }
  
    private void Awake() {
        if (_instance != null && _instance != this) { Destroy(this.gameObject); } else { _instance = this; }
        DontDestroyOnLoad(gameObject);
        GetManagers();
    }

    private void Start() {
        GetManager<CourseReader>().ReadCourses(() => {
            GetManager<CourseSelector>().CreateContent();
        });
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
