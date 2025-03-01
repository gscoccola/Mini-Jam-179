using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float LinearDamping = 0f;
    Vector2 _playerSize;

    Rigidbody2D _mRigidBody;
    private Vector2 _moveInput;
    GameManager _gameManager;
   
    PlayerProperties _properties;



    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _mRigidBody = GetComponent<Rigidbody2D>();
        _mRigidBody.linearDamping = LinearDamping;
        _properties = GetComponent<PlayerProperties>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        _mRigidBody.linearVelocity = Vector2.MoveTowards(_mRigidBody.linearVelocity,
            _moveInput * _properties._speed._currentValue, _properties._speed._currentValue * 5 * Time.deltaTime);
        CheckForBoundaries();
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }



    void CheckForBoundaries()
    {

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -_properties._screenWidth + _properties._size._currentValue / 2,
            _properties._screenWidth - _properties._size._currentValue / 2),

            Mathf.Clamp(transform.position.y, -_properties._screenHeight + _properties._size._currentValue / 2
            , _properties._screenHeight - _properties._size._currentValue / 2),

            0f
            );

    }

    void OnAttack(InputValue value)
    {
        //_gameManager.ExpandArea();
    }


}
