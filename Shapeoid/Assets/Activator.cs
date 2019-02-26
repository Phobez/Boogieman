using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public KeyCode fireKey;
    public KeyCode airKey;
    public KeyCode waterKey;
    public KeyCode earthKey;

    private GameObject note;

    private bool active;

    // Start is called before the first frame update
    private void Start()
    {
        active = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(fireKey))
            {
                Destroy(note);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            active = true;
            note = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }
}
