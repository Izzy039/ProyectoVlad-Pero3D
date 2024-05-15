using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    //Variables de movimiento
    [Header("Parámetros de movimiento")]
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Variables de gravedad
    [Header("Configuración de gravedad")]
    public float gravity = -9.81f;
    Vector3 velocity;

    //Variables de salto
    [Header("Parámetros de salto")]
    public float jumpForce;
    public float maxJumpHeight;
    public bool isGrounded;

    void Update()
    {
        //Para obtener los ejes horizontal y vertical del input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //Usando WASD y flechas como input, asigna valores a los ejes X y Y, luego se normalizan para evitar acelerar si se presionan dos teclas simultáneamente
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //Asigna valor a la gravedad y se multiplica por tiempo
        velocity.y += gravity * Time.deltaTime;
        //Asigna valor al booleano
        isGrounded = controller.isGrounded;

        //Aplica una fuerza en Y si se está en el suelo, para garantizar que el CharacterController detecte la colisión correctamente
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.9f;
        }

        if (direction.magnitude >= 0.1f)
        {
            //Atan calcula la rotación del modelo para poder mirar a donde apunta el mouse, luego lo convierte a grados usando Rad2Deg. Luego suma el ángulo de la cámara para mirar en esa dirección
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //Suaviza el giro del personaje al cambiar de dirección
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //Cambia la rotación del personaje al cambiar de dirección
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //Permite que el jugador se mueva a donde apunta la cámara
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //Instrucción de movimiento, multiplica dirección normalizada * velocidad * tiempo, para hacer la velocidad independiente del framerate
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
