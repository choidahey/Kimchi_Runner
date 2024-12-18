using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    //private float move_speed = 8;

    void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.CalculateGameSpeed() * Time.deltaTime;
    }
}
