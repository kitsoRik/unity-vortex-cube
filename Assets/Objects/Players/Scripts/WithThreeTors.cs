using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class WithThreeTors : MonoBehaviour
    {
        private const int MAX_VALUE_TO_CHANGE_ROTATION = 200;
        private const float KOEF_TO_MULT_ROTATION = 5;

        public Transform Tor1, Tor2, Tor3, Cube;
        public Vector3 Q1, Q2, Q3, QC;

        private void Start()
        {
            if (transform.root.name != "Shop")
            {
                GameController.replayCause += AfterLose;
                GameController.mainMenuCause += AfterLose;
            }
            Q1 = new Vector3(Random.value, Random.value, Random.value);
            Q2 = new Vector3(Random.value, Random.value, Random.value);
            Q3 = new Vector3(Random.value, Random.value, Random.value);
            QC = new Vector3(Random.value, Random.value, Random.value);
        }

        private void FixedUpdate()
        {
            Tor1.rotation = Quaternion.Slerp(Tor1.rotation, Tor1.rotation * Quaternion.Euler(Q1 * KOEF_TO_MULT_ROTATION), 0.5f);
            Tor2.rotation = Quaternion.Slerp(Tor2.rotation, Tor2.rotation * Quaternion.Euler(Q2 * KOEF_TO_MULT_ROTATION), 0.5f);
            Tor3.rotation = Quaternion.Slerp(Tor3.rotation, Tor3.rotation * Quaternion.Euler(Q3 * KOEF_TO_MULT_ROTATION), 0.5f);
            Cube.rotation = Quaternion.Slerp(Cube.rotation, Cube.rotation * Quaternion.Euler(QC * KOEF_TO_MULT_ROTATION), 0.5f);

            if (Random.Range(0, MAX_VALUE_TO_CHANGE_ROTATION) == 0)
                Q1 = new Vector3(Random.value, Random.value, Random.value);
            if (Random.Range(0, MAX_VALUE_TO_CHANGE_ROTATION) == 0)
                Q2 = new Vector3(Random.value, Random.value, Random.value);
            if (Random.Range(0, MAX_VALUE_TO_CHANGE_ROTATION) == 0)
                Q3 = new Vector3(Random.value, Random.value, Random.value);
            if (Random.Range(0, MAX_VALUE_TO_CHANGE_ROTATION) == 0)
                QC = new Vector3(Random.value, Random.value, Random.value);
        }

        private void AfterLose()
        {
            try
            {
                Vector3 _v3 = new Vector3(0, 0, 0);
                Tor1.eulerAngles = _v3;
                _v3.x = 90;
                Tor2.eulerAngles = _v3;
                _v3.y = 90;
                Tor3.eulerAngles = _v3;
            }
            catch (System.Exception) { }
        }
    }
}