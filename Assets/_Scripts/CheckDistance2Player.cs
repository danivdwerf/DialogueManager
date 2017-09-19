using UnityEngine;

public class CheckDistance2Player : MonoBehaviour 
{
    private Transform player;
    private void Start(){player = GameObject.FindGameObjectWithTag (Tags.player).transform;}

    public bool inRange(Vector3 objectToCheck, float maxDistance)
    {
        var distance = (player.position - objectToCheck).sqrMagnitude;
        if (distance <= maxDistance)
            return true;
        return false;
    }
}