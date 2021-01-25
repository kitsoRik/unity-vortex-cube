using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fortune;

namespace Fortune
{
    public class FortuneCanvas : MonoBehaviour
    {
        public Fortune fortune;
        private void Awake()
        {
            fortune.Starting();
            gameObject.SetActive(false);
        }
    }
}
