using System.Collections.Generic;
using Controllers.LineControllers;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class StrandController : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private new EdgeCollider2D collider;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float deviationAngle;
        [SerializeField] private float moveSpeed;
        [SerializeField] private string gameOverTag;
        [SerializeField] private string colorTag;
        [SerializeField] private float bounceBackForce = 0.5f;
        [SerializeField] private string controlType;

        readonly List<Vector3> _linePositions = new (); // Lijst van posities van de lijn
        Vector3 _direction = Vector3.up; // De richting van de lijn
        private Camera _mainCamera; // De main camera in de scene
        private ILineController lineController; // De linecontroller in de scene
    
        // Start is called before the first frame update
        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            collider = GetComponent<EdgeCollider2D>();
            rb = GetComponent<Rigidbody2D>();

            lineController = LineControllerFactory.CreateLineController(controlType);
            //Als er geen rigidbody2d component is toegevoegd, voeg er dan een toe
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic; // Zet het bodytype van de rigidbody op dynamic
            }
            
            _mainCamera = Camera.main; // Zoekt de main camera in de scene en slaat deze op in de variabele _mainCamera
            
            _linePositions.Add(transform.position); // Voeg de positie van de lijn toe aan de lijst van posities
            lineRenderer.positionCount = _linePositions.Count; // Zet het aantal posities van de lijnrenderer op het aantal posities in de lijst
            lineRenderer.useWorldSpace = false; // Zet de lijnrenderer op local space zodat hij matcht met het collisionbody
            lineRenderer.SetPosition(0, transform.localPosition); // Zet de positie van de lijnrenderer op de positie van de lijn
        
            UpdateCollider();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 inputDirection = lineController.GetDirection();

            if (inputDirection != Vector3.zero)
            {
                _direction = inputDirection;
                _direction = ApplyDeviation(_direction, deviationAngle);
            }

            MoveLine();
        }
    
        // Past de afwijking toe op de bewegingsrichting
        Vector3 ApplyDeviation(Vector3 originalDirection, float angle)
        {
            // Omrekenen van graden naar radialen
            float angleRad = angle * Mathf.Deg2Rad;

            // Berekent de nieuwe richting met behulp van de cosinus en sinus van de hoek
            float cosAngle = Mathf.Cos(angleRad); // Cosinus van de hoek
            float sinAngle = Mathf.Sin(angleRad); // Sinus van de hoek

            float newX = originalDirection.x * cosAngle - originalDirection.y * sinAngle; // Nieuwe x-positie
            float newY = originalDirection.x * sinAngle + originalDirection.y * cosAngle; // Nieuwe y-positie

            return new Vector3(newX, newY, 0f).normalized;
        }

        void MoveLine()
        {
            // Verplaats de lijn horizontaal, houd deze gecentreerd in de verticale richting van de camera
            if (_direction != Vector3.zero)
            {
                Vector3 newPosition = new Vector3(
                    Mathf.Clamp(transform.position.x + _direction.x * (moveSpeed * Time.deltaTime), _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, _mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x),
                    _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _mainCamera.nearClipPlane)).y,
                    0f
                );

                rb.MovePosition(newPosition); // Positie van de Rigidbody bijwerken
                _linePositions.Add(transform.position); // Nieuwe positie toevoegen aan de lijst met posities

                lineRenderer.positionCount = _linePositions.Count; // Aantal posities instellen op de LineRenderer
                for (int i = 0; i < _linePositions.Count; i++)
                {
                    lineRenderer.SetPosition(i, transform.InverseTransformPoint(_linePositions[i])); // Posities instellen op de LineRenderer in lokale ruimte
                }

                UpdateCollider(); // Collider bijwerken
            }
        }
    
        void UpdateCollider()
        {
            collider.points = ToVector2Array(_linePositions); // Punten van de collider bijwerken op basis van de posities van de lijn
        }
    
        Vector2[] ToVector2Array(List<Vector3> vector3List)
        {
            Vector2[] vector2Array = new Vector2[vector3List.Count]; // Array van Vector2's met de grootte van de lijst van Vector3's
            for (int i = 0; i < vector3List.Count; i++)
            {
                Vector3 localPosition = transform.InverseTransformPoint(vector3List[i]); // Omrekenen van de wereldpositie naar de lokale positie
                vector2Array[i] = new Vector2(localPosition.x, localPosition.y); // Toevoegen van de lokale positie aan de array
            }
            return vector2Array;
        }
        
        void OnCollisionEnter2D(Collision2D collision) // Wordt aangeroepen als er een botsing is
        {
            if (collision.gameObject.CompareTag(gameOverTag)) // Als de tag van het object dat botst gelijk is aan de gameOverTag
            {
                Debug.Log("Game Over!");
            }
            else if (collision.gameObject.CompareTag("leftBoundary") || collision.gameObject.CompareTag("rightBoundary")) // Als de tag van het object dat botst gelijk is aan de leftBoundary of rightBoundary
            {
                // TODO: verbeter logicatie voor botsing met de zijkanten, heeft nu onverwachte effecten
                Vector2 normal = collision.contacts[0].normal; // Vector van het botsingspunt bepalen
                Vector2 reflectionDirection = Vector2.Reflect(_direction, normal).normalized; // Reflectiepunt berekenen op basis van de botsingsvector
                _direction = reflectionDirection; // Richting instellen op de reflectievector
                ApplyBounceBackForce(); // Botsingkracht toepassen
            }
            else if (collision.gameObject.CompareTag(colorTag)) // Als de tag van het object dat botst gelijk is aan de colorTag
            {
                Debug.Log($"{colorTag} detected!");
            }
        }

        void ApplyBounceBackForce()
        {
            rb.AddForce(_direction * bounceBackForce, ForceMode2D.Impulse); // Botsingkracht toepassen op de rigidbody
        }
    }
}