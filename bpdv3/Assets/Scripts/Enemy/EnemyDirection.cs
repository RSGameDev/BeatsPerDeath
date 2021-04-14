using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyNS
{
    public class EnemyDirection : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private GameObject _nextMoveGO;
        private Collider _collider;
        
        private bool firstMove = true;
        private int _randomDirectionForShroomToFace = 1;

        private void Awake()
        {
            _collider = _nextMoveGO.GetComponent<Collider>();
        }

        public void FaceDirection()
        {
            var num = _randomDirectionForShroomToFace;
            _randomDirectionForShroomToFace = Random.Range(0, 4);

            //// If the direction is the same as the last turn. the collider needs to trigger again.
            //if (num == _randomDirectionForShroomToFace)
            //{
            //    _collider.enabled = false;
            //    _collider.enabled = true;
            //}

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
    }
}