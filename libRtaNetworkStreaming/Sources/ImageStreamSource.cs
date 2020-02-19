
namespace rtaNetworking
{


    public abstract class ImageStreamSource
    {


        /// <summary>
        /// Gets or sets the source of images that will be streamed to the 
        /// any connected client.
        /// </summary>
        protected System.Collections.Generic.IEnumerable<System.Drawing.Image> m_imageSource { get; set; }
        protected System.Collections.Generic.IEnumerable<byte[]> m_bufferSource { get; set; }
        protected System.Collections.Generic.IEnumerable<System.IO.MemoryStream> m_streamSource { get; set; }



        public virtual System.Collections.Generic.IEnumerable<System.IO.MemoryStream> Streams
        {
            get
            {
                if (this.m_streamSource != null)
                {

                    foreach (System.IO.MemoryStream ms in this.m_streamSource)
                    {
                        yield return ms;

                        ms.Close();
                    } // Next ms 
                } // End if (this.m_streamSource != null) 


                if (this.m_bufferSource != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();

                    foreach (byte[] img in this.m_bufferSource)
                    {
                        ms.SetLength(0);
                        ms.Write(img, 0, img.Length);
                        yield return ms;
                    } // Next img 

                    ms.Close();
                    ms = null;
                } // End if (this.m_bufferSource != null) 


                if (this.m_imageSource != null)
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
                } // End if (this.m_imageSource != null) 
                
                yield break;
            }

        } // End Property Streams 


        public virtual System.Collections.Generic.IEnumerable<byte[]> Buffers
        {
            get
            {

                if (this.m_bufferSource != null)
                {
                    foreach (byte[] img in this.m_bufferSource)
                    {
                        yield return img;
                    } // Next img 

                }

                if (this.m_streamSource != null)
                {
                    byte[] retValue = null;

                    foreach (System.IO.MemoryStream ms in this.m_streamSource)
                    {
                        retValue = ms.ToArray();
                        ms.Dispose();
                        yield return retValue;
                    }
                }


                if (this.m_imageSource != null)
                {
                    foreach (System.Drawing.Image img in this.m_imageSource)
                    {
                        byte[] retValue = null;

                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            ms.SetLength(0);
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            retValue = ms.ToArray();
                        } // End Using ms 

                        yield return retValue;
                    } // Next img 
                }

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
