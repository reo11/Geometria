using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {
	public void OnClickStart()
    {
        Debug.Log("start");
		// buildに各シーンを追加しないといけない
		SceneManager.LoadScene ("Ranking");
    }

	public void OnClickSetting()
    {
        Debug.Log("setting");
		// buildに各シーンを追加しないといけない
		SceneManager.LoadScene ("Setting");
    }
}
