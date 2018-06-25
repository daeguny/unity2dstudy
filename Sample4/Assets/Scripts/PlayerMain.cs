using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour {
    PlayerController playerCtrl;

    private void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerCtrl.activeSts)
        {
            return;
        }

        // 패드 처리
        float joyMv = Input.GetAxis("Horizontal");
        playerCtrl.ActionMove(joyMv);

        // 점프
        if (Input.GetButtonDown("Jump"))
        {
            playerCtrl.ActionJump();
        }
	}
}
