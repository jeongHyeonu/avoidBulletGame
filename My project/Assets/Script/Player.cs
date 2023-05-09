using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private static Player instance = null;
    [SerializeField] public GameObject HIttedParticle;

    public static Player Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private Rigidbody rb;
    public Animator ani;
    private bool isBtnDown = false;
    public float playerSpeed = 5;
    public float joyStickX = 0;
    public float joyStickY = 0;
    public bool isJoyStickDown = false;
    public bool isDied = false;
    private float rotateSpeed = 8f;
    private Vector3 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //ani = GetComponent<Animator>();
        instance = this;
        dir = Vector3.zero;
    }

    private void Update()
    {
        if (isDied) return; // 죽었으면 실행 X

        // 방향키
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");
        if (hAxis == 0 && vAxis == 0) { ani.SetBool("walk", false); isBtnDown = false; }
        else { isBtnDown = true; ani.SetBool("walk", true); dir = new Vector3(hAxis, 0, vAxis); }

        // 조이스틱
        if (isJoyStickDown)
        {
            hAxis = joyStickX*0.02f;
            vAxis = joyStickY*0.02f;
            if (hAxis == 0 && vAxis == 0) ani.SetBool("walk", false);
            else { ani.SetBool("walk", true); dir = new Vector3(hAxis, 0, vAxis); }
        }

        // 이동 및 회전
        if (isJoyStickDown || isBtnDown)
        {
            transform.position += dir * playerSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }

        if (ani.GetFloat("jump") >= 0) ani.SetFloat("jump", ani.GetFloat("jump") - Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ani.GetFloat("jump") < 0) ani.SetFloat("jump",.7f);
        }

        // 경기장 밖으로 떨어지면 게임오버
        if (this.transform.position.y < -20f) { isDied = true; GameManager.Instance.GameOver(); }

    }

    public void MovePlayerJoystick(float x, float y)
    {
        isJoyStickDown = true;
        joyStickX = x;
        joyStickY = y;
    }

}
