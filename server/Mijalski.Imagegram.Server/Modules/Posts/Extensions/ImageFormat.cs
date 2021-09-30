using System.Text;

namespace Mijalski.Imagegram.Server.Modules.Posts.Extensions;

public enum ImageFormat
{
    Bmp,
    Jpg,
    Png,
    Unknown
}

public static class ImageFormatExtensions
{
    public static ImageFormat GetImageFormat(byte[] bytes)
    {
        var bmp = Encoding.ASCII.GetBytes("BM"); // BMP
        var png = new byte[] { 137, 80, 78, 71 }; // PNG
        var jpg = new byte[] { 255, 216, 255 }; // JPG

        if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
        {
            return ImageFormat.Bmp;
        }

        if (png.SequenceEqual(bytes.Take(png.Length)))
        {
            return ImageFormat.Png;
        }

        if (jpg.SequenceEqual(bytes.Take(jpg.Length)))
        {
            return ImageFormat.Jpg;
        }

        return ImageFormat.Unknown;
    }
}