namespace Resources
{
    public class MediaResources
    {
        public static byte[] GetLoaderImage()
        {
            string imgPath = Path.Combine(Environment.CurrentDirectory, "AppResources", "loading.gif");
            if (!File.Exists(imgPath))
                return null;

            var fileBytes = File.ReadAllBytes(imgPath);
            return fileBytes;
        }


    }
}
