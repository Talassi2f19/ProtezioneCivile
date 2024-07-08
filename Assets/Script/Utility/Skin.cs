using System;
using UnityEngine;

namespace Script.Utility
{
    public class Skin : MonoBehaviour
    {
        [SerializeField]private Animator animator;
        [SerializeField]private AnimatorOverrideController coc;
        [SerializeField]private AnimatorOverrideController cri;
        [SerializeField]private AnimatorOverrideController ggev;
        [SerializeField]private AnimatorOverrideController giornalista;
        [SerializeField]private AnimatorOverrideController medico;
        [SerializeField]private AnimatorOverrideController pc;
        [SerializeField]private AnimatorOverrideController pompiere;
        [SerializeField]private AnimatorOverrideController segreteria;
        [SerializeField]private AnimatorOverrideController sindaco;
        [SerializeField]private AnimatorOverrideController tlc;
        [SerializeField]private AnimatorOverrideController vigile;
        [SerializeField]private AnimatorOverrideController normale;
        
        // public bool flag;
        // public Ruoli r;
        // private void Update()
        // {
        //     if (flag)
        //     {
        //         flag = false;
        //         SetSkin(r);
        //     }
        // }
        
        public void SetSkin(Ruoli ruolo)
        {
            switch (ruolo)
            {
                case Ruoli.Coc:
                    animator.runtimeAnimatorController = coc;
                    break;
                case Ruoli.RefCri:
                case Ruoli.VolCri:
                    animator.runtimeAnimatorController = cri;
                    break;
                case Ruoli.RefGgev:
                case Ruoli.VolGgev:
                    animator.runtimeAnimatorController = ggev;
                    break;
                case Ruoli.RefPC:
                case Ruoli.VolPC:
                    animator.runtimeAnimatorController = pc;
                    break;
                case Ruoli.RefPolizia:
                case Ruoli.VolPolizia:
                    animator.runtimeAnimatorController = vigile;
                    break;
                case Ruoli.VolFuoco:
                    animator.runtimeAnimatorController = pompiere;
                    break;
                case Ruoli.Sindaco:
                    animator.runtimeAnimatorController = sindaco;
                    break;
                case Ruoli.Medico:
                    animator.runtimeAnimatorController = medico;
                    break;
                // case Ruoli.Giornalista:
                //     animator.runtimeAnimatorController = giornalista;
                //     break;
                case Ruoli.Segreteria:
                    animator.runtimeAnimatorController = segreteria;
                    break;
                case Ruoli.RefTlc:
                    animator.runtimeAnimatorController = tlc;
                    break;
                default:
                    animator.runtimeAnimatorController = normale;
                    break;
            }
        }
    }
}
