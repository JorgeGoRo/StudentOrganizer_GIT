using UnityEngine;


    public class CourseHolder : MonoBehaviour {

        [SerializeField] private GameObject topicHolderPrefab;
        public Course currentCourse;
        private void Awake() {
            for (int i = 0; i < transform.childCount; i++) {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        public void FillCourse(Course course) {
            currentCourse = course;
            for (int i = 0; i < currentCourse.topics.Count; i++) {
                Topic topic = currentCourse.topics[i];
                TopicHolder topicHolder = Instantiate(topicHolderPrefab, transform).GetComponent<TopicHolder>();
                topicHolder.FillTopic(topic);
            }
        }

    }
