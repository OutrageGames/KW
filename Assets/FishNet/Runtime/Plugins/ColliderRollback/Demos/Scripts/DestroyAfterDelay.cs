using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField]
    public float Delay;

    private void Start()
    {
        Destroy(gameObject, Delay);
    }

}
