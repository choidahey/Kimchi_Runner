using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    void Update()
    {
        // ���� ������Ʈ Ǯ������ ����ȭ
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
