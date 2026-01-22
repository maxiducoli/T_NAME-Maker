using System.Runtime.InteropServices;

namespace WEUtils
{
    public static class Simsala_BIN
    {
        #region ~ COMPRESOR ~
        private const string DLL_PATH = "compressUtils.dll";
        //private const string DLL_PATH = "D:\\Documentos\\Mis Proyectos C#\\WE Decompress 2k24 by CARP\\Debug\\compressUtils.dll";
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DeCompress")]
        private static extern bool DeCompress(ref IntPtr BufDest, IntPtr BufSrc);
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Compress")]
        public static extern bool Compress(ref IntPtr BufDest, IntPtr BufSrc, ref uint SizeResult, uint SizeSrc);
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.Cdecl, EntryPoint = "FindCompressedLength")]
        private static extern long FindCompressedLength(IntPtr BufSrc);
        // Declaración de la función nativa DecodeImage desde la DLL
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DecodeImage")]
        private static extern bool DecodeImage(IntPtr BufSrc, ref IntPtr BufDest, uint xsize, uint ysize, byte depth, byte ComprFlag);
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CompressBMPImage")]
        public static extern bool CompressBMPImage(string inputBMPFilePath, string outputDecodedFilePath, string outputCompressedFilePath);
        [DllImport(DLL_PATH, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateCompressedFile")]
        public static extern bool CreateCompressedFile(string inputBMPFilePath, string outputDecodedFilePath, string outputCompressedFilePath);
        // Descomprimir
        public static bool CallDecompress(ref byte[] data, byte[] buffer)
        {
            byte[] sourceData = buffer;
            byte[] destinationData = new byte[data.Length];
            IntPtr ptrSrc = Marshal.AllocHGlobal(sourceData.Length);
            IntPtr ptrDest = Marshal.AllocHGlobal(destinationData.Length);
            bool result = false;

            try
            {
                Marshal.Copy(sourceData, 0, ptrSrc, sourceData.Length);

                if (DeCompress(ref ptrDest, ptrSrc))
                {
                    Marshal.Copy(ptrDest, destinationData, 0, destinationData.Length);
                    Array.Copy(destinationData, data, data.Length);
                    result = true;
                }
                else
                {
                    Console.WriteLine("La descompresión falló.");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptrSrc);
                Marshal.FreeHGlobal(ptrDest);
            }

            return result;
        }
        // Tamaño del archivo comprimido
        public static int CallFindCompressedLength(byte[] data)
        {
            byte[] sourceData = data;
            IntPtr ptrSrc = Marshal.AllocHGlobal(sourceData.Length);
            long result = -1;

            try
            {
                Marshal.Copy(sourceData, 0, ptrSrc, sourceData.Length);
                result = FindCompressedLength(ptrSrc);
                if (result <= 0)
                {
                    Console.WriteLine("La lectura de datos falló.");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptrSrc);
            }

            return (int)result;
        }
        // Comprimir
        public static bool CallCompress(ref byte[] data, byte[] buffer, ref uint dataSize, int bufferSize)
        {
            IntPtr ptrSrc = IntPtr.Zero;
            IntPtr ptrDest = IntPtr.Zero;
            bool result = false;
            uint dataSizeTemp = dataSize;
            uint bufferSizeTemp = Convert.ToUInt32(bufferSize);
            int finalSize = 0;
            try
            {
                // Reserva de memoria para los punteros de origen y destino
                ptrSrc = Marshal.AllocHGlobal(bufferSize);
                ptrDest = Marshal.AllocHGlobal(bufferSize);  // Asume que dataSizeTemp es el tamaño máximo esperado
                data = new byte[bufferSize];
                // Copia de datos del array de bytes a la memoria no administrada
                Marshal.Copy(buffer, 0, ptrSrc, buffer.Length);

                // Llamada a la función nativa Compress
                result = Compress(ref ptrDest, ptrSrc, ref dataSizeTemp, bufferSizeTemp);

                if (result)
                {
                    // Copia del resultado desde la memoria no administrada al array de bytes
                    finalSize = Convert.ToInt32(dataSizeTemp);
                    data = new byte[finalSize];
                    Marshal.Copy(ptrDest, data, 0, finalSize);
                }
                else
                {
                    Console.WriteLine("La compresión falló.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CallCompress: {ex.Message}");
                result = false;
            }
            finally
            {
                // Liberación de memoria no administrada
                if (ptrSrc != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptrSrc);
                }
                if (ptrDest != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptrDest);
                }
            }
            dataSize = dataSizeTemp;
            return result;
        }
        // Convertir Imagen en RAW
        public static bool CallDecodeImage(byte[] bufSrc, ref byte[] bufDest, int xsize, int ysize, byte depth, byte comprFlag)
        {
            //bufDest = null;
            IntPtr ptrSrc = IntPtr.Zero;
            IntPtr ptrDest = IntPtr.Zero;
            bool result = false;

            try
            {
                // Pinning del array bufSrc para obtener un puntero a sus datos
                ptrSrc = Marshal.AllocHGlobal(bufSrc.Length);
                Marshal.Copy(bufSrc, 0, ptrSrc, bufSrc.Length);

                // Calcular el tamaño necesario para bufDest según los parámetros
                //decimal bytesPerPixel = (depth / 8m);
                int bufDestSize = xsize * ysize;
                ptrDest = Marshal.AllocHGlobal(bufDestSize);

                // Llamada a la función nativa DecodeImage
                result = DecodeImage(ptrSrc, ref ptrDest, (uint)xsize, (uint)ysize, depth, comprFlag);

                if (result)
                {
                    // Copia del resultado desde la memoria no administrada a un array administrado
                    bufDest = new byte[bufDestSize];
                    Marshal.Copy(ptrDest, bufDest, 0, (int)(bufDestSize));
                }
                else
                {
                    Console.WriteLine("La decodificación falló.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CallDecodeImage: {ex.Message}");
                return false;
            }
            finally
            {
                // Liberación de los punteros de memoria
                if (ptrSrc != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptrSrc);
                }
                if (ptrDest != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptrDest);
                }
            }

            return result;
        }
        // Comprimir imagen BMP 4 u 8 bits
        public static bool CallCompressBMPImage(string inputBMPFilePath, string outputRAW, string outputDecodedFilePath)
        {
            bool result = false;
            try
            {
                result = CompressBMPImage(inputBMPFilePath, outputRAW, outputDecodedFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al decomprimir: " + ex.Message);
            }

            return result;
        }
        public static bool CallCompressedFile(string inputBMPFilePath, string outputRAW, string outputDecodedFilePath)
        {
            bool result = false;
            try
            {
                result = CreateCompressedFile(inputBMPFilePath, outputRAW, outputDecodedFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al decomprimir: " + ex.Message);
            }

            return result;
        }
        #endregion
    }
}