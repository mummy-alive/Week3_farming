using UnityEngine;
using UnityEngine.UI;

public class ScreenColorFilter : MonoBehaviour
{
    public float colorChangeDuration = 0f;
    private float elapsedTime = 0f;
    private bool isChangingColor = false;
    [SerializeField] float _transparency = 0.5f;
    private Image filterImage;

    public float fadeDuration = 0f;
    public float blackDuration = 0f; // Duration to stay black
    private bool isFadingToBlack = false;
    private bool isBeingTransparent = false;

    void Start()
    {
        filterImage = GetComponent<Image>();
        filterImage.color = Color.clear; // Start with a transparent color
    }

    void Update()
    {
        if (isChangingColor)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < colorChangeDuration)
            {
                filterImage.color = new Color(Random.value, Random.value, Random.value, _transparency); // Adjust alpha for transparency
            }
            else
            {
                isChangingColor = false;
                elapsedTime = 0f;
                filterImage.color = Color.clear; // Reset to transparent
            }
        }
        if(isFadingToBlack)
        {
            FadeToBlack();
        }
        if (isBeingTransparent)
        {
            FadeToTransparent();
        }
    }

    public void StartColorChange(float duration)
    {
        colorChangeDuration = duration;
        isChangingColor = true;
        elapsedTime = 0f;
    }

    public void StartFadeOut()
    {
        print("Stat fade out");
        isFadingToBlack = true;
        elapsedTime = 0f;
        fadeDuration = 2f;
    }

    public void StartFadeIn()
    {
        print("Stat fade in");
        isBeingTransparent = true;
        elapsedTime = 0f;
        fadeDuration = 2f;
    }
    void FadeToBlack()
    {
        if (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            filterImage.color = new Color(0, 0, 0, alpha);
        }
        else
        {
            elapsedTime = 0f;
            fadeDuration = 0f;
            isFadingToBlack = false;
            filterImage.color = Color.clear;
        }
    }


    void FadeToTransparent()
    {
        if (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            filterImage.color = new Color(0, 0, 0, alpha);
        }
        else
        {
            elapsedTime = 0f;
            fadeDuration = 0f;
            isBeingTransparent = false;
            filterImage.color = Color.clear;
        }
    }

    private static ScreenColorFilter _instance;

    public static ScreenColorFilter Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(ScreenColorFilter)) as ScreenColorFilter;
                if (_instance == null)
                    Debug.Log("no Singleton inventory");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null) _instance = this;
        else if (_instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
