using System.Collections.Generic;

namespace SJNScaffolding.RazorPage.Models
{

    public class LayuiResultDto<T>: LayuiResultDto
    {
        public LayuiResultDto()
        {
        }
        public int count { get; set; }

        public LayuiResultDto(int count, IList<T> data)
        {
            code = 0;
            this.count = count;
            this.data = data;
        }

        public IList<T> data { get; set; }
    }

    public class LayuiResultDto
    {
        public LayuiResultDto()
        {
        }

        public LayuiResultDto(string msg)
        {
            code = 0;
            this.msg = msg;
        }

        public int code { get; set; }
        public string msg { get; set; }
    }

}


