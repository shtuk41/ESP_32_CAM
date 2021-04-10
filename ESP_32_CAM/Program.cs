using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Drawing;

namespace ESP_32_CAM
{
    class Program
    {
        static void Main()
        {
            Task t = new Task(DownloadPageAsync);
            t.Start();
            Console.WriteLine("Downloading page...");
            Console.ReadLine();
        }

        static async void DownloadPageAsync()
        {
            // ... Target page.
            string page = "http://192.168.1.138/capture?_cb=1616885847557";

            // ... Use HttpClient.
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                byte [] result = await content.ReadAsByteArrayAsync();

                // ... Display the result.
                using (Image image = Image.FromStream(new MemoryStream(result)))
                {
                    image.Save("NewImage.jpg", ImageFormat.Jpeg);
                }
            }
        }
    }

}
