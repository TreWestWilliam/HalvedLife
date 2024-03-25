using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[UnityEngine.RequireComponent(typeof(RenderTexture))]
public class ComparisonCameraSystem : MonoBehaviour
{
    [Tooltip("This is the camera used for screenshots.  This should not be the main camera.")]
    public Camera cam;
    [Tooltip("These are the locations the Camera will take pictures at.")]
    public Transform[] CapturePositions;
    [Tooltip("The RenderTexture should be hooked up to the Camera in the Camera's editor.")]
    public RenderTexture RT;
    [Tooltip("The prefix for screenshots taken manually.")]
    public string prefix = "Manual";
    [Space]
    [Header("Automatic Screenshots")]
    [Tooltip("Should the screenshots be captured everytime the game starts?")]
    public bool RunOnStart;
    [Tooltip("This is a seperate Prefix for Screenshots taken automatically.")]
    public string StartPrefix = "Default";
    private int iterator;
    
    IEnumerator Start()
    {
        //This makes sure that we dont accidentally have our automatic screenshots running on clients.
        #if UNITY_EDITOR
        Debug.Log("Unity Editor");
        #elif DEVELOPMENT_BUILD
            StartPrefix = "Default_DevelopmentBuild";
        #else
                        RunOnStart = false;
        #endif
        //This only runs if we have automatic/default photos enabled
        if (RunOnStart) 
        {
            // First we makes sure frames are rendering.
            yield return new WaitForEndOfFrame();
            //Then we start captures
            yield return StartCoroutine(CaptureAll());
        }
        yield return null;
    }

    public void EditorCapture() 
    {
        StartCoroutine(CaptureAll());
    }

    public IEnumerator CaptureAll() 
    {
        cam.gameObject.SetActive(true);

        for (int i = 0; i < CapturePositions.Length; i++) 
        {
            yield return new WaitUntil(() => RT.IsCreated());
            cam.gameObject.transform.SetPositionAndRotation(CapturePositions[i].position, CapturePositions[i].rotation);
            iterator = i;
            yield return StartCoroutine(SavePNG());
        }

        cam.gameObject.SetActive(false);
        RunOnStart = false;
        yield return null;
    }

    IEnumerator SavePNG() 
    {
        yield return new WaitForEndOfFrame();
        //RT = cam.activeTexture;
        RenderTexture.active = RT;
        Texture2D texture = new Texture2D(RT.width, RT.height, TextureFormat.RGBA32, false,false);
        texture.ReadPixels(new Rect(0, 0, RT.width, RT.height), 0, 0);
        RenderTexture.active = null;

        byte[] bytes = texture.EncodeToPNG();
        DestroyImmediate(texture);
        string name;

        if (RunOnStart)
        {
            name = $"../Captures/{StartPrefix}_{iterator}_{System.DateTime.Now.Month},{System.DateTime.Now.Day},{System.DateTime.Now.Year}.png";
        }
        else 
        {
            name = $"../Captures/{prefix}_{iterator}_{System.DateTime.Now.Month},{System.DateTime.Now.Day},{System.DateTime.Now.Year}.png";
        }


        File.WriteAllBytes($"{Application.dataPath}/{name}", bytes);
        Debug.Log($"Wrote image {name} to {Application.dataPath}", gameObject);
        yield return null;
    }
}
