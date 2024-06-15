using System.Collections.Generic;
using Controllers.LineControllers;
using Controllers.ScoreControllers;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class TimestrandUI : MonoBehaviour
    {
        [Tooltip("The color tag of the line to control.")]
        [SerializeField] private string colorTag;

        [Tooltip("A string representing the type of control type")]
        [SerializeField] private string controlType;

        [Tooltip("The prefeb to be used for the draw point")]
        [SerializeField] private GameObject _drawPointPrefab;

        readonly List<Vector3> _linePositions = new(); // Lijst van posities van de lijn
        private LineRenderer _lineRenderer;
        private GameObject _linePointer;

        // Start is called before the first frame update
        void Start()
        {
            // Get the LineRenderer component and set it to local space
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.useWorldSpace = false;

            // Spawn a drawpoint for this strand
            _drawPointPrefab.tag = colorTag;
            _drawPointPrefab.GetComponent<DrawpointController>().controlType = controlType;
            _linePointer = Instantiate(_drawPointPrefab, transform, false);            
        }

        // On frame update add a new position from the drawpoint to the line. Because we're using physics we use FixedUpdate instead of Update
        private void FixedUpdate()
        {
            AddPosition(_linePointer.transform.position);
        }

        // Add a new position for the line
        void AddPosition(Vector3 position)
        {
            // Voeg de positie van de lijn toe aan de lijst van posities
            _linePositions.Add(position);

            // Zet het aantal posities van de lijnrenderer op het aantal posities in de lijst
            int count = _linePositions.Count;
            _lineRenderer.positionCount = count;

            // Punten van de collider bijwerken op basis van de posities van de lijn
            _lineRenderer.SetPosition(count - 1, transform.InverseTransformPoint(_linePositions[count - 1]));
        }
    }
}