/*
* Copyright 2008 ZXing authors
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;

using com.google.zxing.common;
using com.google.zxing.oned;
using com.google.zxing.pdf417.encoder;
using com.google.zxing.qrcode;

namespace com.google.zxing
{
   /// <summary> This is a factory class which finds the appropriate Writer subclass for the BarcodeFormat
   /// requested and encodes the barcode with the supplied contents.
   /// 
   /// </summary>
   /// <author>  dswitkin@google.com (Daniel Switkin)
   /// </author>
   /// <author>www.Redivivus.in (suraj.supekar@redivivus.in) - Ported from ZXING Java Source 
   /// </author>
   public sealed class MultiFormatWriter : Writer
   {
      private static readonly IDictionary<BarcodeFormat, Func<Writer>> formatMap;

      static MultiFormatWriter()
      {
         formatMap = new Dictionary<BarcodeFormat, Func<Writer>>
                        {
                           {BarcodeFormat.EAN_8, () => new EAN8Writer()},
                           {BarcodeFormat.EAN_13, () => new EAN13Writer()},
                           {BarcodeFormat.UPC_A, () => new UPCAWriter()},
                           {BarcodeFormat.QR_CODE, () => new QRCodeWriter()},
                           {BarcodeFormat.CODE_39, () => new Code39Writer()},
                           {BarcodeFormat.CODE_128, () => new Code128Writer()},
                           {BarcodeFormat.ITF, () => new ITFWriter()},
                           {BarcodeFormat.PDF_417, () => new PDF417Writer()},
                           {BarcodeFormat.CODABAR, () => new CodaBarWriter()},
                        };
      }

      public BitMatrix encode(String contents, BarcodeFormat format, int width, int height)
      {
         return encode(contents, format, width, height, null);
      }

      public BitMatrix encode(String contents, BarcodeFormat format, int width, int height, IDictionary<EncodeHintType, object> hints)
      {
         if (!formatMap.ContainsKey(format))
            throw new ArgumentException("No encoder available for format " + format);

         return formatMap[format]().encode(contents, format, width, height, hints);
      }
   }
}