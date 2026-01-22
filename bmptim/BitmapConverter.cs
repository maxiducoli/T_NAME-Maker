using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace bmptim
{
    public class BitmapConverter
    {
        //public Bitmap ConvertTo4Bit(Bitmap original)
        //{
        //    //ColorPalette pal= original.Palette;
        //    // Crear una nueva imagen con formato de 4 bits
        //    Bitmap newBitmap = new Bitmap(original.Width, original.Height, PixelFormat.Format4bppIndexed);

        //    // Obtener la paleta de colores de la nueva imagen
        //    ColorPalette palette = newBitmap.Palette;

        //    // Asignar una paleta de 16 colores
        //    //for (int i = 0; i < 16; i++)
        //    //{
        //    //    palette.Entries[i] = Color.FromArgb(i * 17, i * 17, i * 17); // Escala de grises
        //    //}
        //    newBitmap.Palette = original.Palette;

        //    // Bloquear los bits de la nueva imagen para escribir los datos
        //    BitmapData newData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
        //        ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);

        //    // Bloquear los bits de la imagen original para leer los datos
        //    BitmapData originalData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height),
        //        ImageLockMode.ReadOnly, original.PixelFormat);

        //    int height = original.Height;
        //    int width = original.Width;

        //    // Copiar los datos de la imagen original a la nueva imagen de 4 bits
        //    unsafe
        //    {
        //        for (int y = 0; y < height; y++)
        //        {
        //            byte* originalRow = (byte*)originalData.Scan0 + (y * originalData.Stride);
        //            byte* newRow = (byte*)newData.Scan0 + (y * newData.Stride);

        //            for (int x = 0; x < width; x++)
        //            {
        //                Color originalColor = Color.FromArgb(originalRow[x * 4 + 2], originalRow[x * 4 + 1], originalRow[x * 4]);
        //                int grayScale = (int)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);
        //                int paletteIndex = grayScale / 16;

        //                if (x % 2 == 0)
        //                {
        //                    newRow[x / 2] = (byte)(paletteIndex << 4);
        //                }
        //                else
        //                {
        //                    newRow[x / 2] |= (byte)paletteIndex;
        //                }
        //            }
        //        }
        //    }

        //    // Desbloquear las imágenes
        //    original.UnlockBits(originalData);
        //    newBitmap.UnlockBits(newData);

        //    return newBitmap;
        //}


        //public Bitmap ConvertTo4Bit(Bitmap original)
        //{
        //    // Verificar que la imagen original ya tenga una paleta (debe ser 4bpp o 8bpp)
        //    if (original.PixelFormat != PixelFormat.Format8bppIndexed && original.PixelFormat != PixelFormat.Format4bppIndexed)
        //    {
        //        throw new InvalidOperationException("La imagen original debe estar en un formato indexado (4bpp o 8bpp) para convertirla a 4bpp.");
        //    }

        //    // Crear una nueva imagen con formato de 4 bits
        //    Bitmap newBitmap = new Bitmap(original.Width, original.Height, PixelFormat.Format4bppIndexed);

        //    // Asignar la paleta de colores de la imagen original a la nueva imagen
        //    ColorPalette palette = newBitmap.Palette;
        //    for (int i = 0; i < palette.Entries.Length; i++)
        //    {
        //        palette.Entries[i] = original.Palette.Entries[i];
        //    }
        //    newBitmap.Palette = palette;

        //    // Bloquear los bits de la nueva imagen para escribir los datos
        //    BitmapData newData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
        //        ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);

        //    // Bloquear los bits de la imagen original para leer los datos
        //    BitmapData originalData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height),
        //        ImageLockMode.ReadOnly, original.PixelFormat);

        //    int height = original.Height;
        //    int width = original.Width;

        //    // Copiar los datos de la imagen original a la nueva imagen de 4 bits
        //    unsafe
        //    {
        //        for (int y = 0; y < height; y++)
        //        {
        //            byte* originalRow = (byte*)originalData.Scan0 + (y * originalData.Stride);
        //            byte* newRow = (byte*)newData.Scan0 + (y * newData.Stride);

        //            for (int x = 0; x < width; x++)
        //            {
        //                // Leer el color del píxel original (asumiendo que es un formato indexado)
        //                int originalIndex = originalRow[x];

        //                // Mapear el índice original al nuevo formato de 4 bits
        //                int paletteIndex = originalIndex % 16;

        //                if (x % 2 == 0)
        //                {
        //                    newRow[x / 2] = (byte)(paletteIndex << 4);
        //                }
        //                else
        //                {
        //                    newRow[x / 2] |= (byte)paletteIndex;
        //                }
        //            }
        //        }
        //    }

        //    // Desbloquear las imágenes
        //    original.UnlockBits(originalData);
        //    newBitmap.UnlockBits(newData);

        //    return newBitmap;
        //}

        /*
         public Bitmap ConvertTo4Bit(Bitmap original)
         {
             // Crear una nueva imagen con formato de 4 bits
             Bitmap newBitmap = new Bitmap(original.Width, original.Height, PixelFormat.Format4bppIndexed);

             // Crear una paleta de 16 colores y asignarla a la nueva imagen
             ColorPalette palette = newBitmap.Palette;
             // Puedes usar una paleta predefinida o generar una dinámica basada en la imagen original
             for (int i = 0; i < 16; i++)
             {
                 palette.Entries[i] = Color.FromArgb(i * 17, i * 17, i * 17); // Escala de grises
             }
             newBitmap.Palette = palette;

             // Bloquear los bits de la nueva imagen para escribir los datos
             BitmapData newData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                 ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);

             // Bloquear los bits de la imagen original para leer los datos
             BitmapData originalData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height),
                 ImageLockMode.ReadOnly, original.PixelFormat);

             int height = original.Height;
             int width = original.Width;

             // Copiar los datos de la imagen original a la nueva imagen de 4 bits
             unsafe
             {
                 for (int y = 0; y < height; y++)
                 {
                     byte* originalRow = (byte*)originalData.Scan0 + (y * originalData.Stride);
                     byte* newRow = (byte*)newData.Scan0 + (y * newData.Stride);

                     for (int x = 0; x < width; x++)
                     {
                         // Leer el color del píxel original (32bpp)
                         int originalColorB = originalRow[x * 4];
                         int originalColorG = originalRow[x * 4 + 1];
                         int originalColorR = originalRow[x * 4 + 2];

                         Color originalColor = Color.FromArgb(originalColorR, originalColorG, originalColorB);

                         // Buscar el índice más cercano en la paleta
                         int closestColorIndex = 0;
                         int minDistance = int.MaxValue;

                         for (int i = 0; i < 16; i++)
                         {
                             Color paletteColor = palette.Entries[i];
                             int distance = ColorDistance(originalColor, paletteColor);

                             if (distance < minDistance)
                             {
                                 minDistance = distance;
                                 closestColorIndex = i;
                             }
                         }

                         // Asignar el color al nuevo bitmap en formato 4bpp
                         if (x % 2 == 0)
                         {
                             newRow[x / 2] = (byte)(closestColorIndex << 4);
                         }
                         else
                         {
                             newRow[x / 2] |= (byte)closestColorIndex;
                         }
                     }
                 }
             }

             // Desbloquear las imágenes
             original.UnlockBits(originalData);
             newBitmap.UnlockBits(newData);

             return newBitmap;
         }

         // Función para calcular la distancia entre dos colores (usando distancia euclidiana)
         private int ColorDistance(Color c1, Color c2)
         {
             int r = c1.R - c2.R;
             int g = c1.G - c2.G;
             int b = c1.B - c2.B;
             return r * r + g * g + b * b; // Evitamos la raíz cuadrada por eficiencia
         }

         */


        public Bitmap ConvertTo4Bit(Bitmap original)
        {
            // Crear una nueva imagen con formato de 4 bits
            Bitmap newBitmap = new Bitmap(original.Width, original.Height, PixelFormat.Format4bppIndexed);

            // Generar una paleta de 16 colores basada en los colores más frecuentes de la imagen original
            ColorPalette palette = newBitmap.Palette;
            Color[] originalColors = GetMostFrequentColors(original, 16);
            for (int i = 0; i < 16; i++)
            {
                palette.Entries[i] = originalColors[i];
            }
            newBitmap.Palette = palette;

            // Bloquear los bits de la nueva imagen para escribir los datos
            BitmapData newData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);

            // Bloquear los bits de la imagen original para leer los datos
            BitmapData originalData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height),
                ImageLockMode.ReadOnly, original.PixelFormat);

            int height = original.Height;
            int width = original.Width;

            // Copiar los datos de la imagen original a la nueva imagen de 4 bits
            unsafe
            {
                for (int y = 0; y < height; y++)
                {
                    byte* originalRow = (byte*)originalData.Scan0 + (y * originalData.Stride);
                    byte* newRow = (byte*)newData.Scan0 + (y * newData.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        // Leer el color del píxel original (32bpp)
                        int originalColorB = originalRow[x * 4];
                        int originalColorG = originalRow[x * 4 + 1];
                        int originalColorR = originalRow[x * 4 + 2];

                        Color originalColor = Color.FromArgb(originalColorR, originalColorG, originalColorB);

                        // Buscar el índice más cercano en la paleta
                        int closestColorIndex = 0;
                        int minDistance = int.MaxValue;

                        for (int i = 0; i < 16; i++)
                        {
                            Color paletteColor = palette.Entries[i];
                            int distance = ColorDistance(originalColor, paletteColor);

                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                closestColorIndex = i;
                            }
                        }

                        // Asignar el color al nuevo bitmap en formato 4bpp
                        if (x % 2 == 0)
                        {
                            newRow[x / 2] = (byte)(closestColorIndex << 4);
                        }
                        else
                        {
                            newRow[x / 2] |= (byte)closestColorIndex;
                        }
                    }
                }
            }

            // Desbloquear las imágenes
            original.UnlockBits(originalData);
            newBitmap.UnlockBits(newData);

            return newBitmap;
        }

        // Función para calcular la distancia entre dos colores (usando distancia euclidiana)
        private int ColorDistance(Color c1, Color c2)
        {
            int r = c1.R - c2.R;
            int g = c1.G - c2.G;
            int b = c1.B - c2.B;
            return r * r + g * g + b * b; // Evitamos la raíz cuadrada por eficiencia
        }

        // Función para obtener los N colores más frecuentes de la imagen original
        private Color[] GetMostFrequentColors(Bitmap bitmap, int count)
        {
            var colors = new Dictionary<int, int>();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int argb = color.ToArgb();

                    if (colors.ContainsKey(argb))
                        colors[argb]++;
                    else
                        colors[argb] = 1;
                }
            }

            return colors.OrderByDescending(kv => kv.Value)
                         .Take(count)
                         .Select(kv => Color.FromArgb(kv.Key))
                         .ToArray();
        }

        public Bitmap ConvertTo8Bit(Bitmap original)
        {
            // Crear una nueva imagen con formato de 8 bits
            Bitmap newBitmap = new Bitmap(original.Width, original.Height, PixelFormat.Format8bppIndexed);

            // Obtener la paleta de colores de la nueva imagen
            ColorPalette palette = newBitmap.Palette;

            // Asignar una paleta de 256 colores
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i); // Escala de grises
            }
            newBitmap.Palette = palette;

            // Bloquear los bits de la nueva imagen para escribir los datos
            BitmapData newData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Bloquear los bits de la imagen original para leer los datos
            BitmapData originalData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height),
                ImageLockMode.ReadOnly, original.PixelFormat);

            int height = original.Height;
            int width = original.Width;

            // Copiar los datos de la imagen original a la nueva imagen de 8 bits
            unsafe
            {
                for (int y = 0; y < height; y++)
                {
                    byte* originalRow = (byte*)originalData.Scan0 + (y * originalData.Stride);
                    byte* newRow = (byte*)newData.Scan0 + (y * newData.Stride);

                    for (int x = 0; x < width; x++)
                    {
                        Color originalColor = Color.FromArgb(originalRow[x * 4 + 2], originalRow[x * 4 + 1], originalRow[x * 4]);
                        int grayScale = (int)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);
                        newRow[x] = (byte)grayScale;
                    }
                }
            }

            // Desbloquear las imágenes
            original.UnlockBits(originalData);
            newBitmap.UnlockBits(newData);

            return newBitmap;
        }
        public byte[] GetImageData(Bitmap sourceImage)
        {
            int stride;
            if (sourceImage == null)
                throw new ArgumentNullException("sourceImage", "Source image is null!");
            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadOnly, sourceImage.PixelFormat);
            stride = sourceData.Stride;
            Byte[] data = new Byte[stride * sourceImage.Height];
            Marshal.Copy(sourceData.Scan0, data, 0, data.Length);
            sourceImage.UnlockBits(sourceData);
            return data;
        }
    }
}
