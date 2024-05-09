using UnityEngine;

//test

public class DragController : MonoBehaviour
{
    public Draggable LastDragged => _lastDragged; // Geeft het draggable object terug in _lastDragged
    private bool _isDragActive; // Geeft aan of er op dit moment een object gesleept wordt
    private Vector2 _screenPosition; // De positie van de muis op het scherm
    private Vector3 _worldPosition; // De positie van de muis in de wereld 
    private Draggable _lastDragged; // Het laatstgesleepte game object van de draggable klasse

    // Awake wordt aangeroepen wanneer het script wordt geladen
    private void Awake()
    {
        // Zoek alle dragcontrollers in de scene, als er meer dan 1 is, verwijder dan de huidige dragcontroller
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1 )
        {
            Destroy( gameObject );
        }
    }

    // Update wordt elke frame aangeroepen
    private void Update()
    {
        // Als er op de linkermuisknop los wordt gelaten of als er een touch is en de touch is geëindigd
        if (_isDragActive && (Input.GetMouseButtonUp(0) || Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            Drop(); // Roep de drop methode aan
            return;
        }
        if (Input.GetMouseButton(0)) // Als er op de linkermuisknop wordt geklikt
        {
            Vector3 mousePos = Input.mousePosition; // Sla de muispositie op in de variabele mousePos
            _screenPosition = new Vector2(mousePos.x, mousePos.y); // Sla de muispositie op in de variabele _screenPosition
        }
        else if (Input.touchCount > 0) // Als er meer dan een aanrakingen zijn
        {
            _screenPosition = Input.GetTouch(0).position; // Sla de positie van de eerste aanraking op in de variabele _screenPosition
        }
        else
        {
            return;
        }

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition); // Sla de positie van de muis in de wereld op in de variabele _worldPosition

        if (_isDragActive ) // Als er op dit moment een object wordt gesleept
        {
            Drag(); // Roep de drag methode aan
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero); // Sla de informatie van de raycast op in de variabele hit
            if (hit.collider != null) // Als de collider van de hit niet null is, oftwel er is geen botsing met een ander object
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>(); // Sla het draggable object op in de variabele draggable
                if (draggable != null) // Als de draggable niet null is
                {
                    _lastDragged = draggable; // Sla de draggable op in de variabele _lastDragged
                    InitDrag(); // Roep de initDrag methode aan
                }
            }
        }
    }

    // Initialiseer het slepen
    void InitDrag()
    {
        _lastDragged.LastPosition = _lastDragged.transform.position; // Sla de positie van het laatstgesleepte object op in de variabele LastPosition
        UpdateDragStatus(true); // Update de status van het slepen met de UpdateDragStatus methode
    }

    // Sleep het object
    private void Drag()
    {
        _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y); // Zet de positie van het laatstgesleepte object op de positie van de muis
        _lastDragged.GetComponent<Renderer>().sortingOrder = 100; // Zet de sorteerorde van de renderer van het laatstgesleepte object op 100, hierdoor komt het object bovenop andere objecten te staan
    }

    // Laat het object los
    void Drop()
    {
        UpdateDragStatus(false); // Update de status van het slepen met de UpdateDragStatus methode
    }

    // Update de status van het slepen
    void UpdateDragStatus(bool isDragging)
    {
        _isDragActive = _lastDragged.isDragging = isDragging; // Zet de isDragging variabele van het laatstgesleepte object op isDragging
        _lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default; // Zet de laag van het laatstgesleepte object op Layer.Dragging als isDragging true is, anders op Layer.Default
    }
}
