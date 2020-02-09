
// -------------------------------------------------
// Developed By : Ragheed Al-Tayeb
// e-Mail       : ragheedemail@gmail.com
// Date         : April 2012
// -------------------------------------------------
namespace rtaNetworking.Streaming
{

    /// <summary>
    /// Provides a stream writer that can be used to write images as MJPEG 
    /// or (Motion JPEG) to any stream.
    /// </summary>
    public class MjpegWriter : System.IDisposable
    {

        // private static byte[] CRLF = new byte[] { 13, 10 };
        // private static byte[] EmptyLine = new byte[] { 13, 10, 13, 10};
        // private string _Boundary;


        public MjpegWriter(System.IO.Stream stream)
            : this(stream, "--boundary")
        {

        }


        public MjpegWriter(System.IO.Stream stream, string boundary)
        {

            this.Stream = stream;
            this.Boundary = boundary;
        }



        public string Boundary { get; private set; }
        public System.IO.Stream Stream { get; private set; }


        public void WriteHeader()
        {

            Write(
                    "HTTP/1.1 200 OK\r\n" +
                    "Content-Type: multipart/x-mixed-replace; boundary=" +
                    this.Boundary +
                    "\r\n"
                 );

            this.Stream.Flush();
        }


        public void Write(System.Drawing.Image image)
        {
            System.IO.MemoryStream ms = BytesOf(image);
            this.Write(ms);
        }


        public void Write(System.IO.MemoryStream imageStream)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendLine();
            sb.AppendLine(this.Boundary);
            sb.AppendLine("Content-Type: image/jpeg");
            sb.AppendLine("Content-Length: " + imageStream.Length.ToString());
            sb.AppendLine();

            Write(sb.ToString());
            imageStream.WriteTo(this.Stream);
            Write("\r\n");

            this.Stream.Flush();
        }


        private void Write(byte[] data)
        {
            this.Stream.Write(data, 0, data.Length);
        }


        private void Write(string text)
        {
            byte[] data = BytesOf(text);
            this.Stream.Write(data, 0, data.Length);
        }


        private static byte[] BytesOf(string text)
        {
            return System.Text.Encoding.ASCII.GetBytes(text);
        }


        private static System.IO.MemoryStream BytesOf(System.Drawing.Image image)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms;
        }


        public string ReadRequest(int length)
        {

            byte[] data = new byte[length];
            int count = this.Stream.Read(data, 0, data.Length);

            if (count != 0)
                return System.Text.Encoding.ASCII.GetString(data, 0, count);

            return null;
        }


        public void Dispose()
        {

            try
            {

                if (this.Stream != null)
                    this.Stream.Dispose();

            }
            finally
            {
                this.Stream = null;
            }
        }


    }


}