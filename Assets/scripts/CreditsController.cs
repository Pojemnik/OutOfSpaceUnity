using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public TextAsset creditsText;
    public TMPro.TextMeshProUGUI credtitsTextMesh;

    private void Awake()
    {
        credtitsTextMesh.text = creditsText.text;
    }
}
