using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float LinearDamping = 0f;
    [SerializeField] float GroundAcceleration = 5f;
    Rigidbody2D _mRigidBody;
    private Vector2 _moveInput;


    private void Awake()
    {
        _mRigidBody = GetComponent<Rigidbody2D>();
        _mRigidBody.linearDamping = LinearDamping;
    }
    void Start()
    {
        
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        _mRigidBody.linearVelocity = Vector2.MoveTowards(_mRigidBody.linearVelocity, _moveInput * MoveSpeed, GroundAcceleration * Time.deltaTime); ;
    }
}
