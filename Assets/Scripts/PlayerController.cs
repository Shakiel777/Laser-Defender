using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float minX, maxX;
    public float speed = 15.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Left Arrow Pressed");
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Right Arrow Pressed");
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            // transform.position.x = Mathf.Clamp(this, minX, maxX);
        }
	}


    //Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);
    //float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
    //paddlePos.x = Mathf.Clamp(mousePosInBlocks, minX, maxX);
    //this.transform.position = paddlePos;
}
