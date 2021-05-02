using System;
using Managers;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyNS
{
    public class EnemyDirection : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private GameObject _nextMoveGO;
        private Collider _nextMoveCollider;

        private bool firstMove = true;
        private int _randomDirectionForShroomToFace = 1;
        private bool hasFacedDirection;
        private bool hasReset;
        public bool reassignNextObj;

        private void Awake()
        {
            _nextMoveCollider = _nextMoveGO.GetComponent<Collider>();
        }

        private void Update()
        {
            if (BeatManager.Instance.BeatIndex == 1 || BeatManager.Instance.BeatIndex == 5 && !hasReset)
            {
                hasReset = true;
                hasFacedDirection = false;
            }

            if ((BeatManager.Instance.BeatIndex == 2 || BeatManager.Instance.BeatIndex == 6) && !hasFacedDirection)
            {
                hasReset = false;
                hasFacedDirection = true;
                FaceDirection();
            }
        }

        
        
        public void FaceDirection()
        {
            if (_enemy.isNew)
            {
                //reassignNextObj = true;
                _nextMoveCollider.enabled = true;
                return;
            }

            _nextMoveCollider.enabled = true;
            
            var num = _randomDirectionForShroomToFace;
            _randomDirectionForShroomToFace = Random.Range(0, 4);

            // If the direction is the same as the last turn. the collider needs to trigger again.
            //if (num == _randomDirectionForShroomToFace)
            //{
            //    reassignNextObj = true;
            //    _nextMoveCollider.enabled = true;
            //}

            var enemyType = _enemy.currentEnemyType;

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
    }
}