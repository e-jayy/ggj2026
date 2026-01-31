using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    [Tooltip("X position at which the object is destroyed")]
    public float destroyXPosition = -15f;

    void Update()
    {
        if (transform.position.x <= destroyXPosition)
        {
            Destroy(gameObject);
        }
    }
}
