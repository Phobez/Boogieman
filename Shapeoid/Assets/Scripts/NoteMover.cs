using System.Collections;
using UnityEngine;

/// <summary>
/// The component which handles the movement and behaviour of regular Notes.
/// </summary>
public class NoteMover : MonoBehaviour
{
    public GameObject activator;
    public SongParser.NoteType noteType;

    protected NoteGenerator noteGenerator;
    protected float noteSpeed = 0.0f;
    protected KeyCode keyToPress;
    protected ScoreHandler scoreHandler;
    protected bool isDespawning;

    protected float hitOffset;
    protected float greatHitOffset;
    protected float perfectHitOffset;
    protected const float despawnTime = 1.5f;

    // Start is called before the first frame update
    protected void Start()
    {
        noteGenerator = GameObject.FindGameObjectWithTag("GameController").GetComponent<NoteGenerator>();
        scoreHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreHandler>();
        activator = GameObject.FindGameObjectWithTag("Player");

        isDespawning = false;

        noteSpeed = noteGenerator.noteSpeed;
        hitOffset = noteGenerator.hitOffset;
        greatHitOffset = noteGenerator.greatHitOffset;
        perfectHitOffset = noteGenerator.perfectHitOffset;

        switch (noteType)
        {
            case SongParser.NoteType.Fire:
                keyToPress = GameData.currentSavedData.keyBindings["FireKey"];
                break;
            case SongParser.NoteType.Air:
                keyToPress = GameData.currentSavedData.keyBindings["AirKey"];
                break;
            case SongParser.NoteType.Water:
                keyToPress = GameData.currentSavedData.keyBindings["WaterKey"];
                break;
            case SongParser.NoteType.Earth:
                keyToPress = GameData.currentSavedData.keyBindings["EarthKey"];
                break;
                //default:
                //    keyToPress = KeyCode.W;
                //    break;
        }

        // Debug.Log(keyToPress.ToString());
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!noteGenerator.isPaused)
        {
            Vector3 _tempPos = transform.position;
            // _tempPos.x -= noteSpeed;
            _tempPos.x -= (noteSpeed / Time.deltaTime);
            transform.position = _tempPos;

            if (Input.GetKeyDown(keyToPress))
            {
                //Debug.Log("Key to Press if entered successfully.");
                CheckLocation();
            }
            // missed
            if (transform.position.x < activator.transform.position.x - hitOffset && !isDespawning)
            {
                GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 0.0f, 0.0f));
                StartCoroutine(DespawnNote());
                isDespawning = true;
            }
        }
    }

    // checks if it can be hit
    // destroys note if it can
    protected virtual void CheckLocation()
    {
        //if ((transform.position.x >= activator.transform.position.x - hitOffset && transform.position.x <= activator.transform.position.x + hitOffset) && (transform.position.y == activator.transform.position.y))
        //{
        //    //Debug.Log("Checking location.");
        //    scoreHandler.SendMessage("AddScore", transform.position.x);
        //    GameData.currentSongStats.notesHit++;
        //    Destroy(this.gameObject);
        //}

        bool _isHit = false;
        float _accuracyMultiplier = 1.0f;
        ScoreHandler.HitAccuracy _hitAccuracy = ScoreHandler.HitAccuracy.Good;

        if (transform.position.x >= activator.transform.position.x - hitOffset + perfectHitOffset && transform.position.x <= activator.transform.position.x + hitOffset - perfectHitOffset)
        {
            _accuracyMultiplier = 1.2f;
            _isHit = true;
            _hitAccuracy = ScoreHandler.HitAccuracy.Perfect;
        }
        else if (transform.position.x >= activator.transform.position.x - hitOffset + greatHitOffset && transform.position.x <= activator.transform.position.x + hitOffset - greatHitOffset)
        {
            _accuracyMultiplier = 1.1f;
            _isHit = true;
            _hitAccuracy = ScoreHandler.HitAccuracy.Great;
        }
        else if (transform.position.x >= activator.transform.position.x - hitOffset && transform.position.x <= activator.transform.position.x + hitOffset)
        {
            _accuracyMultiplier = 1f;
            _isHit = true;
            _hitAccuracy = ScoreHandler.HitAccuracy.Good;
        }

        if (_isHit)
        {
            // Debug.Log("Accuracy Multiplier: " + _accuracyMultiplier);
            scoreHandler.SendMessage("AddScore", _accuracyMultiplier);
            scoreHandler.SendMessage("ChangeAccuracyText", _hitAccuracy);
            GameData.currentSongStats.notesHit++;
            Destroy(this.gameObject);
        }
    }

    // despawns note if missed
    protected virtual IEnumerator DespawnNote()
    {
        scoreHandler.SendMessage("LoseEnergy");
        //Debug.Log("Lose energy sent.");
        scoreHandler.SendMessage("ResetStreak");
        //Debug.Log("Reset streak sent.");
        scoreHandler.SendMessage("ChangeAccuracyText", ScoreHandler.HitAccuracy.Miss);
        yield return new WaitForSeconds(despawnTime);
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger entered.");
        //if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(keyToPress))
        //{
        //    Debug.Log("Trigger entered successfully.");
        //    CheckLocation();
        //}
    }
}
