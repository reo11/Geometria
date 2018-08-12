using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChange : MonoBehaviour {

    [SerializeField]
    Color btnColor = Color.red;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeButtonColor()
    {
        var button = this.GetComponent<Button>();
        var colors = button.colors;

        /*
        colors.normalColor = btnColor;
        colors.highlightedColor = btnColor;
        colors.pressedColor = btnColor;
        colors.disabledColor = btnColor;

        button.colors = colors;
        */
        button.image.color = btnColor;
    }
}
