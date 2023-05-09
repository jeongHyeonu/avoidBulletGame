using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaneRotate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    int rotationFlag = 0;
    float rotateSpeed = .5f;

    float deltaX = 0f;
    float deltaZ = 0f;

    private bool isBtnDown;

    private void Update()
    {
        if (isBtnDown)
        {
            Vector3 mouseVec3 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (mouseVec3.x > .5f && mouseVec3.y > .5f) rotationFlag = 1; // 제 1사분면
            else if (mouseVec3.x < .5f && mouseVec3.y > .5f) rotationFlag = 2; // 제 2사분면
            else if (mouseVec3.x < .5f && mouseVec3.y < .5f) rotationFlag = 3; // 제 3사분면
            else if (mouseVec3.x > .5f && mouseVec3.y < .5f) rotationFlag = 4; // 제 4사분면

            switch (rotationFlag)
            {
                case 0:
                    break;
                case 1:
                    deltaX += rotateSpeed;
                    deltaZ -= rotateSpeed;
                    break;
                case 2:
                    deltaX += rotateSpeed;
                    deltaZ += rotateSpeed;
                    break;
                case 3:
                    deltaX -= rotateSpeed;
                    deltaZ += rotateSpeed;
                    break;
                case 4:
                    deltaX -= rotateSpeed;
                    deltaZ -= rotateSpeed;
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isBtnDown)
        {
            deltaX = Mathf.Clamp(deltaX, -20f, 20f);
            deltaZ = Mathf.Clamp(deltaZ, -20f, 20f);
            Vector3 newRot = new Vector3(deltaX, 0, deltaZ);
            this.transform.rotation = Quaternion.Euler(newRot);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBtnDown = false;
        rotationFlag = 0;
    }
}
