using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public enum Shape { IDLE, FIRE, AIR, WATER, EARTH };

    public Shape shape;

    public Sprite fireSprite;
    public Sprite airSprite;
    public Sprite waterSprite;
    public Sprite earthSprite;

    public float speed;

    protected Rigidbody2D rb;

    protected SpriteRenderer sprRend;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(-speed, 0f);

        sprRend = GetComponent<SpriteRenderer>();

        switch (shape)
        {
            case Shape.FIRE:
                sprRend.sprite = fireSprite;
                sprRend.color = Color.red;
                break;
            case Shape.AIR:
                sprRend.sprite = airSprite;
                sprRend.color = Color.grey;
                break;
            case Shape.WATER:
                sprRend.sprite = waterSprite;
                sprRend.color = Color.blue;
                break;
            case Shape.EARTH:
                sprRend.sprite = earthSprite;
                sprRend.color = Color.green;
                break;
        }
    }
}
