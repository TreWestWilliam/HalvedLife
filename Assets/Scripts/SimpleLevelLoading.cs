//Hachisk-san June 11,2024 
//admin@hachiski.com for support uwu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleLevelLoading : MonoBehaviour
{
    [Header("Scenes")]
    public Scene SceneToGoTo;
    public string TargetString;
    private Scene Current;
    public Scene LoadingScene;
    [Header("Loading Things")]
    public bool IsLoading = false;
    public bool StartedUnload = false;
    public bool StartedLoadingTarget = false;
    //AsyncOperation LoadInbetween;
    //AsyncOperation UnloadInitial;
    //AsyncOperation LoadTarget;
    //AsyncOperation UnloadLoading;
    [Header("Player Stuff")]
    public GameObject Player;
    public Transform LoadingScenePlacement;
    public Transform FinalScenePlacement;

    public void Start()
    {
        Current = SceneManager.GetActiveScene();
        LoadingScene = SceneManager.GetSceneByPath("Assets/Scenes/GS_Loading.unity");
        SceneToGoTo = SceneManager.GetSceneByName(TargetString);
        
        DontDestroyOnLoad(gameObject);
        Player = GameObject.Find("XR Origin (XR Rig)");
        DontDestroyOnLoad(Player.transform.parent);

       // StartCoroutine(LoadLoadingScene());
    }

    public void Update()
    {
        
        
    }

    public void StartLoading() 
    {
        if (!IsLoading) 
        {
            IsLoading = true;
           // StartCoroutine(LoadLoadingScene());
        }
        
        
    }


    public IEnumerator LoadLoadingScene() 
    {
        //yield return null;
        //AsyncOperation LoadInbetween = SceneManager.LoadSceneAsync("Empty", LoadSceneMode.Additive);
        //LoadInbetween.allowSceneActivation = false;
        //Debug.Log($"Loading Progress {LoadInbetween.progress}");
        
       // while (!LoadInbetween.isDone) 
        //{ 
            /*
            if (LoadInbetween.progress >= 0.9f) 
            {
                LoadInbetween.allowSceneActivation = true;
            }*/
        //}
        /*
        if (LoadInbetween.isDone) 
        {
            Player.transform.parent.position = LoadingScenePlacement.position;
            Player.transform.position = LoadingScenePlacement.position;
            AsyncOperation UnloadInitial = SceneManager.UnloadSceneAsync(Current.buildIndex);
        }
        */
        yield return null;



    }
    /*
    public IEnumerator LoadLoadingScene() 
    {
        yield return null;
        LoadInbetween = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        yield return null;
        while (LoadInbetween.isDone == false) 
        {
            Debug.Log("Loading");
        }
        if (LoadInbetween.isDone)
        {
                if (!StartedUnload)
                {
                    StartedUnload = true;
                    //SceneManager.SetActiveScene(LoadingScene);
                    Player.transform.parent.position = LoadingScenePlacement.position;
                    Player.transform.position = LoadingScenePlacement.position;
                    UnloadInitial = SceneManager.UnloadSceneAsync(Current.buildIndex);
                }
                else
                {
                    if (UnloadInitial.isDone)
                    {
                        if (!StartedLoadingTarget)
                        {
                            StartedLoadingTarget = true;
                            LoadTarget = SceneManager.LoadSceneAsync(SceneToGoTo.buildIndex, LoadSceneMode.Additive);
                        }
                    }
                }
        }


        if (StartedLoadingTarget)
        {
            if (LoadTarget.isDone)
            {
                if (UnloadLoading == null)
                {
                    SceneManager.SetActiveScene(SceneToGoTo);
                    Player.transform.parent.position = LoadingScenePlacement.position;
                    Player.transform.position = LoadingScenePlacement.position;
                    UnloadLoading = SceneManager.UnloadSceneAsync(LoadingScene);
                }
                else
                {
                    if (UnloadLoading.isDone)
                    {
                        Destroy(gameObject);
                    }
                }

            }
        }
        yield return null;

    }*/

    /*
    public async bool LoadStartScene() 
    {
        AsyncOperation loadInbetween = SceneManager.LoadSceneAsync(LoadingScene.buildIndex, LoadSceneMode.Single);
        while (!loadInbetween.isDone) 
        {
            yield return null;
        }
    }*/




}
