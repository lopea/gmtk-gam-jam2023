using UnityEngine;

namespace env
{
    public class WaterMovement : MonoBehaviour
    {
        [SerializeField] private Texture ripple_texture;

        private Renderer _mat;
        // Start is called before the first frame update
        void Start()
        {
            _mat = GetComponent<Renderer>();
            Material main_mat = _mat.material;
            main_mat.mainTexture = ripple_texture;
            main_mat.color = Color.white;
            main_mat.mainTextureScale = new Vector2(5, 5);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
