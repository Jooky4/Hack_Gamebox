using UnityEngine;

//эта строчка гарантирует что наш скрипт не завалится ести на плеере будет отсутствовать компонент Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class PlayerControlNotPhisic : MonoBehaviour
{
    // т.к. логика движения изменилась мы выставили меньшее и более стандартное значение
    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private float JumpForce = 300f;
    [SerializeField]
    private float speedRotateCamera = 1;
    [SerializeField]
    private Animator animatorPlayer;

    //что бы эта переменная работала добавьте тэг "Ground" на вашу поверхность земли
    private bool _isGrounded;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animatorPlayer = GetComponentInChildren<Animator>();
    }


    void FixedUpdate()
    {
        //обратите внимание что все действия с физикой 
        //желательно делать в FixedUpdate, а не в Update
        JumpLogic();

        // в даном случае допустимо использовать это здесь, но можно и в Update.
        // но раз уж вызываем здесь, то 
        // двигать будем используя множитель fixedDeltaTimе 
        MovementLogic();

        RotateCamera();
    }

    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // что бы скорость была стабильной в любом случае
        // и учитывая что мы вызываем из FixedUpdate мы умножаем на fixedDeltaTimе
        transform.Translate(movement * Speed * Time.fixedDeltaTime);
        if (moveVertical != 0)
        {
            animatorPlayer.SetBool("MoveBool", true);
        }
        else
        {
            animatorPlayer.SetBool("MoveBool", false);

        }
    }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                // Обратите внимание что я делаю на основе Vector3.up а не на основе transform.up
                // если наш персонаж это шар -- его up может быть в том числе и вниз и влево и вправо. 
                // Но нам нужен скачек только вверх! Потому и Vector3.up
                rb.AddForce(Vector3.up * JumpForce);
            }
        }
    }


    void RotateCamera()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * speedRotateCamera, 0);
        //Debug.Log(Input.GetAxis("Mouse X"));
    }

    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }
}