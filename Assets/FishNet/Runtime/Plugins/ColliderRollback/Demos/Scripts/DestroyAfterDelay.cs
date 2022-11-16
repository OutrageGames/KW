using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField]
    private float _delay;

    private void Awake()
    {
        Destroy(gameObject, _delay);
    }

}
