using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main
{
    public class Lives : MonoBehaviour
    {
        #region Life

        [Header("Life Count")] 
        public Image[] lifeCount;
        
        [SerializeField] private GameObject[] LeftSide;
        public SpriteRenderer[] _spriteRendererArrayLeft;
        
        [SerializeField] private GameObject[] RightSide;
        public SpriteRenderer[] _spriteRendererArrayRight;

        private Color fadeColour;
        [SerializeField] private ColourHelper _colourHelper;
        
        
        //public Image[] lifeCount;
        #endregion

        private void Start()
        {
            _spriteRendererArrayLeft = new SpriteRenderer[LeftSide.Length];
            for (var i = 0; i < LeftSide.Length; i++)
            {
                _spriteRendererArrayLeft[i] = LeftSide[i].GetComponent<SpriteRenderer>();
            }
            
            _spriteRendererArrayRight = new SpriteRenderer[RightSide.Length];
            for (var i = 0; i < RightSide.Length; i++)
            {
                _spriteRendererArrayRight[i] = RightSide[i].GetComponent<SpriteRenderer>();
            }
        }

        public void PlayerLoseLife(int lifecount)
        {
            switch (lifecount)
            {
                case 2:
                    _colourHelper.ReferenceLivesThreeLeft(_spriteRendererArrayLeft[0], _spriteRendererArrayLeft[1],
                        _spriteRendererArrayLeft[2], _spriteRendererArrayRight[0], _spriteRendererArrayRight[1], _spriteRendererArrayRight[2]);
                    break;
                case 1:
                    _colourHelper.ReferenceLivesThreeLeft(_spriteRendererArrayLeft[2], _spriteRendererArrayLeft[3],
                        _spriteRendererArrayLeft[4], _spriteRendererArrayRight[2], _spriteRendererArrayRight[3], _spriteRendererArrayRight[4]);
                    break;
                case 0:
                    _colourHelper.ReferenceLivesThreeLeft(_spriteRendererArrayLeft[4], _spriteRendererArrayLeft[5],
                        _spriteRendererArrayLeft[6], _spriteRendererArrayRight[4], _spriteRendererArrayRight[5], _spriteRendererArrayRight[6]);
                    break;
            }
        }
        
    }
}
