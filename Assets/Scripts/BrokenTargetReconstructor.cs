using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTargetReconstructor : MonoBehaviour
{
    [SerializeField] private float reconstructTime = 1.5f;
    [SerializeField] private Transform piecesContainer;

    private List<Vector3> piecesPos = new();
    private List<Vector3> piecesRot = new();
                 
    private List<Vector3> fallenPiecesPos = new();
    private List<Vector3> fallenPiecesRot = new();

    private float timeToFinish;
    private bool fallenPiecesRegistered = false;

    void Update()
    {
        if (Time.time + reconstructTime >= timeToFinish)
        {
            if (!fallenPiecesRegistered) RegisterFallenPieces();
            Reconstruct();
            if (Time.time >= timeToFinish) Destroy(gameObject);
        }
    }

    private void RegisterFallenPieces()
    {
        for (int pieceIdx = 0; pieceIdx < piecesContainer.childCount; pieceIdx++)
        {
            fallenPiecesPos.Add(piecesContainer.GetChild(pieceIdx).transform.position);
            fallenPiecesRot.Add(piecesContainer.GetChild(pieceIdx).transform.eulerAngles);
        }
        fallenPiecesRegistered = true;
    }

    private void Reconstruct()
    {
        float lerpValue = Mathf.InverseLerp(timeToFinish - reconstructTime, timeToFinish, Time.time);

        for (int childIdx = 0; childIdx < piecesContainer.childCount; childIdx++)
        {
            piecesContainer.GetChild(childIdx).transform.position =
                Vector3.Lerp(fallenPiecesPos[childIdx], piecesPos[childIdx], lerpValue);

            piecesContainer.GetChild(childIdx).transform.eulerAngles =
                Vector3.Lerp(fallenPiecesRot[childIdx], piecesRot[childIdx], lerpValue);
        }
    }

    public void TargetDestroyed(float timeToReconstruct)
    {
        timeToFinish = Time.time + timeToReconstruct;

        for (int pieceIdx = 0; pieceIdx < piecesContainer.childCount; pieceIdx++)
        {
            piecesPos.Add(piecesContainer.GetChild(pieceIdx).transform.position);
            piecesRot.Add(piecesContainer.GetChild(pieceIdx).transform.eulerAngles);
        }
    }
}
