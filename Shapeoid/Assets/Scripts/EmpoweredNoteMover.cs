using System.Collections;
using UnityEngine;

/// <summary>
/// The component which handles the movement and behaviour of Empowered Notes.
/// </summary>
public class EmpoweredNoteMover : NoteMover
{
    // checks if it can be hit
    // destroys note if it can
    protected override void CheckLocation()
    {
        if ((transform.position.x >= activator.transform.position.x - hitOffset && transform.position.x <= activator.transform.position.x + hitOffset) && (transform.position.y == activator.transform.position.y))
        {
            scoreHandler.SendMessage("AddPower");
            GameData.currentSongStats.notesHit++;
            Destroy(this.gameObject);
        }
    }

    // despawns note if missed
    protected override IEnumerator DespawnNote()
    {
        scoreHandler.SendMessage("LoseEnergy");
        yield return new WaitForSeconds(despawnTime);
        Destroy(this.gameObject);
    }
}
