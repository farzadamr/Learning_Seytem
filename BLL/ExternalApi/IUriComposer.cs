using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ExternalApi
{
    public interface IUriComposer
    {
        string Compose(string src);
    }
    public class UriComposer : IUriComposer
    {
        public string Compose(string src)
        {
            return "https://localhost:44373/" + src;
        }
    }
}
