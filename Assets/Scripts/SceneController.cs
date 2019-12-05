using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static readonly int[] sceneMap = { 3, 2, 2, 3, 4, 2, 1, 1, 2, 3, 1, 4, 3, 3, 3, 5, 4, 4, 4, 5, 5, 5, 1, 1, 2 };
    private static readonly int[] blockMap = { 37, 22, 20, 34, 45, 17, 2, 1, 15, 33, 10, 48, 25, 34, 33, 56, 42, 44, 45, 50, 57, 53, 4, 7, 18};

    public static int sceneID = -1;
    private static float startTime;

    public GameObject ring;
    public GameObject buttons;
    public GameObject[] scenes;

    private ObjectTag[] tags;
    private SoundChecker sound;

    private bool soundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        tags = FindObjectsOfType<ObjectTag>();
        sound = FindObjectOfType<SoundChecker>();

        SetScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneID != -1)
        {
            // Delay the sound so it plays after the scene is loaded
            if(!soundPlayed && Time.time - startTime > 0.5)
            {
                sound.PlaySound();
                soundPlayed = true;
            }

            // Return to menu after 5 seconds
            if(Time.time - startTime > 5)
            {
                SetScene(0);
            }
        }
    }

    public bool CheckBlockID(int id)
    {
        return blockMap[sceneID] == id;
    }

    public void SetScene(int num)
    {
        sceneID = num - 1; // Buttons are 1-indexed, since this was how they were specified on the spreadsheet

        SetHidden(ring, sceneID != -1); // Ring is visible when a scene is being shown
        SetHidden(buttons, sceneID == -1); // Buttons are visible when no scene is shown

        // Hide scenes that are not supposed to be shown, and show the one we need
        for (int i = 0; i < scenes.Length; i++)
        {
            SetHidden(scenes[i], sceneID >= 0 && sceneMap[sceneID] == i+1);
        }

        // Move the ring to the necessary block
        foreach(ObjectTag tag in tags)
        {
            if(sceneID >= 0 && tag.id == blockMap[sceneID])
            {
                ring.transform.position = tag.gameObject.transform.position;
            }
        }

        // Reset timer and sound values
        startTime = Time.time;
        soundPlayed = false;
    }

    void SetHidden(GameObject obj, bool state)
    {
        // Hide all visible components
        foreach (Renderer r in obj.GetComponentsInChildren<Renderer>())
        {
            r.enabled = state;
        }

        foreach (MeshRenderer r in obj.GetComponentsInChildren<MeshRenderer>())
        {
            r.enabled = state;
        }

        foreach (Collider c in obj.GetComponentsInChildren<Collider>())
        {
            c.enabled = state;
        }

        foreach (Animator a in obj.GetComponentsInChildren<Animator>())
        {
            a.enabled = state;
        }
    }
}
