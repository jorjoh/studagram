using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using Microsoft.Azure.WebJobs;

namespace WebJob1
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var host = new JobHost();
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
        private static void Resize(int width, Stream input, Stream output)
        {
            using (var factory = new ImageFactory(preserveExifData: true))
            using (var memory = new MemoryStream())
            {
                factory.Load(input)
                  .Resize(new Size(width, 0))
                  .Format(new JpegFormat { Quality = 75 })
                  .Save(memory);

                memory.CopyTo(output);
            }
        }
        public static void Create640x([BlobTriggerAttribute("barakuba-input/{name}")] Stream input,
                              [BlobAttribute("barakuba/640x/{name}", FileAccess.Write)] Stream output)
        {
            Resize(640, input, output);
        }

        public static void Create240x([BlobTriggerAttribute("barakuba-input/{name}")] Stream input,
                                      [BlobAttribute("barakuba/240x/{name}", FileAccess.Write)] Stream output)
        {
            Resize(240, input, output);
        }
    }
}
