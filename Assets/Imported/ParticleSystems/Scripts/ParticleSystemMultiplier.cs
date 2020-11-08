using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ParticleSystemMultiplier : MonoBehaviour
    {
        // I have the Update code

        public float multiplier = 1;

        private float countdown = 3f;
        private void Start(){
            var systems = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem system in systems){
				ParticleSystem.MainModule mainModule = system.main;
				mainModule.startSizeMultiplier *= multiplier;
                mainModule.startSpeedMultiplier *= multiplier;
                mainModule.startLifetimeMultiplier *= Mathf.Lerp(multiplier, 1, 0.5f);
                system.Clear();
                system.Play();
                
            }
        }
        void Update(){
            if(countdown <= 0){
                Destroy(gameObject);
            }
            countdown -= Time.deltaTime;
        }
    }
}
