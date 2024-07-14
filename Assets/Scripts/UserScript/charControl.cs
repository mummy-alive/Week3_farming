using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class charControl : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2d;
    [SerializeField] private Animator animator;
    [SerializeField] private float _playerSpeed = 2f;

    private bool _canMove = true;

    private void Start()
    {
        UIDialogue.OpenDialogueUI += ( () => {_canMove = false; _rigidbody2d.velocity = new Vector2(0,0); } );
        UIDialogue.CloseDialogueUI += ( () => {_canMove = true;} );

    }
    Vector2 motionVector;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        motionVector = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );
        animator.SetFloat("Horizontal", motionVector.x);
        animator.SetFloat("Vertical", motionVector.y);
        animator.SetFloat("Speed", motionVector.sqrMagnitude);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canMove) _rigidbody2d.velocity = motionVector * _playerSpeed;
    }
}
