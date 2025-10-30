using UnityEngine;

public class MapCtrl : MonoBehaviour
{
    [Header("Material & Texture List")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Texture[] textures;

    private int currentIndex = 0;

    private void Start()
    {
        if (textures.Length > 0)
            ApplyTexture();
    }

    private void OnEnable()
    {
        UISelectMap.OnNextClicked += NextTexture;
        UISelectMap.OnPrevClicked += PreviousTexture;
    }

    private void OnDisable()
    {
        UISelectMap.OnNextClicked -= NextTexture;
        UISelectMap.OnPrevClicked -= PreviousTexture;
    }

    public void NextTexture()
    {
        currentIndex++;
        if (currentIndex >= textures.Length)
            currentIndex = 0; // quay lại đầu danh sách

        ApplyTexture();
    }

    public void PreviousTexture()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = textures.Length - 1; // quay lại cuối danh sách

        ApplyTexture();
    }

    private void ApplyTexture()
    {
        if (targetRenderer != null && textures.Length > 0)
        {
            targetRenderer.material.SetTexture("_MainTex", textures[currentIndex]);
        }
    }
}
