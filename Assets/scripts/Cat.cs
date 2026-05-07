using UnityEngine;
using TMPro;

public class Cat : MonoBehaviour
{
    [Header("Feed Settings")]
    public int maxFood = 3;

    private int currentFood = 0;
    private bool full = false;

    [Header("UI")]
    public TMP_Text popupText;

    void Start()
    {
        popupText.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Food"))
            return;

        Feed(collision.gameObject);
    }

    void Feed(GameObject food)
    {
        if (full)
        {
            ShowPopup("Enough, Stop");
            Destroy(food);
            return;
        }

        currentFood++;

        Destroy(food);

        ShowPopup($"Yum! ({currentFood}/{maxFood})");

        if (currentFood >= maxFood)
        {
            full = true;
            ShowPopup("Enough, Stop");
        }
    }

    void ShowPopup(string message)
    {
        popupText.gameObject.SetActive(true);
        popupText.text = message;

        CancelInvoke(nameof(HidePopup));
        Invoke(nameof(HidePopup), 2f);
    }

    void HidePopup()
    {
        popupText.gameObject.SetActive(false);
    }
}
