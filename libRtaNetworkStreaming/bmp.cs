namespace rtaNetworking.Streaming
{
    public class bmp
    {
        
    }
}


#ifndef LINUXSCREENSHOT_BMP_H
#define LINUXSCREENSHOT_BMP_H




#ifdef __cplusplus
    // #include <iostream>
	// #include <fstream>
	#include <cstdio>
	#include <cstdlib>
	#include <cstdint>
	#include <cstring>
#else
    #include <stdio.h> // printf
    #include <stdlib.h> // for malloc
    #include <stdint.h> // for int32_t, int8_t, etc.
    #include <stdbool.h> // true, false in plain old C
    #include <string.h>  // for strlen, strcopy
#endif



// https://www.geeksforgeeks.org/c-program-swap-two-numbers/
void swap(int *xp, int *yp)
{
    int temp = *xp;
    *xp = *yp;
    *yp = temp;
}

int swapTest1()
{
    int x, y;
    printf("Enter Value of x ");
    scanf("%d", &x);
    printf("\nEnter Value of y ");
    scanf("%d", &y);
    swap(&x, &y);
    printf("\nAfter Swapping: x = %d, y = %d", x, y);
    return 0;
}








/*
// or swap like this:
// x and y,
void swap2(int &x, int &y)
{
    int temp = x;
    x = y;
    y = temp;
}

int swapTest2()
{
    int x, y;
    printf("Enter Value of x ");
    scanf("%d", &x);
    printf("\nEnter Value of y ");
    scanf("%d", &y);
    swap2(x, y);
    printf("\nAfter Swapping: x = %d, y = %d", x, y);
    return 0;
}

*/



#if ( defined (__WIN32__) || defined (__WIN32) || defined (_WIN32) || defined (WIN32)  )
// windows specific code goes here
#pragma warning(disable:4458)

#pragma comment (lib,"gdiplus.lib")

#include <Windows.h>
#include <ObjIdl.h>
#include <minmax.h>
#include <gdiplus.h>
// #include <gdiplusheaders.h>
// #include <wingdi.h>
// #include <gdiplusbitmap.h>
// #include <gdiplusflat.h>
// #include <Gdipluspixelformats.h>


// using namespace Gdiplus;

#pragma warning(default:4458)
#endif



#define BMP_HEADER_SIZE 54
#define DIB_HEADER_SIZE 40

// Correct values for the header
#define MAGIC_VALUE         0x4D42
#define NUM_PLANE           1
#define COMPRESSION         0
#define NUM_COLORS          0
#define IMPORTANT_COLORS    0
#define BITS_PER_BYTE 8
// #define BITS_PER_PIXEL 24
#define BITS_PER_PIXEL 32


#ifdef _MSC_VER
#pragma pack(push)  // save the original data alignment
#pragma pack(1)     // Set data alignment to 1 byte boundary
#endif


typedef struct
#ifndef _MSC_VER
        __attribute__((packed))
#endif
{
    uint16_t type;              // Magic identifier: 0x4d42
    uint32_t size;              // File size in bytes
    uint16_t reserved1;         // Not used
    uint16_t reserved2;         // Not used
    uint32_t offset;            // Offset to image data in bytes from beginning of file
    uint32_t dib_header_size;   // DIB Header size in bytes
    int32_t  width_px;          // Width of the image
    int32_t  height_px;         // Height of image
    uint16_t num_planes;        // Number of color planes
    uint16_t bits_per_pixel;    // Bits per pixel
    uint32_t compression;       // Compression type
    uint32_t image_size_bytes;  // Image size in bytes
    int32_t  x_resolution_ppm;  // Pixels per meter
    int32_t  y_resolution_ppm;  // Pixels per meter
    uint32_t num_colors;        // Number of colors
    uint32_t important_colors;  // Important colors
} BMPHeader;


#ifdef _MSC_VER
#pragma pack(pop)  // restore the previous pack setting
#endif



typedef struct
{
    BMPHeader header;
    // unsigned char* data;
    // It is more informative and will force a necessary compiler error
    // on a rare machine with 16-bit char.
    uint8_t* data;
} BMPImage;





//   Return the size of an image row in bytes.
// - Precondition: the header must have the width of the image in pixels.
uint32_t computeImageSize(BMPHeader *bmp_header)
{
    uint32_t bytes_per_pixel = bmp_header->bits_per_pixel / BITS_PER_BYTE;
    uint32_t bytes_per_row_without_padding = bmp_header->width_px * bytes_per_pixel;
    uint32_t padding = (4 - (bmp_header->width_px * bytes_per_pixel) % 4) % 4;
    uint32_t row_size_bytes = bytes_per_row_without_padding + padding;

    // stride = row_size_bytes
    // int stride = 4 * ((bmp_header->width_px * bytes_per_pixel + 3) / 4);

    return row_size_bytes * bmp_header->height_px;
}




// Free all memory referred to by the given BMPImage.
void free_bmp(BMPImage *image)
{
    free(image->data);
    free(image);
}



// Close file and release memory.void _clean_up(FILE *fp, BMPImage *image, char **error)
void _clean_up(FILE *fp, BMPImage *image, char **error)
{
    if (fp != NULL)
    {
        fclose(fp);
    }
    free_bmp(image);
    free(*error);
}


// Print error message and clean up resources.
void _handle_error(char **error, FILE *fp, BMPImage *image)
{
    fprintf(stderr, "ERROR: %s\n", *error);
    _clean_up(fp, image, error);

    exit(EXIT_FAILURE);
}


// Open file. In case of error, print message and exit.
FILE *_open_file(const char *filename, const char *mode)
{
    FILE *fp = fopen(filename, mode);
    if (fp == NULL)
    {
        fprintf(stderr, "Could not open file %s\n", filename);

        exit(EXIT_FAILURE);
    }

    return fp;
}




//   Make a copy of a string on the heap.
// - Postcondition: the caller is responsible to free
//   the memory for the string.
char *_string_duplicate(const char *string)
{
    char *copy = (char*)malloc(sizeof(*copy) * (strlen(string) + 1));
    if (copy == NULL)
    {
        // return "Not enough memory for error message";
        const char* error_message = "Not enough memory for error message";
        size_t len = strlen(error_message);
        char* error = (char*)malloc(len * sizeof(char) + 1);
        strcpy(error, error_message);
        return error;
    }

    strcpy(copy, string);
    return copy;
}



// Check condition and set error message.
bool _check(bool condition, char **error, const char *error_message)
{
    bool is_valid = true;
    if (!condition)
    {
        is_valid = false;
        if (*error == NULL)  // to avoid memory leaks
        {
            *error = _string_duplicate(error_message);
        }
    }
    return is_valid;
}


//   Write an image to an already open file.
// - Postcondition: it is the caller's responsibility to free the memory
//   for the error message.
// - Return: true if and only if the operation succeeded.
bool write_bmp(FILE *fp, BMPImage *image, char **error)
{
    // Write header
    rewind(fp);
    size_t num_read = fwrite(&image->header, sizeof(image->header), 1, fp);
    if (!_check(num_read == 1, error, "Cannot write image"))
    {
        return false;
    }
    // Write image data
    num_read = fwrite(image->data, image->header.image_size_bytes, 1, fp);
    if (!_check(num_read == 1, error, "Cannot write image"))
    {
        return false;
    }

    return true;
}


void write_image(const char *filename, BMPImage *image, char **error)
{
    FILE *output_ptr = _open_file(filename, "wb");

    if (!write_bmp(output_ptr, image, error))
    {
        _handle_error(error, output_ptr, image);
    }

    fflush(output_ptr);
    fclose(output_ptr);
    _clean_up(output_ptr, image, error);
}




#endif //LINUXSCREENSHOT_BMP_H
