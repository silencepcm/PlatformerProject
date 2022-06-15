using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class VideoSmoothStartEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage image;
    VideoPlayer player;
    float SpeedStart = 50f;
    float SpeedEnd = 55f;
    bool load = false;
    [SerializeField] private string _sceneName = "Build";
    public string _SceneName => this._sceneName;

    private AsyncOperation _asyncOperation;

    private IEnumerator LoadSceneAsyncProcess(string sceneName)
    {
        // Begin to load the Scene you have specified.
        this._asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Don't let the Scene activate until you allow it to.
        this._asyncOperation.allowSceneActivation = false;

        while (!this._asyncOperation.isDone)
        {
            Debug.Log($"[scene]:{sceneName} [load progress]: {this._asyncOperation.progress}");

            yield return null;
        }
    }

    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.time < 5f)
        {
            image.color = new Color32(((byte)(Mathf.Clamp(image.color.r + Time.time * SpeedStart, 0f, 255f))), ((byte)(Mathf.Clamp(image.color.g + Time.time * SpeedStart, 0f, 255f))), ((byte)(Mathf.Clamp(image.color.b + Time.time * SpeedStart, 0f, 255f))), 255);
        } else if (!load)
        {
            load = true;

                this.StartCoroutine(this.LoadSceneAsyncProcess(sceneName: this._sceneName));
        }
      
        else if(player.time >=25f)
        {
            this._asyncOperation.allowSceneActivation = true;
        }
    }
}
