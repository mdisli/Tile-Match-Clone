using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Workspace.Scripts.Floating_Text
{
    public class FloatingTextController : MonoBehaviour
    {
        [SerializeField] private FloatingText floatingText;

        private void OnEnable()
        {
            TileHolder.OnTilesPopped += ShowFloatingText;
        }

        private void OnDisable()
        {
                TileHolder.OnTilesPopped -= ShowFloatingText;
        }

        private void ShowFloatingText(TileHolder.OnTilesPoppedActionClass arg0)
        {
            int i = 0;
            foreach (var position in arg0.positions)
            {
               FloatingText ft = Instantiate(floatingText, position, Quaternion.identity,transform);

               FloatingTextRequirements ftReq = new FloatingTextRequirementsBuilder()
                   .SetOffset(150)
                   .SetStartPosition(position)
                   .SetTransform(ft.transform)
                   .SetDuration(Random.Range(.5f,1f))
                   .SetDelay(i * 0.1f)
                   .Build();
               ft._requirements = ftReq;

               i++;
            }
           
        }
    }
}