using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    //Variables de movimiento
    [Header("Par�metros de movimiento")]
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Variables de gravedad
    [Header("Configuraci�n de gravedad")]
    public float gravity = -9.81f;
    Vector3 velocity;

    //Variables de salto
    [Header("Par�metros de salto")]
    public float jumpForce;
    public float maxJumpHeight;
    public bool isGrounded;

    void Update()
    {
        //Para obtener los ejes horizontal y vertical del input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //Usando WASD y flechas como input, asigna valores a los ejes X y Y, luego se normalizan para evitar acelerar si se presionan dos teclas simult�neamente
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //Asigna valor a la gravedad y se multiplica por tiempo
        velocity.y += gravity * Time.deltaTime;
        //Asigna valor al booleano
        isGrounded = controller.isGrounded;

        //Aplica una fuerza en Y si se est� en el suelo, para garantizar que el CharacterController detecte la colisi�n correctamente
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.9f;
        }

        if (direction.magnitude >= 0.1f)
        {
            //Atan calcula la rotaci�n del modelo para poder mirar a donde apunta el mouse, luego lo convierte a grados usando Rad2Deg. Luego suma el �ngulo de la c�mara para mirar en esa direcci�n
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //Suaviza el giro del personaje al cambiar de direcci�n
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //Cambia la rotaci�n del personaje al cambiar de direcci�n
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //Permite que el jugador se mueva a donde apunta la c�mara
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //Instrucci�n de movimiento, multiplica direcci�n normalizada * velocidad * tiempo, para hacer la velocidad independiente del framerate
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        //Instrucci�n para aplicar la gravedad, se multiplica por tiempo de nuevo para asignar el valor al cuadrado.
        //REMINDER: Esta instrucci�n debe estar solamente dentro de Update, si se subordina a otro m�todo puede interferir en la funcionalidad del salto
        controller.Move(velocity * Time.deltaTime);

        //Condicional de salto
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(maxJumpHeight * -2f * gravity);
        }

    }
}
