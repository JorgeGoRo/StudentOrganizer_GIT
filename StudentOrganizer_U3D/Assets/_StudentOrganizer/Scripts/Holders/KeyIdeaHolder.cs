
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIdeaHolder : MonoBehaviour
{
    private void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
