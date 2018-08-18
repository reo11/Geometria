using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChange : MonoBehaviour {

    [SerializeField]
    Color btnColor = Color.red;
    // id
    private static int countID = 0; 
    public int Id{ get; private set; }

    // Use this for initialization
    void Start () {
        Id = countID++;
        Debug.Log(Id);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeButtonColor()
    {
        var button = this.GetComponent<Button>();
        var colors = button.colors;

        button.image.color = btnColor;
        Debug.Log(this.Id);
    }
}
