using System;
using Managers;
using UnityEngine;

namespace EnemyNS
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyNextMove _enemyNextMove;
        [SerializeField] private GameObject _designatedTileGameObject;
        [HideInInspector] public GameObject NextMoveLocationGO; 
        
        private float _step;
        [SerializeField] private float _speed = 2.0f;
        [SerializeField] private Vector3 location;

        public bool IsEnemyMoving;
        
        public bool hasBegunMove;


        private void Update()
        {
            if (hasBegunMove)
            {
                Movement();
            }
        }

        public void Movement()
        {
            //VacateEnemyStartTile();
            IsEnemyMoving = true;
            _step = _speed * Time.deltaTime;

            var position = _designatedTileGameObject.transform.position;
            location = new Vector3(position.x, position.y, position.z);

            transform.position = Vector3.MoveTowards(transform.position, location, _step);

            if (Vector3.Distance(transform.position, location) < 0.01f)
            {
                transform.position = location;
                IsEnemyMoving = false;
                //_enemyNextMove.NewCycle();
            }
        }
        
        public void AssignNextTile()
        {
            _designatedTileGameObject = NextMoveLocationGO;
        }

        public void ResetValues()
        {
            hasBegunMove = false;
        }
    }
}