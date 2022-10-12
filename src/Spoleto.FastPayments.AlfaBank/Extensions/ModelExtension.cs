using System.Drawing;
using Spoleto.FastPayments.AlfaBank.Helpers;
using Spoleto.FastPayments.AlfaBank.Models;

namespace Spoleto.FastPayments.AlfaBank.Extensions
{
    public static class ModelExtension
    {
        //public static Image ToImage(this QRCodeResponseModel responseModel)
        //=> ImageHelper.ConvertToImage(responseModel.ImageBytes);

        //public static void SaveImageToFile(this QRCodeResponseModel responseMode, string filename)
        //    => ImageHelper.SaveImageToFile(responseMode.ImageBytes, filename);

        //public static Image ToImage(this QRСodeCashLinkResponseModel responseModel)
        //    => ImageHelper.ConvertToImage(responseModel.ContentBytes);

        //public static void SaveImageToFile(this QRСodeCashLinkResponseModel responseMode, string filename)
        //    => ImageHelper.SaveImageToFile(responseMode.ContentBytes, filename);

        /// <summary>
        /// Успешная оплата?
        /// </summary>
        public static bool IsStatusSuccessful(this QRCodeStatusResponseModel responseModel)
            => responseModel.Status == QRCodeStatus.ACWP;

        /// <summary>
        /// Отказ в оплате данного QR-кода?
        /// </summary>
        public static bool IsStatusFailed(this QRCodeStatusResponseModel responseModel)
            => responseModel.Status == QRCodeStatus.RJCT;

        /// <summary>
        /// В обработке: необходимо повторить запрос до получения одного из финальных статусов «ACWP» или «RJCT»?
        /// </summary>
        public static bool IsStatusWaiting(this QRCodeStatusResponseModel responseModel)
            => responseModel.Status == QRCodeStatus.NTST
            || responseModel.Status == QRCodeStatus.RCVD
            || responseModel.Status == QRCodeStatus.ACTC;

    }
}
