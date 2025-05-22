using UnityEngine;

public class spawnpoint : MonoBehaviour
{
    [SerializeField] GameObject graphics;
    private void Awake()
    {
        graphics.SetActive(false);
    }
}
