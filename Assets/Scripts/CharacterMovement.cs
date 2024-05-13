using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Parámetros generales")]
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float smoothAngle = 0.1f;
    float turnSmoothVelocity;

    [Header("Parámetros de gravedad")]
    public float gravity = 9.81f;
    Vector3 velocity;

    //Variables de salto
    [Header("Parámetros de salto")]
    public float jumpForce;
    public float maxJumpHeight;
    public bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        isGrounded = controller.isGrounded;

        //Aplica una fuerza en Y si se está en el suelo, para garantizar que el CharacterController detecte la colisión correctamente
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -10f;
            Debug.Log("On the ground");
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothAngle);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            
        }

        //Instrucción para aplicar la gravedad, se multiplica por tiempo de nuevo para asignar el valor al cuadrado.
        //REMINDER: Esta instrucción debe estar solamente dentro de Update, si se subordina a otro método puede interferir en la funcionalidad del salto
        controller.Move(velocity * Time.deltaTime);

        //Condicional de salto
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(maxJumpHeight * -2f * gravity);
        }

    }
}
