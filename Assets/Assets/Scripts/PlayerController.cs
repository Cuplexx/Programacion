using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;

    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float mouseSensitivity = 100f;

    public float gravity = -9.81f;

    float xRotation = 0f;
    Vector3 velocity;

    private bool canControl = true;
    private Vector3 input;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ConsumableSystem.onConsumibleUsed += ConsumibleUsed;

        //Añadir las funciones locales a los calbacks del DialogueManager
        DialogueManager.singleton.onDialogueStart += DisableControl;
        DialogueManager.singleton.onDialogueEnd+= EnableControl;

        void EnableControl(Dialogue d)
        {
            canControl = true;
        }

        void DisableControl(Dialogue d)
        {
            canControl = false;
        }
    }

    void Update()
    {
        Move();
        Look();

        if(canControl == false)
        {
            input = Vector3.zero;
            return;
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void ConsumibleUsed(ItemInfo consumible)
    {
        //hay que transformar el ItemIn
        Consumible cons = consumible as Consumible;
        //solo cambia la velocidad de movimiento si es un buff de este tipo
        //if (cons.walkSpeed != 0)
        //{
        //    //llamar a la corrutina que cambia la velocidad de movimiento con los valores del consumible
        //    StartCoroutine(MoveSpeedChangeCrt(cons.moveSpeedAmount, cons.duration));
        //}
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}