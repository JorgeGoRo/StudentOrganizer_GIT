using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private List<BaseManager> _managers;
    private Dictionary<Type, BaseManager>_managersDictionary;
    private void Awake() {
        GetManagers();
    }

    private void Start() {
        GetManager<CourseReader>().GenerateCourses();
        GetManager<CourseSelector>().FillContent();
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
