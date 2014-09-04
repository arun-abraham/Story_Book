using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalInput : MonoBehaviour {
    public TextMesh storyText;
    public List<Noun> subjects;
    public int subjectIndex = 0;
    public List<Noun> objects;
    public int objectIndex = 0;

    void Start()
    {
        UpdateStory();
    }

    void Update()
    {
        // Increment target object.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            objectIndex++;
            if (objects[objectIndex] == subjects[subjectIndex])
            {
                objectIndex++;
            }
            if (objectIndex > objects.Count)
            {
                objectIndex = 0;
            }

            UpdateStory();
        }
    }

    private void UpdateStory()
    {
        storyText.text = subjects[subjectIndex].id + " " + subjects[subjectIndex].FindObjectConnection(objects[objectIndex]).action + " " + objects[objectIndex].id;
    }
}
