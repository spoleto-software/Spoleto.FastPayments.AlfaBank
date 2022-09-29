using System;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using CIS.FastPayments.AlfaBank.Helpers;
using CIS.FastPayments.AlfaBank.Models;

namespace CIS.FastPayments.AlfaBank.Extensions
{
    public static class ModelExtension
    {
        public static Image ToImage(this QRCodeResponseModel responseModel)
        => ImageHelper.ConvertToImage(responseModel.ImageBytes);

        public static void SaveImageToFile(this QRCodeResponseModel responseMode, string filename)
            => ImageHelper.SaveImageToFile(responseMode.ImageBytes, filename);

        public static Image ToImage(this QRСodeCashLinkResponseModel responseModel)
            => ImageHelper.ConvertToImage(responseModel.ContentBytes);

        public static void SaveImageToFile(this QRСodeCashLinkResponseModel responseMode, string filename)
            => ImageHelper.SaveImageToFile(responseMode.ContentBytes, filename);

    }
}
