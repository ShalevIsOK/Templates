using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControls : MonoBehaviour
{
    [SerializeField] float moveToTargetThreshold = 1f;
    [SerializeField] private float speed = 1f;
    public Vector3 Velocity => rb2d.velocity;
    private Vector2 currentPosition => new Vector2(transform.position.x, transform.position.y);

    private Rigidbody2D rb2d;
    private Vector2 mousePos;
    private Vector2 target;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var toTarget = target - currentPosition;
        if (toTarget.magnitude > moveToTargetThreshold)
        {
            Vector2 movementVec = toTarget.normalized * speed;
            rb2d.velocity = movementVec;
        }
        else rb2d.velocity = Vector2.zero;
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();

    }
    public void MoveInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            target = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}