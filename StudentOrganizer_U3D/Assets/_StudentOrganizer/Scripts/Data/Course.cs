using System.Collections.Generic;

public class Course {
    public string name;
    public List<Topic> topics;

    public Course(string name, List<Topic> topics) {
        this.name = name;
        this.topics = topics;
    }
}

