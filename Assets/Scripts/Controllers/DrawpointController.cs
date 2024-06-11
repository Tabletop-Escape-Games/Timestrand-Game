using Controllers;
using Controllers.LineControllers;
using Controllers.ScoreControllers;
using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawpointController : MonoBehaviour
{
    [SerializeField] public string controlType;
    [SerializeField] public float scrollSpeed = 2f;
    [SerializeField] public float bounceBackForce = 4f;

    private Camera _mainCamera;                         // De main camera in de scene
    private Rigidbody2D _rigidBody;                     // De rigidbody van het gameObject;
    private ILineController _lineController;            // De linecontroller in de scene
    private PointScoreStrategy _scoreStrategy;
    private string _colorTag;

    void Start()
    {
        _mainCamera = Camera.main;
        _rigidBody = GetComponent<Rigidbody2D>();
        _lineController = LineControllerFactory.CreateLineController(controlType);
        _scoreStrategy = new PointScoreStrategy();
        _colorTag = this.tag;

        // Set the pointer in the middle of screen vertically
        _rigidBody.position = new Vector3(
            _rigidBody.position.x,
            _mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f)).y,
            0f);

        // Add a velocity to the pointer corresponding to the camera
        _rigidBody.velocity = Vector3.up * scrollSpeed;
    }

    void FixedUpdate()
    {
        // Set horizontal movement by adding force
        float dx = _lineController.GetDirection().x;
        _rigidBody.AddForce(Vector3.right * dx * scrollSpeed);

        // But keep the vertical speed in line with the camera
        _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, scrollSpeed, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision) // Wordt aangeroepen als er een botsing is
    {
        // Als de tag van het object dat botst gelijk is aan de leftBoundary of rightBoundary
        if (collision.gameObject.CompareTag("leftBoundary") || collision.gameObject.CompareTag("rightBoundary")) 
        {
            float speed = _rigidBody.velocity.magnitude;
            Vector2 direction = Vector2.Reflect(_rigidBody.velocity.normalized, collision.contacts[0].normal);

            _rigidBody.velocity = direction * speed * bounceBackForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // We hebben een hit, vergelijk of de kleur van de lijn overeen komt met de kleur van het target
        if (collider.CompareTag(_colorTag))
        {
            // Hit met een valide target, doe er iets mee (in dit geval log in de console)
            //Debug.Log($"Collision with {collider.name} detected!");
            GameController.Instance.UpdateScore(_scoreStrategy, 2);
            Debug.Log(GameController.Instance.GetScore());

            // Verwijder het collider component van het target zodat deze niet nogmaals geraakt kan worden
            Destroy(collider);
        }
        else if (collider.CompareTag("gameOver"))
        {
            GameController.Instance.GameOver();
        }
    }
}