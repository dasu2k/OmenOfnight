using UnityEngine;
using UnityEngine.UI;

public class slideshow : MonoBehaviour
{
  public Image comicPageImage; // Assign the UI Image from the Inspector
  public Sprite[] comicPages; // Array to hold comic page sprites

  public Image nextPageButton;
  public Image previousPageButton;

  private int currentPageIndex = 0;

  public SceneLoader sceneLoader;

  void Start()
  {
    comicPageImage.sprite = comicPages[currentPageIndex]; // Set the first comic page
    // comicPages = Resources.LoadAll<Sprite>("Assets/Character/comics/"); // Load all sprites from "Sprites" folder (adjust folder name if needed)
  }

  public void nextPage()
  {
    currentPageIndex++;
    if (currentPageIndex >= comicPages.Length)
    {
      // End of slideshow (Optional: Load next scene, etc.)
      comicPageImage.enabled = false;
      nextPageButton.enabled = false;
      previousPageButton.enabled = false;
      sceneLoader.StartCounter();

      return;
    }
    comicPageImage.sprite = comicPages[currentPageIndex];
  }

  public void previousPage()
  {
    currentPageIndex--;
    if (currentPageIndex < 0)
    {
      // Beginning of slideshow
      currentPageIndex = 0;
      return;
    }
    comicPageImage.sprite = comicPages[currentPageIndex];
  }

}
