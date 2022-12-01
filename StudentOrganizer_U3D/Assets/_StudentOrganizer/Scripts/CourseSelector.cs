using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CourseSelector : MonoBehaviour {
   [SerializeField]
   private List<Button> courseButtons;

   [SerializeField]
   //private 
   
   private void Start() {
      for (int i = 0; i < courseButtons.Count; i++) {
         Button courseButton = courseButtons[i];
         courseButton.onClick.AddListener(() => {
            //todo: change ui
         });
      }
   }
}
