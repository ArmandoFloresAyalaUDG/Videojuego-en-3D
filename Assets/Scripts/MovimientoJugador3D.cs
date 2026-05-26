using UnityEngine.InputSystem;
using UnityEngine;

public class MovimientoJugador3D : MonoBehaviour
{
    public float velocidad = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveInput.y += 1;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveInput.y -= 1;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveInput.x += 1;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveInput.x -= 1;

        Vector3 movimiento = new Vector3(moveInput.x, 0f, moveInput.y) * velocidad;
        rb.linearVelocity = new Vector3(movimiento.x, 0f, movimiento.z);
    }
}