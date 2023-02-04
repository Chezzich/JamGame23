using UnityEngine;
using UnityEngine.UI;

public class EndGameEffect : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject textNode;

    private void Start()
    {
        PublicVars.playerController.SetBusy(true);

        LeanTween.value(0, 1f, 1f).setOnUpdate((float alpha) =>
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }).setOnComplete(() =>
        {
            textNode.SetActive(true);

            LeanTween.delayedCall(3f, () => { PublicVars.playerController.SetCanQuit(true); });
        });
    }
}
