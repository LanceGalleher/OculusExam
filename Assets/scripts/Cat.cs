using UnityEngine;
using TMPro;

// Controls a cat that can be fed food objects. Required for task one.

// maxFood - max number of food objects that can be fed to the cat
// currentFood - keeps track of how many food objects have been fed to the cat so far
// full - whether the cat is considered full or not

// Start() - hides the text at the start
// OnCollisionEnter(Collision collision) - called when a food object collider enters the cat's collider. ONLY reacts if the food object has the taq "food" and will attempt to update currentFood if full is false
// Feed(GameObject food) - Handles the feeding logic when food objects touch the cat. If full is true then the popup text will appear with "Enough, Stop" and reject the food object. Otherwise it will add to currentFood and destroy the food object along with popup text saying "Yum" + currentFood "/" + " maxFood"
// ShowPopup(string message) - Handles logic regarding the popup messages. Calls HidePopup() to control the visibility of the text
// HidePopup() - hides the popup text
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
