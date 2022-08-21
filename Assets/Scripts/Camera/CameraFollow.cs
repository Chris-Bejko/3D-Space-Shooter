using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    Transform _player;

    [SerializeField]
    Vector3 _offset;

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _player.position.z) + _offset;
    }
}
