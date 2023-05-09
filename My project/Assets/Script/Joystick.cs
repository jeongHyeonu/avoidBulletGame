using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    float x, y;
    Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        originPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        x += eventData.delta.x * Time.deltaTime * 100;  
        y += eventData.delta.y * Time.deltaTime * 100;

        x = Mathf.Clamp(x, -50f, 50f);
        y = Mathf.Clamp(y, -50f, 50f);


        Vector2 vec = new Vector2(x, y);
        float distance = 50f;// Vector2.Distance(originPos, vec);

        vec = Vector2.ClampMagnitude(vec, distance);
        this.transform.localPosition = vec;

        Player.Instance.MovePlayerJoystick(x, y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("down");

        x = y = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Player.Instance.isJoyStickDown = false;
        this.transform.position = originPos;
        x = y = 0;
    }
}
