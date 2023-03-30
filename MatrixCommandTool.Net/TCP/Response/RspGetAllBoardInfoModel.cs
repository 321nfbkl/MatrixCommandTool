using MatrixCommandTool.Net.TCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.TCP.Response
{
    public class RspGetAllBoardInfoModel : NetBaseModel
    {
        public List<string> BoardName { get; set; }
    }
}
