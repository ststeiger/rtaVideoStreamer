namespace rtaNetworking.Streaming
{


    // #ifdef _MSC_VER
    // #pragma pack(push)  // save the original data alignment
    // #pragma pack(1)     // Set data alignment to 1 byte boundary
    // #endif

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct BMPHeader // # ifndef _MSC_VER __attribute__((packed)) #endif
    {
        public ushort type;              // Magic identifier: 0x4d42
        public uint size;              // File size in bytes
        public ushort reserved1;         // Not used
        public ushort reserved2;         // Not used
        public uint offset;            // Offset to image data in bytes from beginning of file
        public uint dib_header_size;   // DIB Header size in bytes
        public int width_px;          // Width of the image
        public int height_px;         // Height of image
        public ushort num_planes;        // Number of color planes
        public ushort bits_per_pixel;    // Bits per pixel
        public uint compression;       // Compression type
        public uint image_size_bytes;  // Image size in bytes
        public int x_resolution_ppm;  // Pixels per meter
        public int y_resolution_ppm;  // Pixels per meter
        public uint num_colors;        // Number of colors
        public uint important_colors;  // Important colors
    }


    // #ifdef _MSC_VER #pragma pack(pop)  // restore the previous pack setting #endif
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct BMPImage
    {
        public BMPHeader header;
        // unsigned char* data;
        // It is more informative and will force a necessary compiler error
        // on a rare machine with 16-bit char.
        // uint8_t* data;
        public byte[] data;
    } 





    public unsafe class bmp
    {

        public const int BMP_HEADER_SIZE = 54;
        public const int DIB_HEADER_SIZE = 40;

        // Correct values for the header
        public const int MAGIC_VALUE = 0x4D42;
        public const int NUM_PLANE = 1;
        public const int COMPRESSION = 0;
        public const int NUM_COLORS = 0;
        public const int IMPORTANT_COLORS = 0;
        public const uint BITS_PER_BYTE = 8;
        // #define BITS_PER_PIXEL = 24;
        public const int BITS_PER_PIXEL = 32;



        //   Return the size of an image row in bytes.
        // - Precondition: the header must have the width of the image in pixels.
        public static unsafe uint computeImageSize(BMPHeader* bmp_header)
        {
            uint bytes_per_pixel = bmp_header->bits_per_pixel / BITS_PER_BYTE;
            uint bytes_per_row_without_padding = (uint)( bmp_header->width_px * bytes_per_pixel);
            uint padding = (4 - ((uint) bmp_header->width_px * bytes_per_pixel) % 4) % 4;
            uint row_size_bytes = bytes_per_row_without_padding + padding;

            // stride = row_size_bytes
            // int stride = 4 * ((bmp_header->width_px * bytes_per_pixel + 3) / 4);

            return row_size_bytes * (uint)bmp_header->height_px;
        }


        private static byte[] GetBytes<T>(T aux) 
            where T : struct
        {
            int length = System.Runtime.InteropServices.Marshal.SizeOf(aux);
            System.IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            System.Runtime.InteropServices.Marshal.StructureToPtr(aux, ptr, true);
            System.Runtime.InteropServices.Marshal.Copy(ptr, myBuffer, 0, length);
            System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr);

            return myBuffer;
        }


        public static void WriteStructToFile(string filename)
        {
            string saveToFilePath = @"D:\" + filename;

            int bytesWritten = 0;
            using (System.IO.FileStream myFileStream = new System.IO.FileStream(saveToFilePath, System.IO.FileMode.Create))
            {
                BMPHeader myData = new BMPHeader();

                byte[] newBuffer = GetBytes<BMPHeader>(myData);
                myFileStream.Write(newBuffer, 0, newBuffer.Length);
                bytesWritten += newBuffer.Length;
            }
        }




        public static void TestXGetImage()
        {
            // libRtaNetworkStreaming.XImage* foo = libRtaNetworkStreaming.XLib.XGetImage();



            // System.IntPtr pImage;
            // libRtaNetworkStreaming.XImage block1 = (libRtaNetworkStreaming.XImage)System.Runtime.InteropServices.Marshal.PtrToStructure(pImage, typeof(libRtaNetworkStreaming.XImage));
        } // End Sub TestXGetImage





        public static void WriteStructToStream<T>(System.IO.FileStream myFileStream, T myData) 
            where T : struct
        {
            // int bytesWritten = 0;
            byte[] newBuffer = GetBytes<T>(myData);
            myFileStream.Write(newBuffer, 0, newBuffer.Length);
            // bytesWritten += newBuffer.Length;
        }



        // Free all memory referred to by the given BMPImage.
        //public static unsafe void free_bmp(BMPImage* image)
        //{
        //    // free(image->data);
        //    // free(image);
        //}



        //// Close file and release memory.void _clean_up(FILE *fp, BMPImage *image, char **error)
        //public static unsafe void _clean_up(FILE* fp, BMPImage* image, char** error)
        //{
        //    if (fp != NULL)
        //    {
        //        fclose(fp);
        //    }
        //    free_bmp(image);
        //    free(*error);
        //}


        //// Print error message and clean up resources.
        //void _handle_error(char** error, FILE* fp, BMPImage* image)
        //{
        //    fprintf(stderr, "ERROR: %s\n", *error);
        //    _clean_up(fp, image, error);

        //    exit(EXIT_FAILURE);
        //}


        // Open file. In case of error, print message and exit.
        //FILE* _open_file(const char* filename, const char* mode)
        //{
        //    FILE* fp = fopen(filename, mode);
        //    if (fp == NULL)
        //    {
        //        fprintf(stderr, "Could not open file %s\n", filename);
        //        exit(EXIT_FAILURE);
        //    }

        //    return fp;
        //}




        ////   Make a copy of a string on the heap.
        //// - Postcondition: the caller is responsible to free
        ////   the memory for the string.
        //char* _string_duplicate(const char*string)
        //{
        //    char* copy = (char*)malloc(sizeof(*copy) * (strlen(string) + 1));
        //    if (copy == NULL)
        //    {
        //        // return "Not enough memory for error message";
        //        const char* error_message = "Not enough memory for error message";
        //        size_t len = strlen(error_message);
        //        char* error = (char*)malloc(len * sizeof(char) + 1);
        //        strcpy(error, error_message);
        //        return error;
        //    }

        //    strcpy(copy, string);
        //    return copy;
        //}



        //// Check condition and set error message.
        //bool _check(bool condition, char** error, const char* error_message)
        //{
        //    bool is_valid = true;
        //    if (!condition)
        //    {
        //        is_valid = false;
        //        if (*error == NULL)  // to avoid memory leaks
        //        {
        //            *error = _string_duplicate(error_message);
        //        }
        //    }
        //    return is_valid;
        //}


        ////   Write an image to an already open file.
        //// - Postcondition: it is the caller's responsibility to free the memory
        ////   for the error message.
        //// - Return: true if and only if the operation succeeded.
        //bool write_bmp(FILE* fp, BMPImage* image, char** error)
        //{
        //    // Write header
        //    rewind(fp);
        //    size_t num_read = fwrite(&image->header, sizeof(image->header), 1, fp);
        //    if (!_check(num_read == 1, error, "Cannot write image"))
        //    {
        //        return false;
        //    }
        //    // Write image data
        //    num_read = fwrite(image->data, image->header.image_size_bytes, 1, fp);
        //    if (!_check(num_read == 1, error, "Cannot write image"))
        //    {
        //        return false;
        //    }

        //    return true;
        //}


        //void write_image(const char* filename, BMPImage* image, char** error)
        //{
        //    FILE* output_ptr = _open_file(filename, "wb");

        //    if (!write_bmp(output_ptr, image, error))
        //    {
        //        _handle_error(error, output_ptr, image);
        //    }

        //    fflush(output_ptr);
        //    fclose(output_ptr);
        //    _clean_up(output_ptr, image, error);
        //}


        public class MyImage
        {
            public System.Drawing.Imaging.PixelFormat PixelFormat;
            public int Width;
            public int Height;

            private int Stride;

            public byte[] BmpBuffer
            {
                get
                {
                    return PointerToManagedArray(System.IntPtr.Zero, Height * Stride);
                }
            }

            // https://stackoverflow.com/questions/5486938/c-sharp-how-to-get-byte-from-intptr
            private static byte[] PointerToManagedArray(System.IntPtr pnt, int size)
            {
                byte[] managedArray = new byte[size];

                System.Runtime.InteropServices.Marshal.Copy(pnt, managedArray, 0, size);
                return managedArray;
            }

        }



        // https://stackoverflow.com/questions/11452246/add-a-bitmap-header-to-a-byte-array-then-create-a-bitmap-file
        public static byte[] ImageBitmap(MyImage image)
        {

            byte[] bmpBuffer = image.BmpBuffer;
            int bmpBufferSize = bmpBuffer.Length;


            // BmpBufferSize : a pure length of raw bitmap data without the header.
            // the 54 value here is the length of bitmap header.
            byte[] bitmapBytes = new byte[bmpBufferSize + 54];

            #region Bitmap Header
            // 0~2 "BM"
            bitmapBytes[0] = 0x42;
            bitmapBytes[1] = 0x4d;

            // 2~6 Size of the BMP file - Bit cound + Header 54
            System.Array.Copy(System.BitConverter.GetBytes(bmpBufferSize + 54), 0, bitmapBytes, 2, 4);

            // 6~8 Application Specific : normally, set zero
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 6, 2);

            // 8~10 Application Specific : normally, set zero
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 8, 2);

            // 10~14 Offset where the pixel array can be found - 24bit bitmap data always starts at 54 offset.
            System.Array.Copy(System.BitConverter.GetBytes(54), 0, bitmapBytes, 10, 4);
            #endregion

            #region DIB Header
            // 14~18 Number of bytes in the DIB header. 40 bytes constant.
            System.Array.Copy(System.BitConverter.GetBytes(40), 0, bitmapBytes, 14, 4);

            // 18~22 Width of the bitmap.
            System.Array.Copy(System.BitConverter.GetBytes(image.Width), 0, bitmapBytes, 18, 4);

            // 22~26 Height of the bitmap.
            System.Array.Copy(System.BitConverter.GetBytes(image.Height), 0, bitmapBytes, 22, 4);

            // 26~28 Number of color planes being used
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 26, 2);

            // 28~30 Number of bits. If you don't know the pixel format, trying to calculate it with the quality of the video/image source.
            if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                System.Array.Copy(System.BitConverter.GetBytes(24), 0, bitmapBytes, 28, 2);
            }

            // 30~34 BI_RGB no pixel array compression used : most of the time, just set zero if it is raw data.
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 30, 4);

            // 34~38 Size of the raw bitmap data ( including padding )
            System.Array.Copy(System.BitConverter.GetBytes(bmpBufferSize), 0, bitmapBytes, 34, 4);

            // 38~46 Print resolution of the image, 72 DPI x 39.3701 inches per meter yields
            if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 38, 4);
                System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 42, 4);
            }

            // 46~50 Number of colors in the palette
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 46, 4);

            // 50~54 means all colors are important
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 50, 4);
            #endregion DIB Header

            // 54~end : Pixel Data : Finally, time to combine your raw data, BmpBuffer in this code, with a bitmap header you've just created.
            System.Array.Copy(bmpBuffer as System.Array, 0, bitmapBytes, 54, bmpBufferSize);

            return bitmapBytes;
        } // End Function ImageBitmap 





        // https://www.manpagez.com/man/3/X11::Protocol::Ext::XFIXES/
        // https://github.com/D-Programming-Deimos/libX11/blob/master/c/X11/extensions/Xfixes.h

        // https://www.programiz.com/c-programming/examples/sizeof-operator-example
        /*
         #include<stdio.h>
int main() {
    int intType;
    float floatType;
    double doubleType;
    char charType;
    // sizeof evaluates the size of a variable
    printf("Size of int: %ld bytes\n", sizeof(intType));
    printf("Size of float: %ld bytes\n", sizeof(floatType));
    printf("Size of double: %ld bytes\n", sizeof(doubleType));
    printf("Size of char: %ld byte\n", sizeof(charType));
    
    return 0;
}
             */


        // https://www.displayfusion.com/Discussions/View/converting-c-data-types-to-c/?ID=38db6001-45e5-41a3-ab39-8004450204b3


        // https://www.geeksforgeeks.org/reverse-an-array-in-groups-of-given-size/
        // Given an array, reverse every sub-array formed by consecutive k elements.
        // Function to reverse every sub-array formed by consecutive k elements 
        public static void ReverseArrayInGroupsOfK(int[] arr, int k)
        {
            int n = arr.Length;

            for (int i = 0; i < n; i += k)
            {
                int left = i;

                // to handle case when k is  not multiple of n 
                int right = System.Math.Min(i + k - 1, n - 1);
                int temp;

                // reverse the sub-array [left, right] 
                while (left < right)
                {
                    temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                    left += 1;
                    right -= 1;
                } // Whend 

            } // Next i 

        } // End Sub ReverseArrayInGroupsOfK 

        /*


        #include <iostream> 
        using namespace std; 
  
        // Function to reverse every sub-array formed by 
        // consecutive k elements 
        void reverse(int arr[], int n, int k) 
        { 
            for (int i = 0; i < n; i += k) 
            { 
                int left = i; 
  
                // to handle case when k is not multiple of n 
                int right = min(i + k - 1, n - 1); 
  
                // reverse the sub-array [left, right] 
                while (left < right) 
                    swap(arr[left++], arr[right--]); 
  
            } 
        } 
  
        // Driver code 
        int main() 
        { 
            int arr[] = {1, 2, 3, 4, 5, 6, 7, 8}; 
            int k = 3; 
  
            int n = sizeof(arr) / sizeof(arr[0]); 
  
            reverse(arr, n, k); 
  
            for (int i = 0; i < n; i++) 
                cout << arr[i] << " "; 
  
            return 0; 
        } 
        */


        // https://stackoverflow.com/questions/6912601/write-ximage-to-bmp-file-in-c
        // https://stackoverflow.com/questions/14092290/creating-bitmap-object-from-raw-bytes
        // https://github.com/ststeiger/cef-pdf/blob/master/src/Bmp.h
        // https://github.com/ststeiger/cef-pdf/blob/master/src/Bmp.cpp
        // https://codereview.stackexchange.com/questions/196084/read-and-write-bmp-file-in-c/215955#215955
        static byte[] PadLines(byte[] bytes, int rows, int columns)
        {
            int currentStride = columns; // 3
            int newStride = columns;  // 4
            byte[] newBytes = new byte[newStride * rows];

            for (int i = 0; i < rows; i++)
                System.Buffer.BlockCopy(bytes, currentStride * i, newBytes, newStride * i, currentStride);

            return newBytes;
        }


        // https://stackoverflow.com/questions/34176795/any-efficient-way-of-converting-ximage-data-to-pixel-map-e-g-array-of-rgb-quad/38298349
        public static void GetStride(System.IntPtr pixels, int format, int width, int height)
        {
            // The stride is the width of a single row of pixels(a scan line),
            // rounded up to a four - byte boundary. 
            // If the stride is positive, the bitmap is top-down. 
            // If the stride is negative, the bitmap is bottom-up.
            // https://stackoverflow.com/questions/2185944/why-must-stride-in-the-system-drawing-bitmap-constructor-be-a-multiple-of-4

            int bitsPerPixel = ((int)format & 0xff00) >> 8;
            int bytesPerPixel = (bitsPerPixel + 7) / 8;
            int stride = 4 * ((width * bytesPerPixel + 3) / 4);

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, pixels);
        }


        // https://stackoverflow.com/questions/14092290/creating-bitmap-object-from-raw-bytes
        public static void foo(int format, int imageWidth, int imageHeight, byte[] imageData)
        {
            int columns = imageWidth;
            int rows = imageHeight;
            // int stride = columns; 
            byte[] newbytes = PadLines(imageData, rows, columns);

            int bitsPerPixel = ((int)format & 0xff00) >> 8;
            int bytesPerPixel = (bitsPerPixel + 7) / 8;
            int stride = 4 * ((imageWidth * bytesPerPixel + 3) / 4);
            // which is actually: int stride = 4 * Floor((imageWidth * bytesPerPixel + 3) / 4);


            System.Drawing.Bitmap im = new System.Drawing.Bitmap(columns, rows, stride,
                     System.Drawing.Imaging.PixelFormat.Format8bppIndexed,
                     System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(newbytes, 0)
            );

            im.Save(@"C:\Users\musa\Documents\Hobby\image21.bmp");
        }




    }
}

