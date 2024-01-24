using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageCache
{
    private static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    private static Queue<string> spriteNames = new Queue<string>();
    private static int maxSprites = 300;

    public static IEnumerator getSprite(List<Sprite> returnValue, string name) {
        // Gets the sprite from cache
        if (sprites.ContainsKey(name)) {
            returnValue.Add(sprites[name]);
            yield return null;
        }

        // Gets the sprite from URL
        string url = "https://dhhim4ltzu1pj.cloudfront.net/media/images/" + name + ".width-450.png";
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log(request.error);
            yield return null;
        }
        else {
            Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));

            returnValue.Add(sprite);

            // Removes the oldest sprite if cache is full
            if (spriteNames.Count > maxSprites) {
                sprites.Remove(spriteNames.Dequeue());
            }

            // Adds the sprite to cache
            sprites[name] = sprite;
            spriteNames.Enqueue(name);
        }
        yield return null;
    }
}
