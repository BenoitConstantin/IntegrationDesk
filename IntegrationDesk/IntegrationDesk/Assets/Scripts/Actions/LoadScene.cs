using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using UnityEngine.SceneManagement;

public class LoadScene : ActionTask {

    public enum MODE { SYNC, ASYNC }
    public MODE mode;
    public string sceneName;
    public LoadSceneMode loadSceneMode;
    public bool waitEndLoading = true;

    protected override void OnExecute()
    {
        switch (mode)
        {
            case MODE.SYNC: SceneManager.LoadScene(sceneName, loadSceneMode); EndAction(); break;
            case MODE.ASYNC: SceneManager.LoadSceneAsync(sceneName, loadSceneMode); if (waitEndLoading) SceneManager.sceneLoaded += ValidSceneLoaded; else EndAction();  break;
        }
    }

    void ValidSceneLoaded(Scene s, LoadSceneMode mode)
    {
        if (s.name.Equals(sceneName))
        {
            SceneManager.sceneLoaded -= ValidSceneLoaded;
            EndAction();
        }
    }
}
