using Controllers;
using Controllers.LineControllers;
using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DrawpointUI : MonoBehaviour
    {
        [Tooltip("A string defining the LineControllers to be created by the LineController Factory")]
        [SerializeField] public string controlType;

        [Tooltip("The amplification of the force to be applied when a line bounces from a boundary.")]
        [SerializeField] public float bounceBackForce = 4f;

        [Tooltip("The buttonmanager in the game")]
        [SerializeField] public ButtonManager buttonManager;

        private Camera _mainCamera;                         // De main camera in de scene
        private Rigidbody2D _rigidBody;                     // De rigidbody van het gameObject;
        private ILineController _lineController;            // De linecontroller in de scene
        private string _colorTag;

        void Start()
        {
            _colorTag = this.tag;
            _mainCamera = Camera.main;
            _rigidBody = GetComponent<Rigidbody2D>();

            // Retrieve all ButtonControllers and use these to create a LineController
            List<ButtonController> controllers = buttonManager.GetButtonControllers();
            _lineController = LineControllerFactory.CreateButtonLineController(_colorTag, controllers);
            
            // Set the pointer in the middle of screen vertically
            _rigidBody.position = new Vector3(
                _rigidBody.position.x,
                _mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f)).y,
                0f);

            // Add a velocity to the pointer corresponding to the camera
            _rigidBody.velocity = Vector3.up * Settings.scrollSpeed;
        }

        void FixedUpdate()
        {
            // Set horizontal movement by adding force
            float dx = _lineController.GetDirection().x;
            _rigidBody.AddForce(Vector3.right * (dx * Settings.scrollSpeed));

            // But keep the vertical speed in line with the camera
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, Settings.scrollSpeed, 0f);
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

        public void OnTriggerEnter2D(Collider2D collider)
        {
            // We hebben een hit, vergelijk of de kleur van de lijn overeen komt met de kleur van het target
            if (collider.CompareTag(_colorTag))
            {
                // Hit met een valide target, ken punten toe
                GameController.Instance.AddPoints();
                Debug.Log($"Target {_colorTag} hit, {Settings.pointsPerHit} point(s) awarded. New score {GameController.Instance.GetScore()}.");

                // Verwijder het collider component van het target zodat deze niet nogmaals geraakt kan worden
                Destroy(collider);
            }
            else if (collider.CompareTag("gameOver"))
            {
                GameController.Instance.GameOver();
            }
        }
    }
}