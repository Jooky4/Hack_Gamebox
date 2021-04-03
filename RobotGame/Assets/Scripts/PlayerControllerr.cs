using UnityEngine;

//эти строчки гарантирют что наш скрипт не завалится если на плеере будет отсутствовать нужные компоненты
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerControllerr : MonoBehaviour
{
    public float Speed = 0.3f;
    public float JumpForce = 1f;

    //даем возможность выбрать тэг пола.
    //так же убедитесь что ваш Player сам не относится к даному слою. 

    //!!!!Нацепите на него нестандартный Layer, например Player!!!!

    public LayerMask GroundLayer = 1; // 1 == "Default"

    private Rigidbody rb;
    private CapsuleCollider collider; // теперь прийдется использовать CapsuleCollider
    //и удалите бокс коллайдер если он есть

    private bool isGrounded
    {
        get
        {
            var bottomCenterPoint = new Vector3(collider.bounds.center.x, collider.bounds.min.y, collider.bounds.center.z);

            //создаем невидимую физическую капсулу и проверяем не пересекает ли она обьект который относится к полу

            //_collider.bounds.size.x / 2 * 0.9f -- эта странная конструкция берет радиус обьекта.
            // был бы обязательно сферой -- брался бы радиус напрямую, а так пишем по-универсальнее

            return Physics.CheckCapsule(collider.bounds.center, bottomCenterPoint, collider.bounds.size.x / 2 * 0.9f, GroundLayer);
            // если можно будет прыгать в воздухе, то нужно будет изменить коэфициент 0.9 на меньший.
        }
    }

    private Vector3 movementVector
    {
        get
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            return new Vector3(horizontal, 0.0f, vertical);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();

        //т.к. нам не нужно что бы персонаж мог падать сам по-себе без нашего на то указания.
        //то нужно заблочить поворот по осях X и Z
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        //  Защита от дурака
        if (GroundLayer == gameObject.layer)
            Debug.LogError("Player SortingLayer must be different from Ground SourtingLayer!");
    }

    void FixedUpdate()
    {
        JumpLogic();
        MoveLogic();
    }

    private void MoveLogic()
    {
        // т.к. мы сейчас решили использовать физическое движение снова,
        // мы убрали и множитель Time.fixedDeltaTime
        rb.AddForce(movementVector * Speed, ForceMode.Impulse);
    }

    private void JumpLogic()
    {
        if (isGrounded && (Input.GetAxis("Jump") > 0))
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }
}