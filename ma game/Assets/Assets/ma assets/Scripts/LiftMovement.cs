using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MaGame
{
    public class LiftMovement : MonoBehaviour
    {
        [SerializeField] private Transform _maxHeight;
        [SerializeField] private Transform _lowerHeight;
        [SerializeField] private float _speed = 1;
        [SerializeField] private Transform secondLift;
        private bool _reached = false;

        void Start()
        {
            transform.position = _lowerHeight.position;
            
        }

        void Update()
        {
            if (_reached)
            {
                transform.Translate(Vector3.down * Time.deltaTime * _speed);
                secondLift.Translate(Vector3.up * Time.deltaTime * _speed);
            }
            else
            {
                transform.Translate(Vector3.up * Time.deltaTime * _speed);
                secondLift.Translate(Vector3.down * Time.deltaTime * _speed);
            }
            if (transform.position.y <= _lowerHeight.position.y)
            {
                transform.Translate(Vector3.zero);
                secondLift.Translate(Vector3.zero);
                liftRoutine();
                _reached = false;
            }
            else if (transform.position.y >= _maxHeight.position.y)
            {
                transform.Translate(new Vector3(0, 0, 0));
                secondLift.Translate(Vector3.zero);
                liftRoutine();
                _reached = true;
            }

        }

        private IEnumerator liftRoutine()
        {
            yield return new WaitForSeconds(1f);
        }
    }
}