using Api.Models;
using QRCodeSquad;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class QRCodeController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetData([FromBody] FileInput data)
        {
            try
            {
                Result res = new Result();

                if (data.type == FileInput.FileType.IMG)
                {
                    var bytes = Convert.FromBase64String(data.base64);

                    using (var ms = new MemoryStream(bytes))
                    {
                        var bmp = new Bitmap(ms);
                        res.data = new List<string>();
                        res.data.Add(QRService.QRDecode(bmp));
                    }
                } else if(data.type == FileInput.FileType.PDF)
                {
                    var bytes = Convert.FromBase64String(data.base64);
                    res.data = QRService.QRDecodePDF(bytes);
                }

                return Json(res);
            }
            catch (Exception)
            {
                return InternalServerError(new Exception("Formato inválido"));
            }
        }
    }
}
