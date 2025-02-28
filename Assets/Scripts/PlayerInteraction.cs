using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    BoxCollider2D _mCollider;

    // Update is called once per frame
    void Update()
    {
        _mCollider = GetComponent<BoxCollider2D>();
    }
}
