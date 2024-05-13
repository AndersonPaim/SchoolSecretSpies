using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class TeleportDuct : MonoBehaviour
{
    [SerializeField] private List<DuctTrigger> _ductTrigger;

    private void Start()
    {
        foreach (DuctTrigger ductTrigger in _ductTrigger)
        {
            ductTrigger.OnTrigger += HandleDuctTrigger;
        }
    }

    private void OnDestroy()
    {
        foreach (DuctTrigger ductTrigger in _ductTrigger)
        {
            ductTrigger.OnTrigger -= HandleDuctTrigger;
        }
    }

    private void HandleDuctTrigger(PlayerController player, Transform teleportPos)
    {
        Debug.Log("TELEPORT");
        player.Hide(true);
        player.transform.DOMove(teleportPos.position, 3).OnComplete(() => player.Hide(false));
    }
}
