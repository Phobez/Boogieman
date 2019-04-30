using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerNote : MonoBehaviour
{
    public Note.Shape shape;

    public Sprite fireSprite;
    public Sprite airSprite;
    public Sprite waterSprite;
    public Sprite earthSprite;

    public float speed;

    private Rigidbody2D rb;

    private SpriteRenderer sprRend;

    private float time = 0.0f;

    private bool hasStoppedCounting = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(-speed, 0f);
        Debug.Log("START TIME: " + time);

        sprRend = GetComponent<SpriteRenderer>();

        switch (shape)
        {
            case Note.Shape.FIRE:
                sprRend.sprite = fireSprite;
                sprRend.color = Color.red;
                break;
            case Note.Shape.AIR:
                sprRend.sprite = airSprite;
                sprRend.color = Color.grey;
                break;
            case Note.Shape.WATER:
                sprRend.sprite = waterSprite;
                sprRend.color = Color.blue;
                break;
            case Note.Shape.EARTH:
                sprRend.sprite = earthSprite;
                sprRend.color = Color.green;
                break;
        }
    }

    private void Update()
    {
        if (transform.position.x <= -4.0f && !hasStoppedCounting)
        {
            Debug.Log("END TIME: " + time);
            hasStoppedCounting = true;
        }
        else if (!hasStoppedCounting)
        {
            time += Time.deltaTime;
        }
    }
}
