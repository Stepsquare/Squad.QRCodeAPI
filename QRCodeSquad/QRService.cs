﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace QRCodeSquad
{
    public static class QRService
    {

        public static List<string> QRDecodePDF(byte[] bytes)
        {
            var pages = new PDFiumSharp.PdfDocument(bytes).Pages;
            List<string> results = new List<string>();

            foreach (var i in pages) {
                using (var bmp = new PDFiumSharp.PDFiumBitmap((int)i.Width, (int)i.Height, false))
                {
                    i.Render(bmp);

                    MemoryStream ms = new MemoryStream(); 
                    bmp.Save(ms);
                    using (ms)
                    {
                        Bitmap img = new Bitmap(ms);
                        var res = QRDecode(img);
                        results.AddRange(res);
                    }
                }
            }

            return results;
        }

        public static Bitmap GetBitmap(string folder)
        {
            
            return (Bitmap)System.Drawing.Image.FromFile(folder);
        }

        public static List<string> QRDecode(Bitmap bmp)
        {
            List<string> res = new List<string>();
            // create a barcode reader instance

            IBarcodeReader reader = new BarcodeReader();
            // load a bitmap
            var barcodeBitmap = bmp;
            // detect and decode the barcode inside the bitmap
            var result = reader.DecodeMultiple(barcodeBitmap);
            // do something with the result
            if (result != null)
            {
                //var res1 = result.BarcodeFormat.ToString();
                res.AddRange(result.Select(j => j.Text).ToList());

                return res;
            } else
            {
                return null;
            }
        }
    }
}
