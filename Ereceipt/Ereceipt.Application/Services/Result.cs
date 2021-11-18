using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ereceipt.Application.Services
{
    public abstract class Result
    {
        public Result(bool isSuccessed, string error) { IsSuccessed = isSuccessed; Error = error; }
        public Result() { IsSuccessed = true; Error = null; }
        public Result(string error) : this(error == null ? true : false, error) { }
        public bool IsSuccessed { get; set; }
        public string Error { get; set; }
    }
    public class Result<TResult> : Result
    {
        public Result() : base() { }
        public Result(string error) : base(error) { }
        public Result(TResult result) : base() { Data = result; }
        public TResult Data { get; set; }
    }
}
