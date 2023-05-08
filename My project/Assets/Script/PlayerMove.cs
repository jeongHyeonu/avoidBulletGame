using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isBtnDown = false;
    public float rotateSpeed = 10;
    public float playerSpeed = 0.1f;

    private void Update()
    {
        if (isBtnDown)
        {
            Debug.Log("?");
            if (Input.GetButtonDown(KeyCode.DownArrow.ToString()))
            {
                this.transform.Translate(new Vector3(0,0, -playerSpeed));
            }
            if (Input.GetButtonDown(KeyCode.UpArrow.ToString()))
            {
                this.transform.Translate(new Vector3(0, 0, +playerSpeed));
            }
            if (Input.GetButtonDown(KeyCode.LeftArrow.ToString()))
            {
                this.transform.Translate(new Vector3(this.transform.position.x-playerSpeed, this.transform.position.y, this.transform.position.z));
            }
            if (Input.GetButtonDown(KeyCode.RightArrow.ToString()))
            {
                this.transform.Translate(new Vector3(this.transform.position.x+ playerSpeed, this.transform.position.y, this.transform.position.z));
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBtnDown = false;
    }



    public void OnDrag(PointerEventData eventData)
    {
        float x = eventData.delta.x * Time.deltaTime * rotateSpeed;
        float y = eventData.delta.y * Time.deltaTime * rotateSpeed;

        transform.Rotate(0, -x, y, Space.World);

        Debug.Log("µå·¡±×");
    }
}
