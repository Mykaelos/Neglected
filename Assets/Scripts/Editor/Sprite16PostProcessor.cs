using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sprite16PostProcessor : AssetPostprocessor {

    void OnPreprocessTexture() {
        TextureImporter textureImporter = (TextureImporter)assetImporter;

        textureImporter.spritePixelsPerUnit = 16;
        textureImporter.filterMode = FilterMode.Point;
        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
    }
}