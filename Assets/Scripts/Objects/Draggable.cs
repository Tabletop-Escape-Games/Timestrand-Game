using Controllers;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool isDragging; // Geeft aan of er op dit moment een object wordt gesleept
    public Vector3 LastPosition; // De laatst bekende positie van het object

    private Collider2D _collider; // De collider van het object
    private DragController _dragController; // De dragcontroller van het object
    private float moveSpeed = 15f; // De snelheid waarmee het object beweegt
    private System.Nullable<Vector3> _targetPosition; // De positie waar het object naartoe beweegt

    // Start wordt aangeroepen voordat de eerste frame wordt geüpdatet
    private void Start()
    {
        _collider = GetComponent<Collider2D>(); // Sla de collider van het object op in de variabele _collider
        _dragController = FindObjectOfType<DragController>(); // Sla de dragcontroller van het object op in de variabele _dragController
    }

    // Update wordt elke frame aangeroepen
    private void FixedUpdate() // FixedUpdate loopt altijd synchroon met de physics engine, bij reguliere Update kan dit soms niet het geval zijn
    {
        if (_targetPosition.HasValue) // Als er targetPosition een waarde heeft
        {
            if (isDragging) // Als er op dit moment een object wordt gesleept
            {
                _targetPosition = null; // Zet de targetPosition op null
                return; // Stop de methode
            }
            if (transform.position == _targetPosition)  // Als de positie van het object gelijk is aan de targetPositie
            {
                gameObject.layer = Layer.Default; // Zet de layer van het object terug naar de standaard waarde
                _targetPosition = null; // Zet de targetPosition op null
            }
            else
            {
                // Beweeg het object naar de targetPositie met een snelheid van moveSpeed, dit zorgt voor een geleidelijke beweging
                transform.position = Vector3.Lerp(transform.position, _targetPosition.Value, moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    // OnTriggetEnter2D wordt aangeroepen wanneer een object de collider van het object binnenkomt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Draggable collidedDraggable = collision.GetComponent<Draggable>(); // Sla de draggable van de collision op in de variabele collidedDraggable

        if (collidedDraggable != null && _dragController.LastDragged.gameObject == gameObject) // Als de collided draggable niet null is en de laatst gesleepte draggable gelijk is aan het huidige object
        {
            ColliderDistance2D colliderDistance2D = collision.Distance(_collider); // Sla de afstand tussen de collider van het object en de collider van de collision op in de variabele colliderDistance2D
            Vector3 difference = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance; // Sla het verschil tussen de collider en de collision op in de variabele difference
            transform.position -= difference; // Verplaats het object met het verschil
        }
        if (gameObject.tag == collision.tag) // Als de tag van het object gelijk is aan de tag van de collision
        {
            _targetPosition = collision.transform.position; // Sla de positie van de collision op in de targetPositie
        } else { // Als de tag van het object niet gelijk is aan de tag van de collision
            _targetPosition = LastPosition; // Sla de laatst bekende positie van het object op in de targetPositie
        }
    }
}
