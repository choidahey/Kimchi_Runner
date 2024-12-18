using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    void Update()
    {
        // 여기 오브젝트 풀링으로 최적화
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
