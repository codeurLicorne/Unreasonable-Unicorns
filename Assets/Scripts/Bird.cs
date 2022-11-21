using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float launchForce = 500;
    [SerializeField] private float maxDrag = 3;

    Vector2 startPos;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRend;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        startPos = rb2D.position;
        //No movement @ start
        rb2D.isKinematic = true;
    }

    private void OnMouseDown()
    {
        spriteRend.color = Color.red;
    }

    private void OnMouseUp()
    {
        Vector2 currentPos = rb2D.position;
        Vector2 direction = startPos - currentPos;
        direction.Normalize();

        rb2D.isKinematic = false;
        rb2D.AddForce(direction * launchForce);

        spriteRend.color = Color.white;

    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPos = mousePos;

        float distance = Vector2.Distance(desiredPos, startPos);
        if(distance > maxDrag)
        {
            Vector2 direction = desiredPos - startPos;
            direction.Normalize();
            desiredPos = startPos + (direction * maxDrag);
        }

        if (desiredPos.x > startPos.x)
            desiredPos.x = startPos.x;

        //transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        rb2D.position = desiredPos;
    }


    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        StartCoroutine(ResetAfterDelay());
       
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);

        rb2D.position = startPos;
        rb2D.isKinematic = true;
        rb2D.velocity = Vector2.zero;
    }
}
