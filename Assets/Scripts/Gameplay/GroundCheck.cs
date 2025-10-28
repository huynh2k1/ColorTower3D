using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool isCollision = false;
    public Action OnCollisionBlock;

    public void Initialize()
    {
        isCollision = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isCollision)
            return;

        if (collision.gameObject.CompareTag("Block"))
        {
            isCollision = true;
            OnCollisionBlock?.Invoke();
        }
    }
}
