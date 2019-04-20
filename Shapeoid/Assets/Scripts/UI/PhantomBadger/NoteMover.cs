using System.Collections;
using UnityEngine;

// Made by      : Abia Herlianto
// Description  :
public class NoteMover : MonoBehaviour
{
    public GameObject activator;
    public SongParser.NoteType noteType;

    private NoteGenerator noteGenerator;
    private float arrowSpeed = 0.0f;
    private KeyCode keyToPress;

    private const float hitOffset = 0.075f;
    private const float despawnTime = 1.5f;

    // Start is called before the first frame update
    private void Start()
    {
        noteGenerator = GameObject.FindGameObjectWithTag("GameController").GetComponent<NoteGenerator>();

        switch (noteType)
        {
            // TO DO: connect keyToPress with PlayerPrefs
            case SongParser.NoteType.Fire:
                keyToPress = KeyCode.W;
                break;
            case SongParser.NoteType.Air:
                keyToPress = KeyCode.A;
                break;
            case SongParser.NoteType.Water:
                keyToPress = KeyCode.D;
                break;
            case SongParser.NoteType.Earth:
                keyToPress = KeyCode.S;
                break;
            default:
                keyToPress = KeyCode.W;
                break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        arrowSpeed = noteGenerator.arrowSpeed;
        Vector3 _tempPos = transform.position;
        _tempPos.x -= arrowSpeed;
        transform.position = -_tempPos;

        if (transform.position.x < activator.transform.position.x - hitOffset)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 0.0f, 0.0f));
            StartCoroutine(DespawnNote());
        }
    }

    private void CheckLocation()
    {
        if (transform.position.x >= activator.transform.position.y - hitOffset && transform.position.x <= activator.transform.position.x + hitOffset)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DespawnNote()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Activator") && Input.GetKeyDown(keyToPress))
        {
            CheckLocation();
        }
    }
}
