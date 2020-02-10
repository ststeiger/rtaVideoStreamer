
namespace rtaNetworking
{


    public abstract class ImageStreamSource
    {


        /// <summary>
        /// Gets or sets the source of images that will be streamed to the 
        /// any connected client.
        /// </summary>
        protected System.Collections.Generic.IEnumerable<System.Drawing.Image> m_imageSource { get; set; }



        public virtual System.Collections.Generic.IEnumerable<System.IO.MemoryStream> Streams
        {
            get
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                foreach (System.Drawing.Image img in this.m_imageSource)
                {
                    ms.SetLength(0);
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    yield return ms;
                } // Next img 

                ms.Close();
                ms = null;

                yield break;
            }
        } // End Property Streams 


        public virtual System.Collections.Generic.IEnumerable<byte[]> Buffers
        {
            get
            {
                foreach (System.Drawing.Image img in this.m_imageSource)
                {
                    byte[] retValue = null;

                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        ms.SetLength(0);
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        retValue = ms.ToArray();
                    }

                    yield return retValue;
                } // Next img 

                yield break;
            }
        } // End Property Buffers 


        public static ImageStreamSource Instance
        {
            get
            {
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
                {
                    return new LinuxImageStreamSource();
                }

                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                {
                    return new WindowsImageStreamSource();
                }

                throw new System.NotImplementedException("Screenshot functionality not implemented for your platform...");
            }
        }


    }


}
