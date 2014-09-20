﻿using System;
using System.Web;
using System.Drawing;
using System.IO;
using System.Web.SessionState;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace MyAdmin
{
    /// <summary>
    /// Summary description for Captcha
    /// </summary>
    public class Captcha : IHttpHandler, IRequiresSessionState
    {
        public static string KeyEncode = "FidWEdfG";
        public void ProcessRequest(HttpContext context)
        {
            string Key = string.Empty;


            Key = MyUtility.MySecurity.GenerateString(5).ToUpper();
            context.Session["Captcha"] = MyUtility.MySecurity.AES.Encrypt(Key, KeyEncode);


            int iHeight = 50;
            int iWidth = 150;

            Random oRandom = new Random();

            int[] aBackgroundNoiseColor = new int[] { 150, 150, 150 };
            int[] aTextColor = new int[] { 0, 0, 0 };
            int[] aFontEmSizes = new int[] { 16, 19, 22, 25, 28, 31 };

            string[] aFontNames = new string[]
            {
                "Comic Sans MS",
                "Arial",
                "Times New Roman",
                "Georgia",
                "Verdana",
                "Geneva"
            };

            FontStyle[] aFontStyles = new FontStyle[]
            {  
                FontStyle.Bold,
                FontStyle.Italic,
                FontStyle.Regular,
                FontStyle.Strikeout,
                FontStyle.Underline             
            };

            HatchStyle[] aHatchStyles = new HatchStyle[]
            {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, 
                HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
                HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical, 
                HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
                HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, 
                HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, 
                HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
                HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal, 
                HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
                HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal, 
                HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
                HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard, 
                HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
                HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis, 
                HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
                HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
            };

            //Get Captcha in Session
            string sCaptchaText = Key;

            //Creates an output Bitmap
            Bitmap oOutputBitmap = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
            Graphics oGraphics = Graphics.FromImage(oOutputBitmap);
            oGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            //Create a Drawing area
            RectangleF oRectangleF = new RectangleF(0, 0, iWidth, iHeight);
            Brush oBrush = default(Brush);

            //Draw background (Lighter colors RGB 100 to 255)
            oBrush = new HatchBrush(aHatchStyles[oRandom.Next
                (aHatchStyles.Length - 1)], Color.FromArgb((oRandom.Next(100, 255)),
                (oRandom.Next(100, 255)), (oRandom.Next(100, 255))), Color.White);
            oGraphics.FillRectangle(oBrush, oRectangleF);

            System.Drawing.Drawing2D.Matrix oMatrix = new System.Drawing.Drawing2D.Matrix();
            int i = 0;
            for (i = 0; i <= sCaptchaText.Length - 1; i++)
            {
                oMatrix.Reset();
                int iChars = sCaptchaText.Length;
                int x = iWidth / (iChars + 1) * i;
                int y = iHeight / 2;

                //Rotate text Random
                oMatrix.RotateAt(oRandom.Next(-40, 40), new PointF(x, y));
                oGraphics.Transform = oMatrix;

                //Draw the letters with Random Font Type, Size and Color
                oGraphics.DrawString
                (
                    //Text
                sCaptchaText.Substring(i, 1),
                    //Random Font Name and Style
                new Font(aFontNames[oRandom.Next(aFontNames.Length - 1)],
                   aFontEmSizes[oRandom.Next(aFontEmSizes.Length - 1)],
                   aFontStyles[oRandom.Next(aFontStyles.Length - 1)]),
                    //Random Color (Darker colors RGB 0 to 100)
                new SolidBrush(Color.FromArgb(oRandom.Next(0, 100),
                   oRandom.Next(0, 100), oRandom.Next(0, 100))),
                x,
                oRandom.Next(0, iHeight / 4)
                );
                oGraphics.ResetTransform();
            }

            // Change the response headers to output a JPEG image.
            context.Response.Clear();
            context.Response.BufferOutput = true;
            context.Response.ContentType = "image/png";

            using (MemoryStream ms = new MemoryStream())
            {
                oOutputBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.WriteTo(context.Response.OutputStream);
            }

            oOutputBitmap.Dispose();

            context.Response.Flush();
            context.Response.End();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}