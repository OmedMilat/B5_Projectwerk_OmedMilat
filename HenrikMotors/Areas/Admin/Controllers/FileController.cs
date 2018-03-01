using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HenrikMotors.Areas.Admin.Controllers
{

    public class FileController : ApiController
    {
        [HttpPost]
        public Task<HttpResponseMessage> PostFile(int id)
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new CustomMultipartFormDataStreamProvider(root);

            var task = request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(o =>
                {

                    string file1 = provider.FileData.First().LocalFileName;
                    // this is the file name on the server where the file was saved 

                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("File uploaded.")
                    };
                }
            );
            return task;
        }
    }

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path)
        { }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            // override the filename which is stored by the provider (by default is bodypart_x)
            string originalFileName = headers.ContentDisposition.Name.Trim('\"');
            return originalFileName;
        }
    }
}