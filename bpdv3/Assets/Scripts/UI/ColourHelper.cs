using System;
using UI.Main;
using UnityEngine;

namespace UI
{
    public class ColourHelper : MonoBehaviour
    {
        private Lives lives;
        private Color lerpedColourClear = Color.clear;
        public Color lerpedColourFull;
        
        private SpriteRenderer fadeOutRendererLeft;
        private SpriteRenderer hit3RendererLeft;
        private SpriteRenderer fadeInRendererLeft;
        
        public bool enable;
        private Color fadeOutColorLeft;
        private Color hit3ColorLeft;
        private Color fadeInColorLeft;
        
        private float lerpFadeOut = 0f;
        private float lerpFadeIn = 0f;
        private bool complete;
        private int amount;
        
        private SpriteRenderer fadeOutRendererRight;
        private SpriteRenderer hit3RendererRight;
        private SpriteRenderer fadeInRendererRight;
        private Color fadeOutColorRight;
        private Color hit3ColorRight;
        private Color fadeInColorRight;

        // Update is called once per frame
        void Update()
        {
            if (enable)
            {
                if (!complete)
                {
                    FadeColourOut(fadeOutColorLeft, fadeOutColorRight);
                    FadeColourIn(hit3ColorLeft, hit3ColorRight);
                }
                if (complete)
                {
                    FadeColourOut(hit3ColorLeft, hit3ColorRight);
                    FadeColourIn(fadeInColorLeft, fadeInColorRight);
                }
            }

            if (amount == 2)
            {
                enable = false;
                amount = 0;
            }
        }

        public void ReferenceLivesThreeLeft(SpriteRenderer rendererFadeOutLeft,SpriteRenderer rendererHitLeft,SpriteRenderer rendererFadeInLeft, SpriteRenderer rendererFadeOutRight,SpriteRenderer rendererHitRight,SpriteRenderer rendererFadeInRight)
        {
            fadeOutRendererLeft = rendererFadeOutLeft;
            fadeOutColorLeft = fadeOutRendererLeft.color;
            
            hit3RendererLeft = rendererHitLeft;
            hit3ColorLeft = hit3RendererLeft.color;
            
            fadeInRendererLeft = rendererFadeInLeft;
            fadeInColorLeft = fadeInRendererLeft.color;
            
            fadeOutRendererRight = rendererFadeOutRight;
            fadeOutColorRight = fadeOutRendererRight.color;
            
            hit3RendererRight = rendererHitRight;
            hit3ColorRight = hit3RendererRight.color;
            
            fadeInRendererRight = rendererFadeInRight;
            fadeInColorRight = fadeInRendererRight.color;
            
            enable = true;
        }

        public void FadeColourOut(Color colorLeft, Color colorRight)
        {
            if (lerpFadeOut <= 1f)
            {
                if (amount == 0)
                {
                    lerpFadeOut += Time.deltaTime * 2.5f;
                }
                else
                {
                    lerpFadeOut += Time.deltaTime;
                }
                switch (amount)
                {
                    case 0:
                        fadeOutRendererLeft.color = Color.Lerp(colorLeft, lerpedColourClear, lerpFadeOut);
                        fadeOutRendererRight.color = Color.Lerp(colorRight, lerpedColourClear, lerpFadeOut);
                        break;
                    case 1:
                        hit3RendererLeft.color = Color.Lerp(colorLeft, lerpedColourClear, lerpFadeOut);
                        hit3RendererRight.color = Color.Lerp(colorRight, lerpedColourClear, lerpFadeOut);
                        break;
                }
            }
        }

        public void FadeColourIn(Color colorLeft, Color colorRight)
        {
            if (lerpFadeIn <= 1f)
            {
                if (amount == 0)
                {
                    lerpFadeIn += Time.deltaTime * 2.5f;
                }
                else
                {
                    lerpFadeIn += Time.deltaTime;
                }
                switch (amount)
                {
                    case 0:
                        hit3RendererLeft.color = Color.Lerp(colorLeft, lerpedColourFull, lerpFadeIn);
                        hit3RendererRight.color = Color.Lerp(colorRight, lerpedColourFull, lerpFadeIn);
                        break;
                    case 1:
                        fadeInRendererLeft.color = Color.Lerp(colorLeft, lerpedColourFull, lerpFadeIn);
                        fadeInRendererRight.color = Color.Lerp(colorRight, lerpedColourFull, lerpFadeIn);
                        break;
                }
            }
            else
            {
                lerpFadeOut = 0f;
                lerpFadeIn = 0f;
                hit3ColorLeft = hit3RendererLeft.color;
                hit3ColorRight = hit3RendererRight.color;
                amount++;
                complete = true;
            }
        }

        
    }
}
