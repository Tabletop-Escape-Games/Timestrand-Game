using System.Collections.Generic;
using Controllers.LineControllers;
using Controllers.ScoreControllers;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class StrandController : MonoBehaviour
    {
        [SerializeField] private float deviationAngle;
        [SerializeField] private string colorTag;
        [SerializeField] private string controlType;
        [SerializeField] private GameObject _drawPointPrefab;

        readonly List<Vector3> _linePositions = new(); // Lijst van posities van de lijn
        private LineRenderer _lineRenderer;
        private GameObject _linePointer;

        // Start is called before the first frame update
        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();

            // Spawn a drawpoint for this strand
            _drawPointPrefab.tag = colorTag;
            _drawPointPrefab.GetComponent<DrawpointController>().controlType = controlType;
            _linePointer = Instantiate(_drawPointPrefab, transform, false);

            // Zet de lijnrenderer op local space zodat hij matcht met het collisionbody
            _lineRenderer.useWorldSpace = false;
        }

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

        private void FixedUpdate()
        {
            AddPosition(_linePointer.transform.position);
        }
    }
}