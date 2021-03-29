using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyNS
{
    public class EnemyDirection : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

        [SerializeField] private int _randomDirectionForShroomToFace = 1;
        //public bool _startFaceDirection;
        //public bool _canChangeDirection;

        public bool sameDirection;

        public bool hasFacedDirection;
        public bool firstMove;

        [SerializeField] private GameObject _nextMoveGO;
        private Collider _collider;

        private void Awake()
        {
            _collider = _nextMoveGO.GetComponent<Collider>();
        }

        private void Update()
        {
            if (_enemy.isAlive)
            {
                if ((BeatManager.Instance.BeatIndex == 2 || BeatManager.Instance.BeatIndex == 6) && !hasFacedDirection)
                {
                    hasFacedDirection = true;
                    FaceDirection();
                }

                if (BeatManager.Instance.BeatIndex == 1 || BeatManager.Instance.BeatIndex == 5)
                {
                    NewCycle();
                }
            }
        }

        private void FaceDirection()
        {
            if (firstMove)
            {
                firstMove = false;
                transform.LookAt(transform.position + Vector3.back);
                return;
            }

            var num = _randomDirectionForShroomToFace;
            _randomDirectionForShroomToFace = Random.Range(0, 4);

            // If the direction is the same as the last turn. the collider needs to trigger again.
            if (num == _randomDirectionForShroomToFace)
            {
                _collider.enabled = false;
                //sameDirection = true;
                _collider.enabled = true;
            }

            var enemyType = _enemy.CurrentEnemyType;

            switch (enemyType)
            {
                case Enemy.EnemyType.Shroom:
                    switch (_randomDirectionForShroomToFace)
                    {
                        case 0:
                            transform.LookAt(transform.position + Vector3.forward);
                            break;
                        case 1:
                            transform.LookAt(transform.position + Vector3.back);
                            break;
                        case 2:
                            transform.LookAt(transform.position + Vector3.left);
                            break;
                        case 3:
                            transform.LookAt(transform.position + Vector3.right);
                            break;
                    }

                    break;
                case Enemy.EnemyType.Rook:
                    //rook code
                    break;
            }
        }

        private void NewCycle()
        {
            hasFacedDirection = false;
            //_firstMoveStarted = false;
            //_canChangeDirection = true;
            //sameDirection = false;
        }

        private void ResetValues()
        {
            NewCycle();

            transform.LookAt(transform.position + Vector3.back);
            _randomDirectionForShroomToFace = 1;
            firstMove = true;
        }

        private void OnDisable()
        {
            ResetValues();
        }
    }
}