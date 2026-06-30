using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;

    private int currentWaypoint = 0;

    void Update()
    {
        if (currentWaypoint >= waypoints.Length) return;

        Transform target = waypoints[currentWaypoint];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypoint++;
        }
    }
}