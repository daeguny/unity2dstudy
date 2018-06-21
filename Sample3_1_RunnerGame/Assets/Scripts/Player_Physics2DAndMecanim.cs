using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Physics2DAndMecanim : MonoBehaviour {
    public float speed = 12.0f;
    public float jumpPower = 1600.0f;

    bool grounded;
    bool goalCheck;
    float goalTime;

	// Use this for initialization
	void Start () {
        grounded = false;
        goalCheck = false;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Stage_Gate")
        {
            goalCheck = true;
            goalTime = Time.timeSinceLevelLoad;
        }
    }

    // Update is called once per frame
    void Update () {
        Transform groundCheck = transform.Find("GroundCheck");
        grounded = (Physics2D.OverlapPoint(groundCheck.position) != null) ? true : false;
        if (grounded)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpPower));
            }

            GetComponent<Animator>().SetTrigger("Run");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Jump");
        }
        if (transform.position.y < -10.0f)
        {
            SceneManager.LoadScene("StageB1");
        }
	}

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);

        GameObject goCam = GameObject.Find("Main Camera");
        goCam.transform.position = new Vector3(transform.position.x + 5.0f,
            goCam.transform.position.y, goCam.transform.position.z);
    }

    private void OnGUI()
    {
        // 디버그 텍스트
        GUI.TextField(new Rect(10, 10, 300, 60),
            "[Unity2D Sample 3-1 B]\n마우스 왼쪽 버튼을 누르면 가속\n놓으면 점프!");
        if (goalCheck)
        {
            GUI.TextField(new Rect(10, 150, 330, 60),
                string.Format("***** Goal!! *****\nTime {0}", goalTime));
        }
        // 리셋 버튼
        if (GUI.Button(new Rect(10, 80, 100, 20), "리셋"))
        {
            SceneManager.LoadScene("StageB1");
        }
        // 메뉴로 돌아간다
        if (GUI.Button(new Rect(10, 110, 100, 20), "메뉴"))
        {
            SceneManager.LoadScene("SelectMenu");
        }
    }
}
