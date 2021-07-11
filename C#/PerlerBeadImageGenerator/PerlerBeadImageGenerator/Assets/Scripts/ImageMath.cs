using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ImageMath
{
    private static ImageData StoredImageData { get; set; }

    public static bool LoadImageIntoColorArray(Texture2D image)
    {
        try
        {
            StoredImageData = new ImageData(image);
        }
        catch(Exception ex)
        {
            return false;
        }
        return true;
    }

    public static bool NormalizeBw()
    {
        try
        {
            StoredImageData.NormalizeBw();
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    public static Texture2D GetSourceTexture()
    {
        return StoredImageData.GetSourceAsTexture();
    }

    public static Texture2D GetNormalizedTexture()
    {
        return StoredImageData.GetNormalizedAsTexture();
    }

    /// <summary>
    /// Returns a 2D jagged array of colors to signify the best option for that spot.
    /// </summary>
    /// <param name="options">Possible color options to choose for each pixel cluster</param>
    /// <returns></returns>
    public static Color[,] GraphSource(List<Color> options, int pixelCheckLength, int selectionSpacing)
    {
        int arrayWidth = (int)((float)StoredImageData.Width / selectionSpacing);
        int arrayHeight = (int)((float)StoredImageData.Height / selectionSpacing);
        Color[,] imageColorArray = new Color[arrayWidth, arrayHeight];

        //Get the source image as-is as a pixellated color graph.
        for (int x = 0; x < arrayWidth; x++)
        {
            for (int y = 0; y < arrayHeight; y++)
            {
                imageColorArray[x, y] = getAverageColor(x * selectionSpacing, y * selectionSpacing, pixelCheckLength);
            }
        }

        //Find the closest match for each color and replace.
        for (int x = 0; x < arrayWidth; x++)
        {
            for (int y = 0; y < arrayHeight; y++)
            {
                imageColorArray[x, y] = findMostSimilarColor(imageColorArray[x, y], options);
            }
        }
        return imageColorArray;
    }

    public static char[,] GraphBwNormalized(List<Color> options)
    {
        return null;
    }




    /// <summary>
    /// Get the average color, normalizing to black and white with majority rules.
    /// IE 5/9 pixels are grey, therefore it will return a grey color.
    /// </summary>
    /// <returns></returns>
    private static Color getAverageColor(int x, int y, int pixelCheckLength)
    {
        Vector3 total = Vector3.zero;
        Color col = Color.black;
        float count = 0;
        bool isGreyScale = true;

        //First check to see if this is going to be greyscale or colored
        int greyCount = 0;
        int colorCount = 0;
        for (int localX = x; localX < x + pixelCheckLength; localX += 1)
        {
            for (int localY = y; localY < y + pixelCheckLength; localY += 1)
            {
                Color pixel = StoredImageData.GetPixel(localX, localY);
                if (pixel.r == pixel.g && pixel.r == pixel.b) { greyCount++; }
                else { colorCount++; }
            }
        }

        if (colorCount > greyCount)
        {
            isGreyScale = false;
        }

        //Then, calculate the image color... Ignoring colored if isGreyScale
        for (int localX = x; localX < x + pixelCheckLength; localX += 1)
        {
            for (int localY = y; localY < y + pixelCheckLength; localY += 1)
            {
                if (localX > StoredImageData.Width - 1 || localY > StoredImageData.Height - 1)
                {
                    continue;
                }
                else
                {
                    Color pixel = StoredImageData.GetPixel(localX, localY);

                    //If we are in greyscale but detect a pixel that isn't grey, ignore it for calculations
                    if (isGreyScale && !pixel.IsColorGrey()) { continue; }

                    total += new Vector3(pixel.r, pixel.g, pixel.b);
                    col += pixel;
                    count++;
                }
            }
        }
        return col /= count;
        //return new Color(total.x / count, total.y / count, total.z / count);
    }

    private static Color findMostSimilarColor(Color color, List<Color> options)
    {
        float smallestDifference = Mathf.Infinity;
        Color smallestDifferenceColor = options[0];

        //Check if majority alpha, then replace with alpha full
        if (color.a <= .5f)
        {
            return new Color(0, 0, 0, 0);
        }

        float tempDiff;

        foreach (Color c in options)
        {
            tempDiff = differenceOfTwoColors(color, c);
            //if (tempDiff < smallestDifference && color.IsColorGrey() == c.IsColorGrey())
            if (tempDiff < smallestDifference && (!color.IsColorGrey() || c.IsColorGrey()))
            {
                smallestDifference = tempDiff;
                smallestDifferenceColor = c;
            }
        }
        return smallestDifferenceColor;
    }

    private static float differenceOfTwoColors(Color first, Color second)
        => Mathf.Sqrt((Mathf.Pow(second.r - first.r, 2) + Mathf.Pow(second.g - first.g, 2) + Mathf.Pow(second.b - first.b, 2)));
}

public class ImageData
{
    public int Width { get { return pixels != null ? pixels.Length : 0; } }
    public int Height { get { return pixels != null && pixels[0] != null ? pixels[0].Length : 0; } }

    private Color[][] pixels;
    private Color[][] normalizedPixels;
    private Texture2D pixelsTexture;
    private Texture2D normalizedPixelsTexture;

    public ImageData(int width, int height)
    {
        pixels = new Color[width][];
        for (int x = 0; x < width; x++)
        {
            pixels[x] = new Color[height];
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pixels[x][y] = new Color(1, 1, 1);
            }
        }
    }

    public ImageData(Texture2D image)
    {
        pixels = new Color[image.width][];
        for (int x = 0; x < image.width; x++)
        {
            pixels[x] = new Color[image.height];
        }

        for (int x = 0; x < image.width; x++)
        {
            for (int y = 0; y < image.height; y++)
            {
                pixels[x][y] = image.GetPixel(x, y);
            }
        }
    }

    public Color GetPixel(int x, int y)
    {
        return pixels[x][y];
    }

    public Texture2D GetSourceAsTexture()
    {
        if (pixelsTexture != null)
        {
            return pixelsTexture;
        }
        else
        {
            pixelsTexture = new Texture2D(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    pixelsTexture.SetPixel(x, y, pixels[x][y]);
                }
            }
            pixelsTexture.Apply();
            return pixelsTexture;
        }
    }

    public Texture2D GetNormalizedAsTexture()
    {
        if (normalizedPixelsTexture != null)
        {
            return normalizedPixelsTexture;
        }
        else
        {
            normalizedPixelsTexture = new Texture2D(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    normalizedPixelsTexture.SetPixel(x, y, normalizedPixels[x][y]);
                }
            }
            normalizedPixelsTexture.Apply();
            return normalizedPixelsTexture;
        }
    }

    public bool NormalizeBw()
    {
        //First error check to make sure this is indeed grey
        foreach (Color color in pixels.ToEnumerable())
        {
            if (!color.IsColorGrey())
            {
                return false;
            }
        }

        //Second, create the normalizedPixelsArray
        normalizedPixels = new Color[Width][];
        for (int x = 0; x < Width; x++)
        {
            normalizedPixels[x] = new Color[Height];
        }

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                normalizedPixels[x][y] = new Color(0, 0, 0);
            }
        }

        //Then normalize the pixels (IE the darkest color becomes pure black, and the lightest color becomes pure white)
        float minColor = 1;
        float maxColor = 0;
        foreach (Color c in pixels.ToEnumerable())
        {
            if (c.r > maxColor) { maxColor = c.r; }
            if (c.r < minColor) { minColor = c.r; }
        }

        for (int x = 0; x < normalizedPixels.Length; x++)
        {
            for (int y = 0; y < normalizedPixels[x].Length; y++)
            {
                float newColor = (pixels[x][y].r - minColor) / (maxColor - minColor);
                normalizedPixels[x][y] = new Color(newColor, newColor, newColor);
            }
        }
        return true;
    }

    public Color this[int x, int y]
    {
        get { return pixels[x][y]; }
        set { pixels[x][y] = value; }
    }
}
