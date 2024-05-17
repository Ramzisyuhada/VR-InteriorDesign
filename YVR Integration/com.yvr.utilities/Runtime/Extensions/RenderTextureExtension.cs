using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RenderTextureExtension
{
    public static void DownloadTo(this RenderTexture target, string path)
    {
        Texture2D copyingTexture = new Texture2D(target.width, target.height, TextureFormat.RGBA32, false);
        RenderTexture activeBackup = RenderTexture.active;
        RenderTexture.active = target;
        copyingTexture.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
        RenderTexture.active = activeBackup;

        byte[] textureBytes = path.Contains("png") ? copyingTexture.EncodeToPNG() : copyingTexture.EncodeToJPG();
        System.IO.File.WriteAllBytes(path, textureBytes);
        Debug.Log("RenderTexture has been saved to " + path);
    }

    public static Color[] GetPixels(this RenderTexture target)
    {
        Texture2D copyingTexture = new Texture2D(target.width, target.height, TextureFormat.RGBA32, false);
        RenderTexture activeBackup = RenderTexture.active;
        RenderTexture.active = target;
        copyingTexture.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
        RenderTexture.active = activeBackup;

        return copyingTexture.GetPixels();
    }
}
