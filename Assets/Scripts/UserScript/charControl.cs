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
        _rigidbody2d = GetComponent<Rigidbody2D>();
        UIDialogue.OpenDialogueUI += ( () => {_canMove = false; _rigidbody2d.velocity = new Vector2(0,0); } );
        UIDialogue.CloseDialogueUI += ( () => {_canMove = true;} );

    }
    Vector2 motionVector;
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
    private void FixedUpdate()
    {
        if (_canMove) _rigidbody2d.velocity = motionVector * _playerSpeed;
    }

    public void MoveCharTo(Vector2 position)
    {
        _rigidbody2d.position = position;
    }

    public void ScaleCharBy(float multiple)
    {
        transform.localScale = new Vector2(multiple, multiple);
    }

    // Make GMGold a singleton object
    private static charControl _instance;
    public static charControl Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(charControl)) as charControl;
                if (_instance == null)
                    Debug.Log("no Singleton goldManager");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

}
