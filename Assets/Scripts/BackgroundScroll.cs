using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    public MeshRenderer meshRenderer;

    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(GameManager.Instance.CalculateGameSpeed() / 20 * Time.deltaTime, 0);
    }
}
