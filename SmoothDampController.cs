using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmoothDampController : MonoBehaviour
{
    [Header("walk Variables")]
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float accelerationTime = 0.1f; //time to smooth speeding
    [SerializeField] float deccelerationTime = 0.05f; //time to smooth stopping

    private float targetHorizontalVelociety;
    private Rigidbody2D rb2d;
    private float dampTime;
    private Vector2 smoothDampStorage; //ref for the smooth damping

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Walk();
    }

    //public to allow unity invoke
    public void OnWalkInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _targetHorizontalVelociety = maxSpeed * Mathf.Sign(context.ReadValue<float>());
            dampTime = accelerationTime;
        }
        else if (context.canceled)
        {
            _targetHorizontalVelociety = 0;
            dampTime = deccelerationTime;
        }
    }

    private void Walk()
    {
        Vector2 target = new Vector2(_targetHorizontalVelociety, rb2d.velocity.y);
        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity, target, ref smoothDampStorage, dampTime);
    }
}
