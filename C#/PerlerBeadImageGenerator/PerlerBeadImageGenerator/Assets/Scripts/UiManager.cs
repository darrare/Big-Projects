using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;
using Image = UnityEngine.UI.Image;

public class UiManager : MonoBehaviour
{
    private Dictionary<Color, string> Colors = new Dictionary<Color, string>()
    {
        { new Color(0, 0, 0),                 "Black https://fusebeadstore.com/collections/all/products/1000-black-perler-beads                                 "},
        { new Color(.25f, .25f, .25f),        "Dark Grey https://fusebeadstore.com/collections/all/products/1000-dark-grey-perler-beads                         "},
        { new Color(.5f, .5f, .5f),           "Grey https://fusebeadstore.com/collections/all/products/1000-grey-perler-beads                                   "},
        { new Color(.75f, .75f, .75f),        "Light Grey https://fusebeadstore.com/collections/all/products/1000-light-grey-perler-beads                       "},
        { new Color(1, 1, 1),                 "White https://fusebeadstore.com/collections/all/products/1000-white-perler-beads                                 "},
        //{ new Color(0, 0.471f, 0.259f),       "Shamrock https://fusebeadstore.com/collections/all/products/1000-shamrock-perler-beads                           "},
        //{ new Color(0, 0.525f, 0.565f),       "Teal https://fusebeadstore.com/collections/all/products/1000-teal-perler-beads                                   "},
        //{ new Color(0, 0.686f, 0.706f),       "Parrot Green https://fusebeadstore.com/collections/all/products/1000-parrot-green-perler-beads                   "},
        //{ new Color(0, 0.706f, 0.765f),       "Lagoon https://fusebeadstore.com/collections/all/products/1000-lagoon-perler-beads                               "},
        //{ new Color(0, 0.192f, 0.557f),       "Dark Blue https://fusebeadstore.com/collections/all/products/1000-dark-blue-perler-beads                         "},
        //{ new Color(0.016f, 0.561f, 0.945f),  "Turquoise https://fusebeadstore.com/collections/all/products/1000-turquoise-perler-beads                         "},
        //{ new Color(0.031f, 0.365f, 0.322f),  "Forest https://fusebeadstore.com/collections/all/products/1000-forest-perler-beads                               "},
        //{ new Color(0.102f, 0.286f, 0.608f),  "Cobalt https://fusebeadstore.com/collections/all/products/1000-cobalt-perler-beads                               "},
        //{ new Color(0.165f, 0.318f, 0.208f),  "Evergreen https://fusebeadstore.com/collections/all/products/1000-evergreen-perler-beads                         "},
        //{ new Color(0.200f, 0.227f, 0.353f),  "Midnight https://fusebeadstore.com/collections/all/products/1000-midnight-perler-beads                           "},
        //{ new Color(0.251f, 0.506f, 0.859f),  "Light blue https://fusebeadstore.com/collections/all/products/1000-light-blue-perler-beads                       "},
        //{ new Color(0.282f, 0.404f, 0.698f),  "Periwinkle https://fusebeadstore.com/collections/all/products/1000-periwinkle-blue-perler-beads                  "},
        //{ new Color(0.298f, 0.592f, 0.341f),  "Dark Green https://fusebeadstore.com/collections/all/products/1000-dark-green-perler-beads                       "},
        //{ new Color(0.306f, 0.255f, 0.620f),  "Grape https://fusebeadstore.com/collections/all/products/1000-grape-perler-beads                                 "},
        { new Color(0.337f, 0.396f, 0.392f),  "Charcoal https://fusebeadstore.com/collections/all/products/1000-charcoal-perler-beads                           "},
        //{ new Color(0.345f, 0.337f, 0.596f),  "Iris https://fusebeadstore.com/collections/all/products/1000-iris-perler-beads                                   "},
        //{ new Color(0.369f, 0.820f, 0.910f),  "Sky https://fusebeadstore.com/collections/all/products/1000-sky-perler-beads                                     "},
        //{ new Color(0.408f, 0.627f, 0.824f),  "Pastel Blue https://fusebeadstore.com/collections/all/products/1000-pastel-blue-perler-beads                     "},
        //{ new Color(0.408f, 0.671f, 0.220f),  "Bright Green https://fusebeadstore.com/collections/all/products/1000-bright-green-perler-beads                   "},
        //{ new Color(0.408f, 0.235f,  0),      "Brown https://fusebeadstore.com/collections/all/products/1000-brown-perler-beads                                 "},
        //{ new Color(0.424f, 0.173f, 0.275f),  "Eggplant https://fusebeadstore.com/collections/all/products/1000-eggplant-perler-beads                           "},
        //{ new Color(0.435f, 0.788f, 0.686f),  "Light Green https://fusebeadstore.com/collections/all/products/1000-light-green-perler-beads                     "},
        //{ new Color(0.475f, 0.467f, 0.114f),  "Olive https://fusebeadstore.com/collections/all/products/1000-olive-perler-beads                                 "},
        //{ new Color(0.475f, 0.690f,  0),      "Fern https://fusebeadstore.com/collections/all/products/1000-fern-perler-beads                                   "},
        //{ new Color(0.486f,  0, 0.094f),      "Cranapple https://fusebeadstore.com/collections/all/products/1000-cranapple-perler-beads                         "},
        //{ new Color(0.494f, 0.706f,  0),      "Kiwi Lime https://fusebeadstore.com/collections/all/products/1000-kiwi-lime-perler-beads                         "},
        //{ new Color(0.514f, 0.345f, 0.761f),  "Purple https://fusebeadstore.com/collections/all/products/1000-purple-perler-beads                               "},
        { new Color(0.533f, 0.333f, 0.239f),  "Gingerbread https://fusebeadstore.com/collections/all/products/1000-gingerbread-perler-beads                     "},
        //{ new Color(145f/255f, 84f/255f, 0),  "Gingerbread https://fusebeadstore.com/collections/all/products/1000-gingerbread-perler-beads                     "},
        //{ new Color(0.549f, 0.506f, 0.886f),  "Pastel Lavender https://fusebeadstore.com/collections/all/products/1000-pastel-lavender-perler-beads             "},
        //{ new Color(0.553f, 0.651f, 0.922f),  "Blueberry https://fusebeadstore.com/collections/all/products/1000-blueberry-creme-perler-beads                   "},
        //{ new Color(0.569f, 0.816f, 0.533f),  "Pastel Green https://fusebeadstore.com/collections/all/products/1000-pastel-green-perler-beads                   "},
        //{ new Color(0.573f, 0.259f, 0.204f),  "Rust https://fusebeadstore.com/collections/all/products/1000-rust-perler-beads                                   "},
        //{ new Color(0.592f, 0.298f, 0.643f),  "Plum https://fusebeadstore.com/collections/all/products/1000-plum-perler-beads                                   "},
        //{ new Color(0.612f, 0.796f, 0.816f),  "Toothpaste https://fusebeadstore.com/collections/all/products/1000-toothpaste-perler-beads                       "},
        //{ new Color(0.635f, 0.016f, 0.102f),  "Cherry https://fusebeadstore.com/collections/all/products/1000-cherry-perler-beads                               "},
        //{ new Color(0.651f, 0.894f, 0.820f),  "Mint https://fusebeadstore.com/collections/all/products/1000-mint-perler-beads                                   "},
        //{ new Color(0.655f, 0.776f, 0.816f),  "Mist https://fusebeadstore.com/collections/all/products/1000-mist-perler-beads                                   "},
        //{ new Color(0.667f, 0.882f, 0.447f),  "Sour Apple https://fusebeadstore.com/collections/all/products/1000-sour-apple-perler-beads                       "},
        //{ new Color(0.678f, 0.886f, 0.914f),  "Robin Eggs https://fusebeadstore.com/collections/all/products/1000-robins-egg-perler-beads                       "},
        { new Color(0.686f, 0.525f, 0.294f),  "Light Brown https://fusebeadstore.com/collections/all/products/1000-light-brown-perler-beads                     "},
        //{ new Color(160f/255f, 106f/255f, 30f/255f),  "Light Brown https://fusebeadstore.com/collections/all/products/1000-light-brown-perler-beads                     "},
        //{ new Color(0.706f, 0.925f, 0.102f),  "Prickly Pear https://fusebeadstore.com/collections/all/products/1000-prickly-pear-perler-beads                   "},
        //{ new Color(0.722f, 0.678f, 0.831f),  "Light Lavender https://fusebeadstore.com/collections/all/products/1000-light-lavender-perler-beads               "},
        //{ new Color(0.757f, 0.439f, 0.510f),  "Rose https://fusebeadstore.com/collections/all/products/1000-rose-perler-beads                                   "},
        //{ new Color(0.800f, 0.282f, 0.616f),  "Pink https://fusebeadstore.com/collections/all/products/1000-pink-perler-beads                                   "},
        //{ new Color(0.808f, 0.220f, 0.310f),  "Red https://fusebeadstore.com/collections/all/products/1000-red-perler-beads                                     "},
        //{ new Color(0.812f, 0.447f, 0.659f),  "Orchid https://fusebeadstore.com/collections/all/products/1000-orchid-perler-beads                               "},
        //{ new Color(0.820f, 0.467f, 0.667f),  "Bubblegum https://fusebeadstore.com/collections/all/products/1000-bubblegum-perler-beads                         "},
        //{ new Color(0.824f, 0.537f, 0.153f),  "Honey https://fusebeadstore.com/collections/all/products/1000-honey-perler-beads                                 "},
        //{ new Color(0.827f, 0.008f, 0.443f),  "Raspberry https://fusebeadstore.com/collections/all/products/1000-raspberry-perler-beads                         "},
        { new Color(0.835f, 0.835f, 0.729f),  "Creme https://fusebeadstore.com/collections/all/products/1000-creme-perler-beads                                 "},
        //{ new Color(0.835f, 0.906f, 0.349f),  "Sherbert https://fusebeadstore.com/collections/all/products/copy-of-1000-sherbet-perler-beads                    "},
        //{ new Color(0.878f, 0.882f, 0.596f),  "Pastel Yellow https://fusebeadstore.com/collections/all/products/1000-pastel-yellow-perler-beads                 "},
        //{ new Color(0.886f, 0.780f, 0.773f),  "Peach https://fusebeadstore.com/collections/all/products/1000-peach-perler-beads                                 "},
        //{ new Color(0.902f, 0.494f, 0.506f),  "Blush https://fusebeadstore.com/collections/all/products/1000-blush-perler-beads                                 "},
        //{ new Color(0.906f, 0.678f,  0),      "Cheddar  https://fusebeadstore.com/collections/all/products/1000-cheddar-perler-beads                            "},
        //{ new Color(0.910f, 0.729f, 0.788f),  "Light Pink https://fusebeadstore.com/collections/all/products/1000-light-pink-perler-beads                       "},
        //{ new Color(0.914f, 0.855f,  0),      "Yellow https://fusebeadstore.com/collections/all/products/1000-yellow-perler-beads                               "},
        //{ new Color(0.914f, 0.263f, 0.537f),  "Magenta https://fusebeadstore.com/collections/all/products/1000-magenta-perler-beads                             "},
        //{ new Color(0.922f, 0.282f, 0.365f),  "Hot Coral https://fusebeadstore.com/collections/all/products/1000-hot-coral-perler-beads                         "},
        { new Color(0.937f, 0.816f, 0.678f),  "Fawn https://fusebeadstore.com/collections/all/products/1000-fawn-perler-beads                                   "},
        { new Color(0.949f, 0.922f, 0.871f),  "Toasted Marshmallow https://fusebeadstore.com/collections/all/products/1000-toasted-marshmallow-perler-beads     "},
        //{ new Color(0.953f, 0.529f, 0.459f),  "Salmon https://fusebeadstore.com/collections/all/products/1000-salmon-perler-beads                               "},
        //{ new Color(0.957f, 0.682f, 0.718f),  "Flamingo https://fusebeadstore.com/collections/all/products/1000-flamingo-perler-beads                           "},
        //{ new Color(0.984f, 0.569f, 0.745f),  "Cotton Candy https://fusebeadstore.com/collections/all/products/1000-cotton-candy-perler-beads                   "},
        //{ new Color(0.984f, 0.361f, 0.251f),  "Spice https://fusebeadstore.com/collections/all/products/1000-spice-perler-beads                                 "},
        //{ new Color(0.988f, 0.682f, 0.294f),  "Butterscotch https://fusebeadstore.com/collections/all/products/1000-butterscotch-perler-beads                   "},
        //{ new Color(0.992f, 0.478f, 0.302f),  "Tangerine https://fusebeadstore.com/collections/all/products/1000-tangerine-perler-beads                         "},
        { new Color(0.996f, 0.812f, 0.733f),  "Sand https://fusebeadstore.com/collections/all/products/1000-sand-perler-beads                                   "},
        //{ new Color(1, 0.216f, 0.231f),       "Tomato https://fusebeadstore.com/collections/all/products/1000-tomato-perler-beads                               "},
        //{ new Color(1, 0.569f, 0.227f),       "Orange https://fusebeadstore.com/collections/all/products/1000-orange-perler-beads                               "},
        //{ new Color(1, 0.659f, 0.380f),       "Apricot https://fusebeadstore.com/collections/all/products/1000-apricot-perler-beads                             "},
        { new Color(1, 0.773f, 0.592f),       "Tan https://fusebeadstore.com/collections/all/products/1000-tan-perler-beads                                     "}
    };

    [SerializeField]
    private Image SourceImageHolder;

    [SerializeField]
    private Text PixelCheckLengthText;

    [SerializeField]
    private Text SelectionSpacingText;

    [SerializeField]
    private Text ErrorText;

    [SerializeField]
    private Text ReplaceLowUseColorsText;

    [SerializeField]
    private GameObject Prefab;

    [SerializeField]
    private Camera MainCamera;

    [SerializeField]
    private GameObject ObjectsHolder;

    public void Start()
    {

    }
    private void ResetCertainAspectsOnCertainClicks()
    {
        SourceImageHolder.GetComponent<Image>().enabled = true;
        foreach (Transform child in ObjectsHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// The Select Source button was clicked.
    /// 
    /// Open an OpenFileDialog and ask the user to select a PNG or JPG. 
    /// Then convert the file into the texture and load that texture into the SourceImageHolder.
    /// </summary>
    public void SelectSourceButton_Clicked()
    {
        ResetCertainAspectsOnCertainClicks();
        byte[] sourceBytes;

        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            ofd.Filter = "PNG or JPG (*.png;*.jpg)|*.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;

                sourceBytes = File.ReadAllBytes(filePath);
            }
            else
            {
                return;
            }
        }

        Texture2D sourceTexture = new Texture2D(5, 5);
        sourceTexture.LoadImage(sourceBytes);

        Rect sourceTextureRect = new Rect(0, 0, sourceTexture.width, sourceTexture.height);

        Sprite sourceSprite = Sprite.Create(sourceTexture, sourceTextureRect, Vector2.zero);

        SourceImageHolder.sprite = sourceSprite;
        SourceImageHolder.rectTransform.sizeDelta = new Vector2(sourceTexture.width, sourceTexture.height);
    }

    /// <summary>
    /// The Calculate Source button was clicked.
    /// </summary>
    public void CalculateSourceButton_Clicked()
    {
        ResetCertainAspectsOnCertainClicks();
        if (!ImageMath.LoadImageIntoColorArray(SourceImageHolder.sprite.texture))
        {
            ErrorText.text = "Error Calculating Source: Is there an image on your screen? There should be. Click Select Source First.";
        }
        else
        {
            ErrorText.text = "Finished Calculating Source";
        }
    }

    public void NormalizeBwSourceButton_Clicked()
    {
        ResetCertainAspectsOnCertainClicks();
        if (!ImageMath.NormalizeBw())
        {
            ErrorText.text = "Error Normalizing BW Source: Source must be calculated first before normalizing BW. Can only normalize greyscale images.";
        }
        else
        {
            ErrorText.text = "Finished Normalizing BW Source";
        }
    }

    public void DisplaySourceButton_Clicked()
    {
        ResetCertainAspectsOnCertainClicks();
        ErrorText.text = "Finished Displaying Source";
        try
        {
            Texture2D texture = ImageMath.GetSourceTexture();
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            SourceImageHolder.sprite = Sprite.Create(texture, rect, Vector2.zero);
            SourceImageHolder.rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
        }
        catch (Exception ex)
        {
            ErrorText.text = "Error Displaying Source. Source must be calculated first.";
        }
    }

    public void DisplayBwNormalizedButton_Clicked()
    {
        ResetCertainAspectsOnCertainClicks();
        ErrorText.text = "Finished Displaying BW Normalized Source";
        try
        {
            Texture2D texture = ImageMath.GetNormalizedTexture();
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            SourceImageHolder.sprite = Sprite.Create(texture, rect, Vector2.zero);
            SourceImageHolder.rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
        }
        catch (Exception ex)
        {
            ErrorText.text = "Error Displaying BW Normalized Source. BW Normalized must be calculated first.";
        }
    }

    Dictionary<Color, int> ColorUseCount = new Dictionary<Color, int>();
    Color[,] Result;
    public void GraphSourceButton_Clicked()
    {
        ResetCertainAspectsOnCertainClicks();
        Result = ImageMath.GraphSource(Colors.Select(t => t.Key).ToList(), int.Parse(PixelCheckLengthText.text), int.Parse(SelectionSpacingText.text));
        DrawImage();
    }

    public void ReplaceLowUseColors()
    {
        ResetCertainAspectsOnCertainClicks();

        int replaceLowerThan = int.Parse(ReplaceLowUseColorsText.text);

        //Remove all colors that are used less than replaceLowerThan
        List<Color> colorsToAllow = new List<Color>();
        foreach (KeyValuePair<Color, int> item in ColorUseCount)
        {
            if (item.Value >= replaceLowerThan)
            {
                colorsToAllow.Add(item.Key);
            }
        }

        Result = ImageMath.GraphSource(colorsToAllow, int.Parse(PixelCheckLengthText.text), int.Parse(SelectionSpacingText.text));

        DrawImage();
    }

    private void DrawImage()
    {
        ColorUseCount = new Dictionary<Color, int>();
        for (int x = 0; x < Result.GetLength(0); x++)
        {
            for (int y = 0; y < Result.GetLength(1); y++)
            {
                //If alpha, don't draw anything
                if (Result[x, y].a == 0)
                {
                    continue;
                }
                if (!ColorUseCount.ContainsKey(Result[x, y]))
                {
                    ColorUseCount.Add(Result[x, y], 0);
                }
                ColorUseCount[Result[x, y]]++;

                Texture2D texture = new Texture2D(1, 1);
                texture.SetPixel(0, 0, Result[x, y]);
                texture.Apply();
                //y % 2 == 0 ? x : x - .5f
                GameObject newObj = Instantiate(Prefab, new Vector3(x, y, 0), Quaternion.identity, ObjectsHolder.transform);
                newObj.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), Vector2.zero, 1);
            }
        }

        //Move the camera so you can see everything, and disable the source image
        MainCamera.orthographicSize = ((float)Result.GetLength(1)) / 2;
        MainCamera.transform.position = new Vector3(((float)Result.GetLength(0)) / 2, ((float)Result.GetLength(1)) / 2, -10);
        SourceImageHolder.GetComponent<Image>().enabled = false;


        ErrorText.text = $"Stats{Environment.NewLine}";
        ErrorText.text += $"Width: {Result.GetLength(0).ToString()} - {Result.GetLength(0) * 5f / 25.4f}\"{Environment.NewLine}";
        ErrorText.text += $"Height: {Result.GetLength(1).ToString()} - {Result.GetLength(1) * 5f / 25.4f}\"{Environment.NewLine}";
        int total = 0;
        foreach (KeyValuePair<Color, int> item in ColorUseCount)
        {
            ErrorText.text += $"{item.Key.ToString()} used {item.Value} times.{Environment.NewLine}";
            total += item.Value;
        }
        ErrorText.text += $"Total count: {total}";
    }

    public void GraphBwNormalizedButton_Clicked()
    {
        ResetCertainAspectsOnCertainClicks();

    }

    public void GenerateDataFilesButton_Clicked()
    {
        //Build a png 
        Bitmap b = new Bitmap(Result.GetLength(0), Result.GetLength(1));
        for (int x = 0; x < Result.GetLength(0); x++)
        {
            for (int y = 0; y < Result.GetLength(1); y++)
            {
                b.SetPixel(x, Result.GetLength(1) - 1 - y, Result[x, y].ToSystemColor());
            }
        }
        b.Save("output.png", System.Drawing.Imaging.ImageFormat.Png);

        //Build a text file with key
        Dictionary<UnityEngine.Color, char> colorCharPairs = new Dictionary<UnityEngine.Color, char>();
        List<UnityEngine.Color> validColors = new List<UnityEngine.Color>();
        foreach (var item in ColorUseCount)
        {
            validColors.Add(item.Key);
        }
        validColors.Sort(Comparer<Color>.Create((first, second) => first.r.CompareTo(second.r)));

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("KEY: ");
        for (int i = 0; i < validColors.Count; i++)
        {
            colorCharPairs.Add(validColors[i], (char)((int)'A' + i));
            sb.AppendLine($"{(char)((int)'A' + i)} : {validColors[i].ToString()} {ColorUseCount[validColors[i]]} {Colors[validColors[i]]}");
        }
        sb.AppendLine(); sb.AppendLine(); sb.AppendLine(); sb.AppendLine();
        for (int y = 0; y < Result.GetLength(1); y++)
        {
            for (int x = 0; x < Result.GetLength(0); x++)
            {
                if (colorCharPairs.ContainsKey(Result[x, Result.GetLength(1) - 1 - y]))
                    sb.Append(colorCharPairs[Result[x, Result.GetLength(1)- 1 - y]]);
                else
                    sb.Append(" ");
            }
            sb.Append(Environment.NewLine);
        }

        using (StreamWriter sr = new StreamWriter("output.txt"))
        {
            sr.WriteLine(sb.ToString());
        }

        ErrorText.text += $"{Environment.NewLine}Finished generating output";
    }
}
