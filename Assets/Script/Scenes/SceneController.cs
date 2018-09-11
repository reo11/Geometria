using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
	public void OnClickModeSelect()
    {
        Debug.Log("ModeSelect");
		// buildに各シーンを追加しないといけない
		SceneManager.LoadScene ("ModeSelect");
    }

	public void OnClickSetting()
    {
        Debug.Log("setting");
		// buildに各シーンを追加しないといけない
		SceneManager.LoadScene ("Setting");
    }

    public void OnClickTitle()
    {
        Debug.Log("title");
		// buildに各シーンを追加しないといけない
		SceneManager.LoadScene ("Title");
    }

    public void OnClickSingle()
    {
        Debug.Log("single");
		// buildに各シーンを追加しないといけない
		SceneManager.LoadScene ("StageSelect");
    }

    public void OnClickOnlineRanking()
    {
        Debug.Log("online");
		// buildに各シーンを追加しないといけない
		SceneManager.LoadScene ("OnlineRanking");
    }

    public void OnClickOnlineGame()
    {
        Debug.Log("onlineModePlay");
		// buildに各シーンを追加しないといけない
		SceneManager.LoadScene ("OnlineMode");
    }

    public void OnClickSingleGame()
    {
        Debug.Log("singleModePlay");
		// buildに各シーンを追加しないといけない
		SceneManager.LoadScene ("SingleMode");
    }

    public void OnClickOfflineScore()
    {
        Debug.Log("offlineScore");
        // buildに各シーンを追加しないといけない
        SceneManager.LoadScene("OfflineScore");
    }
}
