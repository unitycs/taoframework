#region License
/*
 MIT License
 Copyright 2008 Tao Framework Team
 http://www.taoframework.com
 All rights reserved.

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
*/
#endregion License

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Collections.Generic;
using Tao.FreeType;

namespace Tao.FtGl
{
    /// #region Class Documentation
    /// <summary>
    ///     FTGL bindings for .NET, implementing FTGL 2.1.3.0
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// #endregion Class Documentation
    public static class FtGl
    {
        #region Private Constants

        private const string FTGL_NATIVE_LIBRARY = "ftgl.dll";
        private const CallingConvention CALLING_CONVENTION = CallingConvention.Cdecl;

        #endregion Private Constants

        /// <summary>
        ///
        /// </summary>
        public class FTGLBitmapFont : FTFont
        {
            /// <summary>
            /// Open and read a font file. Create a Bitmap font
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private extern static IntPtr ftglCreateBitmapFont(string namefont);

            public FTGLBitmapFont(string namefont)
            {
                _ptr = ftglCreateBitmapFont(namefont);
            }
        }
        public class FTGLPixmapFont : FTFont
        {
            /// <summary>
            /// Open and read a font file. Create a Pixmap font
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private extern static IntPtr ftglCreatePixmapFont(string namefont);

            public FTGLPixmapFont(string namefont)
            {
                _ptr = ftglCreatePixmapFont(namefont);
            }
        }
        public class FTGLOutlineFont : FTFont
        {
            /// <summary>
            /// Open and read a font file. Create a Outline font
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private extern static IntPtr ftglCreateOutlineFont(string namefont);

            public FTGLOutlineFont(string namefont)
            {
                _ptr = ftglCreateOutlineFont(namefont);
            }
        }
        public class FTGLPolygonFont : FTFont
        {
            /// <summary>
            /// Open and read a font file. Create a Polygon font
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private extern static IntPtr ftglCreatePolygonFont(string namefont);

            public FTGLPolygonFont(string namefont)
            {
                _ptr = ftglCreatePolygonFont(namefont);
            }
        }
        public class FTGLExtrudeFont : FTFont
        {
            /// <summary>
            /// Open and read a font file. Create a Extrud font
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private extern static IntPtr ftglCreateExtrudeFont(string namefont);

            public FTGLExtrudeFont(string namefont)
            {
                _ptr = ftglCreateExtrudeFont(namefont);
            }
        }
        public class FTGLTextureFont : FTFont
        {
            /// <summary>
            /// Open and read a font file. Create a Texture font
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private extern static IntPtr ftglCreateTextureFont(string namefont);

            public FTGLTextureFont(string namefont)
            {
                _ptr = ftglCreateTextureFont(namefont);
            }
        }

        /// <summary>
        /// Generic Font abstract class
        /// </summary>
        abstract public class FTFont : IDisposable
        {
            [CLSCompliant(false)]
            protected IntPtr _ptr = IntPtr.Zero;

            public IntPtr Pointer { get{ return _ptr;} }

            public FTFont()
            {
            }

            /// <summary>
            /// Close a font file.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private extern static void ftglDestroyFont(IntPtr font);

            public void Dispose()
            {
                if(_ptr != IntPtr.Zero)
                    ftglDestroyFont(_ptr);
                _ptr = IntPtr.Zero;
            }

            ~FTFont()
            {
                if(_ptr != IntPtr.Zero)
                    ftglDestroyFont(_ptr);
                _ptr = IntPtr.Zero;
            }

            /// <summary>
            /// Gets the line spacing for the font.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern float ftglGetFontLineHeight(IntPtr font);

            public float LineHeight()
            {
                return ftglGetFontLineHeight(_ptr);
            }

            /// <summary>
            /// Set the character map for the face.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern int ftglSetFontCharMap(IntPtr font, FT_Encoding encoding);

            [CLSCompliant(false)]
            public int CharMap(FT_Encoding encoding)
            {
                return ftglSetFontCharMap(_ptr, encoding);
            }

            /// <summary>
            /// Get the bounding box for a string.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglGetFontBBox(IntPtr font, string str, int start, int end, float[] coord);

            public void BBox(string str,
                             out float x1, out float y1, out float z1,
                             out float x2, out float y2, out float z2)
            {
                BBox(str, 0, -1, out x1, out y1, out z1,
                                 out x2, out y2, out z2);
            }

            public void BBox(string str, int start, int end,
                             out float x1, out float y1, out float z1,
                             out float x2, out float y2, out float z2)
            {
                x1 = y1 = z1 = x2 = y2 = z2 = 0;
                float[]coord  = new float[6];
                ftglGetFontBBox(_ptr, str, start, end, coord);
                x1 = coord[0]; y1 = coord[1]; z1 = coord[2];
                x2 = coord[3]; y2 = coord[4]; z2 = coord[5];
            }

            /// <summary>
            /// Get the current face size in points.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern uint ftglGetFontFaceSize(IntPtr font);

            public int FaceSize()
            {
                return (int)ftglGetFontFaceSize(_ptr);
            }

            /// <summary>
            /// Set the char size for the current face.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern int ftglSetFontFaceSize(IntPtr font, uint size, uint res);

            public int FaceSize(int size, int res)
            {
                return ftglSetFontFaceSize(_ptr, (uint)size, (uint)res);
            }

            public int FaceSize(int size)
            {
                return ftglSetFontFaceSize(_ptr, (uint)size, 72);
            }

            /// <summary>
            /// Gets the global descender height for the face.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern float ftglGetFontDescender(IntPtr font);

            public float Descender()
            {
                return ftglGetFontDescender(_ptr);
            }

            /// <summary>
            /// Get the global ascender height for the face.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern float ftglGetFontAscender(IntPtr font);

            public float Ascender()
            {
                return ftglGetFontAscender(_ptr);
            }

            /// <summary>
            /// Get the advance width for a string.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern float ftglGetFontAdvance(IntPtr font, string str);

            public float Advance(string str)
            {
                return ftglGetFontAdvance(_ptr, str);
            }

            /// <summary>
            /// Set the extrusion distance for the font. Only implemented by FTGLExtrudeFont
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglSetFontDepth(IntPtr font, float depth);

            public void Depth(float depth)
            {
                ftglSetFontDepth(_ptr, depth);
            }

            /// <summary>
            /// Set the outset distance for the font. Only implemented by FTGLOutlineFont,
            /// FTGLPolygonFont and FTGLExtrudeFont
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglSetFontOutset(IntPtr font, float front, float back);

            public void Outset(float front, float back)
            {
                ftglSetFontOutset(_ptr, front, back);
            }

            /// <summary>
            /// Render a string of characters.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglRenderFont(IntPtr font, string str);

            public void Render(string str)
            {
                ftglRenderFont(_ptr, str);
            }

            /// <summary>
            /// Queries the Font for errors.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern int ftglGetFontError(IntPtr font);

            public int Error()
            {
                return ftglGetFontError(_ptr);
            }

            /// <summary>
            /// Attach auxilliary file to font e.g font metrics.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern int ftglAttachFile(IntPtr font, string path);

            public bool Attach(string pathFont)
            {
                int rtn = ftglAttachFile(_ptr, pathFont);
                if(rtn == 1)
                    return true;
                return false;
            }

            /// <summary>
            /// Attach auxilliary data to font e.g font metrics, from memory
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern int ftglAttachData(IntPtr font, string p, int size);

            public bool Attach(string p, int size)
            {
                int rtn = ftglAttachData(_ptr, p, size);
                if(rtn == 1)
                    return true;
                return false;
            }

            /// <summary>
            /// Get the number of character maps in this face.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern int ftglGetFontCharMapCount(IntPtr font);

            public int CharMapCount()
            {
                return ftglGetFontCharMapCount(_ptr);
            }

            /// <summary>
            /// Get a list of character maps in this face.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern List<FT_Encoding> ftglGetFontCharMapList(IntPtr font);

            [CLSCompliant(false)]
            public List<FT_Encoding> CharMapList()
            {
                return ftglGetFontCharMapList(_ptr);
            }

            /// <summary>
            /// Enable or disable the use of Display Lists inside FTGL.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglSetFontDisplayList(IntPtr font, int use);

            public void SetDisplayList(bool use)
            {
                if(!use)
                    ftglSetFontDisplayList(_ptr, 0);
                else
                    ftglSetFontDisplayList(_ptr, 1);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public enum RenderMode : int
        {
            /// <summary>
            ///
            /// </summary>
            RENDER_FRONT = 0x01,
            RENDER_BACK = 0x02,
            RENDER_SIDE = 0x04,
        }
        /// <summary>
        ///
        /// </summary>
        public enum TextAlignment : int
        {
            /// <summary>
            ///
            /// </summary>
            ALIGN_LEFT = 0,
            ALIGN_CENTER = 1,
            ALIGN_RIGHT = 2,
            ALIGN_JUSTIFY = 3,
        }


        /// <summary>
        ///
        /// </summary>
        public class FTSimpleLayout: FTLayout
        {

            /// <summary>
            ///
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private extern static IntPtr ftglCreateSimpleLayout();

            public FTSimpleLayout()
            {
                _ptr = ftglCreateSimpleLayout();
            }

            /// <summary>
            /// Render a string of characters with a specific render mode
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglRenderLayout(IntPtr layout, string str, int mode);

            public void RenderMode(string str, RenderMode mode)
            {
                ftglRenderLayout(_ptr, str, (int)mode);
            }

            /// <summary>
            /// Render a string of characters and distribute extra space amongst the whitespace
            /// regions of the string.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglRenderLayoutSpace(IntPtr layout, string str, float ExtraSpace);

            public void RenderSpace(string str, float extraSpace)
            {
                ftglRenderLayoutSpace(_ptr, str, extraSpace);
            }

            /// <summary>
            /// The maximum line length for formatting text.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglSetLayoutLineLength(IntPtr layout, float lenght);

            public void SetLineLength(float length)
            {
                ftglSetLayoutLineLength(_ptr, length);
            }

            /// <summary>
            /// Get the current line length.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern float ftglGetLayoutLineLength(IntPtr layout);

            public float GetLineLength()
            {
                return ftglGetLayoutLineLength(_ptr);
            }

            /// <summary>
            /// The text alignment mode used to distribute space within a line or rendered text.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglSetLayoutAlignment(IntPtr layout, int align);

            public void SetAlignment(TextAlignment align)
            {
                ftglSetLayoutAlignment(_ptr, (int)align);
            }

            /// <summary>
            /// Get the text alignment mode.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern int ftglGetLayoutAlignement(IntPtr layout);

            public TextAlignment GetAlignment()
            {
                return (TextAlignment)ftglGetLayoutAlignement(_ptr);
            }

            /// <summary>
            /// Sets the line height.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglSetLayoutLineSpacing(IntPtr layout, float space);

            public void SetLineSpacing(float space)
            {
                ftglSetLayoutLineSpacing(_ptr, space);
            }

            /// <summary>
            /// Get the line spacing.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern float ftglGetLayoutLineSpacing(IntPtr layout);

            public float GetLineSpacing()
            {
                return ftglGetLayoutLineSpacing(_ptr);
            }
        }

        /// <summary>
        /// Generic Layout abstract class
        /// </summary>
        abstract public class FTLayout : IDisposable
        {
            [CLSCompliant(false)]
            protected IntPtr _ptr = IntPtr.Zero;

            public FTLayout() {}

            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private extern static void ftglDestroyLayout(IntPtr layout);

            public void Dispose()
            {
                if(_ptr != IntPtr.Zero)
                    ftglDestroyLayout(_ptr);
                _ptr = IntPtr.Zero;
            }

            ~FTLayout()
            {
                if(_ptr != IntPtr.Zero)
                    ftglDestroyLayout(_ptr);
                _ptr = IntPtr.Zero;
            }

            /// <summary>
            /// Get the bounding box for a formatted string.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglGetLayoutBBox(IntPtr layout, string str, float[] coord);

            public void BBox(string str, out float x1, out float y1, out float z1,
                                         out float x2, out float y2, out float z2)
            {
                x1 = y1 = z1 = x2 = y2 = z2 = 0;
                float[]coord  = new float[6];
                ftglGetLayoutBBox(_ptr, str, coord);
                x1 = coord[0]; y1 = coord[1]; z1 = coord[2];
                x2 = coord[3]; y2 = coord[4]; z2 = coord[5];
            }

            /// <summary>
            /// Set the font to use for rendering the text.
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern void ftglSetLayoutFont(IntPtr layout, IntPtr fontToAdd);

            public void SetFont(FTFont font)
            {
                ftglSetLayoutFont(_ptr, font.Pointer);
            }

            /// <summary>
            /// Get the current font
            /// </summary>
            [DllImport(FTGL_NATIVE_LIBRARY, CallingConvention = CALLING_CONVENTION), SuppressUnmanagedCodeSecurity]
            private static extern FTFont ftglGetLayoutFont(IntPtr layout);

            public FTFont GetFont()
            {
                return ftglGetLayoutFont(_ptr);
            }
        }
    }
}
